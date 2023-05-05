using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using OpenCvSharp;
using Unity.Barracuda;
using TMPro;


public class ObjDetect : MonoBehaviour
{
    // Start is called before the first frame update
    WebCamTexture camTexture;
    [SerializeField]
    private NNModel yoloModel;

    [SerializeField]
    private TMP_Text outputClass;
    private Model runtimeModel;
    private IWorker worker;
    private string outputLayerName;
    public Toggle objToggle;
    public float detectionThreshold = 0.60f;
    //public RawImage cameraView;
    
    private int targetFPS = 30;
    private int targetWidth = 1280;
    private int targetHeight = 720;



    void Start()
    {

        objToggle.isOn = false;
        WebCamDevice[] devices = WebCamTexture.devices;
        camTexture = new WebCamTexture(devices[0].name, targetWidth, targetHeight, targetFPS);
        runtimeModel = ModelLoader.Load(yoloModel);
        worker = WorkerFactory.CreateWorker(WorkerFactory.Type.Auto, runtimeModel);
        outputLayerName = runtimeModel.outputs[runtimeModel.outputs.Count - 1];
        //cameraView.texture = camTexture;
    }

    // Update is called once per frame
    void Update()
    {

        if (Time.frameCount % 20 == 0)
        {
            detect();
        }

    }
    public void detect()
    {
        if (objToggle.isOn != false)
        {
            //GetComponent<Renderer>().material.mainTexture = camTexture;
            camTexture.Play();

            Texture2D tex = convertCamTo2d(camTexture);
            Texture2D resizedTex = ResizeTexture(tex, 640, 640);



            using (Tensor inputTensor = new Tensor(resizedTex, channels: 3))
            {

                worker.Execute(inputTensor);
                Tensor outputTensor = worker.PeekOutput(outputLayerName);
                var indexWithHighestProbability = outputTensor.ArgMax()[0];
                ParseYoloV5Output(outputTensor, detectionThreshold);
               
                outputTensor.Dispose();
            }

        }
        else
        {
            camTexture.Stop();
            UnityEngine.Debug.Log($"Toggle is not on");
        }

    }
    public void OnDestroy()
    {
        worker?.Dispose();
    }
    public void ChangeToggle()
    {
        objToggle.isOn = !objToggle.isOn;
    }
    private Texture2D ResizeTexture(Texture2D texture, int width, int height)
    {
        // RenderTexture renderTexture = RenderTexture.GetTemporary(width, height, 24);
        RenderTexture renderTexture = new RenderTexture(width, height, 24);
        RenderTexture.active = renderTexture;
        Graphics.Blit(texture, renderTexture);
        Texture2D resizedTexture = new Texture2D(width, height);
        resizedTexture.ReadPixels(new UnityEngine.Rect(0, 0, width, height), 0, 0);
        resizedTexture.Apply();
        RenderTexture.active = null;
        //RenderTexture.ReleaseTemporary(renderTexture);
        return resizedTexture;
    }
    public Texture2D convertCamTo2d(WebCamTexture webCamTexture)
    {
        // Create new texture2d
        Texture2D tx2d = new Texture2D(webCamTexture.width, webCamTexture.height);
        // Gets all color data from web cam texture and then Sets that color data in texture2d
        tx2d.SetPixels(webCamTexture.GetPixels());
        // Applying new changes to texture2d
        tx2d.Apply();
        Resources.UnloadUnusedAssets();
        return tx2d;
    }
    private void ParseYoloV5Output(Tensor tensor, float thresholdMax)
    {
        //var boxes = new List<BoundingBox>();

        for (int i = 0; i < 25200; i++)
        {
            float confidence = GetConfidence(tensor, i);
            if (confidence < thresholdMax)
                continue;
            
            (int classIdx, float maxClass) = GetClassIdx(tensor, i);
            var className = GetClassName(classIdx);
            
            float maxScore = confidence * maxClass;

            if (maxScore < thresholdMax)
                continue;
            UnityEngine.Debug.Log($"Image was recognised as {className}");
            outputClass.text = className;
            /*float X = tensor[0, 0, 0, i];
            float Y = tensor[0, 0, 1, i];
            float Width = tensor[0, 0, 2, i];
            float Height = tensor[0, 0, 3, i];
            UnityEngine.Rect bbox = new UnityEngine.Rect(X, Y, Width, Height);*/
        }

        
    }
  

