using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveLoadData : MonoBehaviour
{
    // Start is called before the first frame update
    public void Save()
    {
        if (!Directory.Exists(Application.dataPath + "SavedGames"))
        {
            Directory.CreateDirectory(Application.dataPath + "/SavedGames");
        }

        BinaryFormatter bf = new BinaryFormatter();

        FileStream fs = File.Create(Application.dataPath + "/SavedGames/save.rd");

        SaveManager saveManager = new SaveManager();

        saveManager.currentLevel = GameManager.gm.GetCurrentLevel();

        bf.Serialize(fs, saveManager);

        Debug.Log("Game saved");

        fs.Close();
    }
}

[System.Serializable]
class SaveManager
{
    public int currentLevel;
}