using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NewProfileScripts : MonoBehaviour
{
    public InputField PinInputField;
    public InputField NameInputField;

    public void CreateProfileButton()
    {
        //DataPersistenceManager.instance.UpdateUserPin(PinInputField.text);
        DataPersistenceManager.instance.UpdateSelectedProfileId(NameInputField.text);
        DataPersistenceManager.instance.NewGame();
        SaveGameAndLoadScene();
    }

    //Load Scene is a placeholder rn
    private void SaveGameAndLoadScene() 
    {
        // save the game anytime before loading a new scene
        DataPersistenceManager.instance.SaveGame();
        // load the scene
        SceneManager.LoadSceneAsync("SampleScene");
    }
}