    private float GetConfidence(Tensor tensor, int row)
    {
        float tConf = tensor[0, 0, 4, row];
        return Sigmoid(tConf);
    }

    private ValueTuple<int, float> GetClassIdx(Tensor tensor, int row)
    {
        int classIdx = 0;

        float maxConf = tensor[0, 0, 5, row];

        for (int i = 0; i < 7; i++)
        {
            if (tensor[0, 0, 5 + i, row] > maxConf)
            {
                maxConf = tensor[0, 0, 5 + i, row];
                classIdx = i;
            }
        }
        return (classIdx, maxConf);
    }

    private float Sigmoid(float value)
    {
        var k = (float)Math.Exp(value);

        return k / (1.0f + k);
    }

    private string GetClassName(int classIndex)
    {
        string[] classNames = { "AL loaf", "AL rod", "MELD tool", "actuator", "base plate", "control knob", "emergency button" };
        if (classIndex >= 0 && classIndex < classNames.Length)
        {
            // Return the corresponding class name
            return classNames[classIndex];
        }
        else
        {
            // Return an error message if the class index is invalid
            return "Invalid class index";
        }
    }
   /* private IList<BoundingBox> FilterBoundingBoxes(IList<BoundingBox> boxes, int limit, float threshold)
    {
        var activeCount = boxes.Count;
        var isActiveBoxes = new bool[boxes.Count];

        for (int i = 0; i < isActiveBoxes.Length; i++)
        {
            isActiveBoxes[i] = true;
        }

        var sortedBoxes = boxes.Select((b, i) => new { Box = b, Index = i })
                .OrderByDescending(b => b.Box.Confidence)
                .ToList();

        var results = new List<BoundingBox>();

        for (int i = 0; i < boxes.Count; i++)
        {
            if (isActiveBoxes[i])
            {
                var boxA = sortedBoxes[i].Box;
                results.Add(boxA);

                if (results.Count >= limit)
                    break;

                for (var j = i + 1; j < boxes.Count; j++)
                {
                    if (isActiveBoxes[j])
                    {
                        var boxB = sortedBoxes[j].Box;

                        if (IntersectionOverUnion(boxA.UnityEngine.Rect, boxB.UnityEngine.Rect) > threshold)
                        {
                            isActiveBoxes[j] = false;
                            activeCount--;

                            if (activeCount <= 0)
                                break;
                        }
                    }
                }

                if (activeCount <= 0)
                    break;
            }
        }
        return results;
    }

    private float IntersectionOverUnion(UnityEngine.Rect boundingBoxA, UnityEngine.Rect boundingBoxB)
    {
        var areaA = boundingBoxA.width * boundingBoxA.height;

        if (areaA <= 0)
            return 0;

        var areaB = boundingBoxB.width * boundingBoxB.height;

        if (areaB <= 0)
            return 0;

        var minX = Math.Max(boundingBoxA.xMin, boundingBoxB.xMin);
        var minY = Math.Max(boundingBoxA.yMin, boundingBoxB.yMin);
        var maxX = Math.Min(boundingBoxA.xMax, boundingBoxB.xMax);
        var maxY = Math.Min(boundingBoxA.yMax, boundingBoxB.yMax);

        var intersectionArea = Math.Max(maxY - minY, 0) * Math.Max(maxX - minX, 0);

        return intersectionArea / (areaA + areaB - intersectionArea);
    }*/
}