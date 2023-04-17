using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public long lastUpdated;
    public string profileID;
    public string userPin;

    //Example showing how a serializable dictionary could be implemented for future use
    //public SerializableDictionary<string, bool> coinsCollected;

    //put any default values here, i.e. tutorial_counter = 0
    public GameData(string profileID, string userPin) 
    {
        this.profileID = profileID;
        this.userPin = userPin;
    }
}
