using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using OpenCvSharp;
using Unity.Barracuda;

/*
public class ObjDetect : MonoBehaviour
{
    // Start is called before the first frame update
    WebCamTexture camTexture;
    [SerializeField]
    private NNModel yoloModel;

    private Model runtimeModel;
    private IWorker worker;
    private string outputLayerName;
    public Toggle objToggle;
    public float detectionThreshold = 0.5f;
    public RawImage cameraView;
    private GameObject lastBB;
    private GameObject lastT;


    void Start()
    {

        objToggle.isOn = false;
        WebCamDevice[] devices = WebCamTexture.devices;
        camTexture = new WebCamTexture(devices[0].name);
        runtimeModel = ModelLoader.Load(yoloModel);
        worker = WorkerFactory.CreateWorker(WorkerFactory.Type.Auto, runtimeModel);
        outputLayerName = runtimeModel.outputs[runtimeModel.outputs.Count - 1];
        cameraView.texture = camTexture;
    }

    // Update is called once per frame
    void Update()
    {


        detect();


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
    public Mat convert2dToMat(Texture2D texture)
    {
        byte[] data = texture.EncodeToPNG();
        Mat mat = Cv2.ImDecode(data, ImreadModes.Color);
        return mat;
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
                //UnityEngine.Debug.Log($"Image was recognised as class number: {indexWithHighestProbability}");
                var outpArr = outputTensor.ToReadOnlyArray();
                for (int i = 0; i < 17; i++)
                {
                    UnityEngine.Debug.Log($"{outpArr[22*22 + i]}");
                }
                //ProcessOutput(outputTensor, resizedTex);
                outputTensor.Dispose();
            }   

        }
        else
        {
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
    /*void ProcessOutput(Tensor outputTensor, Texture2D inputTexture)
    {
        // Extract the raw output data from the tensor
        var outputArray = outputTensor.ToReadOnlyArray();

        var outputShape = outputTensor.shape;
        var outputHeight = outputShape[5];
        var outputWidth = outputShape[6];

        // Loop over the output tensor and draw bounding boxes around the detected objects
        for (int y = 0; y < outputHeight; y++)
        {
            for (int x = 0; x < outputWidth; x++)
            {
                int index = y * outputWidth + x;

                // Extract the class probabilities and bounding box coordinates from the output tensor
                float[] classProbabilities = new float[17]; //numclasses
                for (int c = 0; c < 17; c++)
                {
                    classProbabilities[c] = outputArray[index + c * outputHeight * outputWidth];

                    //UnityEngine.Debug.Log($"prob: {classProbabilities[c]}");
                }

                float xCenter = outputArray[index + 17 * outputHeight * outputWidth];
                float yCenter = outputArray[index + 18 * outputHeight * outputWidth];
                float width = outputArray[index + 19 * outputHeight * outputWidth];
                float height = outputArray[index + 20 * outputHeight * outputWidth];

                // Decode the bounding box coordinates from the YOLOv5 output
                float xMin = (xCenter - width / 2);//* inputTexture.width;
                float yMin = (yCenter - height / 2);//* inputTexture.height;
                float xMax = (xCenter + width / 2);//* inputTexture.width;
                float yMax = (yCenter + height / 2);//* inputTexture.height;


                // Find the class with the highest probability
                float maxProbability = 0f;
                int maxClass = 0;
                for (int c = 0; c < 17; c++)
                {
                    if (classProbabilities[c] > maxProbability)
                    {
                        maxProbability = classProbabilities[c];
                        maxClass = c;
                    }
                }
                UnityEngine.Debug.Log($"x: {xMin}, y: {yMin}, width: {xMax}, height: {yMax}");
                // Draw a bounding box around the detected object
                if (maxProbability > detectionThreshold) // Only show objects with probability higher than 0.5
                {
                    UnityEngine.Rect boundingBox = new UnityEngine.Rect(xMin, yMin, xMax - xMin, yMax - yMin);
                    string className = GetClassName(maxClass); // Helper function to get the name of the class
                    DrawBoundingBox(xMin,yMin,xMax,yMax);
                    UnityEngine.Debug.Log($"Image was recognised as {className}");
                }
            }
        }


    }
    /*private void DrawBoundingBox(UnityEngine.Rect bbox, string className, float confidence)
    {
        // Calculate the bounding box position and size relative to the input texture
        if (lastBB != null)
        {
            Destroy(lastBB);
            
        }
        if(lastT != null)
        {
            Destroy(lastT);
        }
        var x = bbox.x;
        var y = (1 - bbox.y - bbox.height);
        var width = bbox.width;
        var height = bbox.height;

        // Create a new game object for the bounding box


        // Create a new UI image component for the bounding box
        var boundingBoxGO = new GameObject("BoundingBox");
        lastBB = boundingBoxGO;
        boundingBoxGO.transform.SetParent(transform, false);
        var bboxRect = boundingBoxGO.AddComponent<RectTransform>();
        //bboxRect.color = Color.yellow;


        // Create a new UI text component for the class name and confidence
        var bboxTextGO = new GameObject();
        lastT = bboxTextGO;
        bboxTextGO.transform.SetParent(boundingBoxGO.transform, false);
        var bboxText = bboxTextGO.AddComponent<Text>();
        bboxText.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
        bboxText.fontSize = 16;
        bboxText.alignment = TextAnchor.MiddleCenter;
        bboxText.color = Color.white;
        bboxText.text = $"{className} ({confidence}%)";

       
        bboxRect.anchoredPosition = new Vector2(x + width / 2, y + height / 2);
        bboxRect.sizeDelta = new Vector2(width, height);

        
        //UnityEngine.Debug.Log($"x: {x}, y: {y}, width: {width}, height: {height}");
    }
    private string GetClassName(int classIndex)
    {
        // Map class indices to class names
        //string[] classNames = { "person", "bicycle", "car", "motorcycle", "airplane", "bus", "train", "truck", "boat", "traffic light", "fire hydrant", "stop sign", "parking meter", "bench", "bird", "cat", "dog", "horse", "sheep", "cow", "elephant", "bear", "zebra", "giraffe", "backpack", "umbrella", "handbag", "tie", "suitcase", "frisbee", "skis", "snowboard", "sports ball", "kite", "baseball bat", "baseball glove", "skateboard", "surfboard", "tennis racket", "bottle", "wine glass", "cup", "fork", "knife", "spoon", "bowl", "banana", "apple", "sandwich", "orange", "broccoli", "carrot", "hot dog", "pizza", "donut", "cake", "chair", "couch", "potted plant", "bed", "dining table", "toilet", "tv", "laptop", "mouse", "remote", "keyboard", "cell phone", "microwave", "oven", "toaster", "sink", "refrigerator", "book", "clock", "vase", "scissors", "teddy bear", "hair drier", "toothbrush" };
        string[] classNames = { "AL loaf", "AL rod", "MELD door", "MELD interface", "MELD machine", "MELD tool", "actuator", "base plate", "buttons", "control knob", "emergency button", "interface screen", "power button", "pressure gauge", "temp switch", "thermometer", "wrench" };
        // Check if the class index is within the valid range
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
    /*private void ProcessOutput(Tensor outputTensor, Texture2D texture)
    {
        // Retrieve output data from the tensor
        var outputData = outputTensor.ToReadOnlyArray();

        // Get the width and height of the texture
        int textureWidth = texture.width;
        int textureHeight = texture.height;

        // Get the number of detections in the output
        int detectionCount = outputData.Length / 22;

        // Loop through each detection
        for (int i = 0; i < detectionCount; i++)
        {
            // Get the detection data
            float x = outputData[i * 22 + 3] * textureWidth;
            float y = outputData[i * 22 + 4] * textureHeight;
            float w = outputData[i * 22 + 5] * textureWidth;
            float h = outputData[i * 22 + 6] * textureHeight;
            float confidence = outputData[i * 22 + 2];
            int classIndex = (int)outputData[i * 22 + 1];

            // Check if the detection exceeds the threshold
            if (confidence > detectionThreshold)
            {
                // Calculate the coordinates of the bounding box
                float left = x - w / 2;
                float top = y - h / 2;
                float right = x + w / 2;
                float bottom = y + h / 2;

                // Create a rectangle object with the coordinates of the bounding box
                UnityEngine.Rect rect = new UnityEngine.Rect(left, top, w, h);

                // Create a color for the bounding box based on the class of the object
                Color color = Color.HSVToRGB((float)classIndex / 17, 1f, 1f);

                // Draw the bounding box on the texture
                DrawRectangleOnTexture(texture, rect, color);

                // Display the class name and confidence score above the bounding box
                var classname = GetClassName(classIndex);
                UnityEngine.Debug.Log($"class: {classname}");
                //DisplayTextOnTexture(texture, y, left, $"{classNames[classIndex]}: {confidence.ToString("0.00")}", color, 16);
            }
        }



        // Update the camera view with the modified texture
        cameraView.texture = texture;
    }
    private Texture2D DrawRectangleOnTexture(Texture2D texture, UnityEngine.Rect rect, Color color)
    {
        int x = (int)rect.x;
        int y = (int)rect.y;
        int width = (int)rect.width;
        int height = (int)rect.height;

        Texture2D newTexture = new Texture2D(texture.width, texture.height);
        newTexture.SetPixels(texture.GetPixels());

        for (int i = x; i < x + width; i++)
        {
            newTexture.SetPixel(i, y, color);
            newTexture.SetPixel(i, y + height, color);
        }

        for (int i = y; i < y + height; i++)
        {
            newTexture.SetPixel(x, i, color);
            newTexture.SetPixel(x + width, i, color);
        }

        newTexture.Apply();

        return newTexture;
    }
    private void DrawBoundingBox(float xMin, float yMin, float xMax, float yMax)
    {
        // Calculate the screen coordinates of the bounding box corners
        Vector2 bottomLeft = new Vector2(xMin, yMin);
        Vector2 topRight = new Vector2(xMax , yMax );

        // Calculate the dimensions of the bounding box
        float width = topRight.x - bottomLeft.x;
        float height = topRight.y - bottomLeft.y;

        // Create a new bounding box object if one doesn't exist
        if (lastBB == null)
        {
            lastBB = new GameObject();
            lastBB.name = "BoundingBox";
            lastBB.transform.SetParent(cameraView.transform);
            lastBB.AddComponent<RectTransform>();
            lastBB.AddComponent<Image>();
            lastBB.GetComponent<Image>().color = Color.red;
        }

        // Set the position and size of the bounding box
        lastBB.GetComponent<RectTransform>().anchoredPosition = bottomLeft;
        lastBB.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
    }

    void ProcessOutput(Tensor outputTensor, Texture2D inputTexture)
    {
        // Extract the raw output data from the tensor
        var outputArray = outputTensor.ToReadOnlyArray();

        var outputShape = outputTensor.shape;
        var outputHeight = outputShape[5];
        var outputWidth = outputShape[6];
        
        // Loop over the output tensor and draw bounding boxes around the detected objects
        for (int y = 0; y < outputHeight; y++)
        {
            for (int x = 0; x < outputWidth; x++)
            {
                int index = y * outputWidth + x;
                UnityEngine.Debug.Log($"width: {outputWidth}, height: {outputHeight}");
                // Extract the class probabilities and bounding box coordinates from the output tensor
                float[] classProbabilities = new float[17]; //numclasses
                for (int c = 0; c < 17; c++)
                {
                    classProbabilities[c] = outputArray[index + c * outputHeight * outputWidth];

                    //UnityEngine.Debug.Log($"prob: {classProbabilities[c]}");
                }

                float xCenter = outputArray[index + 17 * outputHeight * outputWidth];
                float yCenter = outputArray[index + 18 * outputHeight * outputWidth];
                float width = outputArray[index + 19 * outputHeight * outputWidth];
                float height = outputArray[index + 20 * outputHeight * outputWidth];

                // Decode the bounding box coordinates from the YOLOv5 output
                float xMin = (xCenter - width / 2);//* inputTexture.width;
                float yMin = (yCenter - height / 2);//* inputTexture.height;
                float xMax = (xCenter + width / 2);//* inputTexture.width;
                float yMax = (yCenter + height / 2);//* inputTexture.height;


                // Find the class with the highest probability
                float maxProbability = 0f;
                int maxClass = 0;
                for (int c = 0; c < 17; c++)
                {
                    if (classProbabilities[c] > maxProbability)
                    {
                        maxProbability = classProbabilities[c];
                        maxClass = c;
                    }
                }
                //UnityEngine.Debug.Log($"x: {xMin}, y: {yMin}, width: {xMax}, height: {yMax}");
                // Draw a bounding box around the detected object
                if (maxProbability > detectionThreshold) // Only show objects with probability higher than 0.5
                {
                    UnityEngine.Rect boundingBox = new UnityEngine.Rect(xMin, yMin, xMax - xMin, yMax - yMin);
                    string className = GetClassName(maxClass); // Helper function to get the name of the class
                    DrawBoundingBox(xMin, yMin, xMax, yMax);
                    UnityEngine.Debug.Log($"Image was recognised as {className}");
                }
            }
        }


    }
}*/
