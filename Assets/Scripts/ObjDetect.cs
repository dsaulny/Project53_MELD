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
    bool camStarted = false;
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

        if (Time.frameCount % 200 == 0)
        {
            detect();
        }

    }
    public void detect()
    {
        if (objToggle.isOn != false)
        {
            
            if (!camStarted)
            {
                camTexture.Play();
                camStarted = true;
            }

            Texture2D tex = convertCamTo2d(camTexture);
            Texture2D resizedTex = ResizeTexture(tex, 640, 640);



            Tensor inputTensor = new Tensor(resizedTex, channels: 3);

            worker.Execute(inputTensor);
            Tensor outputTensor = worker.PeekOutput(outputLayerName);
            var indexWithHighestProbability = outputTensor.ArgMax()[0];
            ParseYoloV5Output(outputTensor, detectionThreshold);
            inputTensor.Dispose();   
            outputTensor.Dispose();
           

        }
        else
        {
            camStarted = false;
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
        RenderTexture renderTexture = RenderTexture.GetTemporary(width, height, 24);
        //RenderTexture renderTexture = new RenderTexture(width, height, 24);
        RenderTexture.active = renderTexture;
        Graphics.Blit(texture, renderTexture);
        Texture2D resizedTexture = new Texture2D(width, height);
        resizedTexture.ReadPixels(new UnityEngine.Rect(0, 0, width, height), 0, 0);
        resizedTexture.Apply();
        RenderTexture.active = null;
        RenderTexture.ReleaseTemporary(renderTexture);
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
            if (className != null)
            {
                outputClass.text = className;
                ChangeToggle();
            }
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
   
}