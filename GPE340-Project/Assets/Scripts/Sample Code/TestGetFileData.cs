using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGetFileData : MonoBehaviour {

    private void Awake() {
        SaveData mySave = new SaveData();
        mySave.playerName = "Swoofz";
        mySave.currentLevel = 20;
        mySave.experience = 20000f;
        mySave.Save("Save 1");

        SaveData loadedSave = SaveData.Load("Save 1");
        Debug.Log(loadedSave.playerName);
    }

}
