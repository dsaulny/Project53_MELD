using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DetectHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public TMP_Text className;
    public GameObject mtImage;
    public Button classButton;
    void Start()
    {
        Button tempButton = classButton.GetComponent<Button>();
        tempButton.onClick.AddListener(detected);

    }
    public void detected()
    {
        string name = className.text;
        switch (name)
        {
            case "AL loaf":
                UnityEngine.Debug.Log($"AL loaf description here");
                break;
            case "actuator":
                UnityEngine.Debug.Log($"actuator description here");
                break;
            case "control knob":
                UnityEngine.Debug.Log($"control knob description here");
                break;
            case "emergency button":
                UnityEngine.Debug.Log($"Meld Tool description here");
                if (mtImage.activeInHierarchy == true)
                {
                    mtImage.SetActive(false);
                }
                else
                {
                    mtImage.SetActive(true);
                }

                break;
        }
    }
}
