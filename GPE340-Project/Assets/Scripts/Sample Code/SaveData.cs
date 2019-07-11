using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System.IO;

[System.Serializable]
public class SaveData {

    public string playerName = null;
    public int currentLevel = 0;
    public float experience = 0;
    // Etc....

    public void Save(string filename) {
        using (FileStream stream = new FileStream(string.Format("{0}/{1}.save", Application.persistentDataPath, filename), FileMode.Create)) {
            IFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, this);
        }
    }

    public static SaveData Load(string filename) {
        using (FileStream stream = new FileStream(string.Format("{0}/{1}.save", Application.persistentDataPath, filename), FileMode.Open, FileAccess.Read)) {
            IFormatter formatter = new BinaryFormatter();
            return formatter.Deserialize(stream) as SaveData;
        }
    }
}
