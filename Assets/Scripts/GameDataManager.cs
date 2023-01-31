using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[System.Serializable]
class GameData
{
    public int level;
}

public class GameDataManager : MonoBehaviour
{

    public static GameDataManager instance;
    private static GameData gameData;

    void Awake() {
        if (instance == null)  {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        } else {
            Destroy(this.gameObject);
        }
        Load();
    }

    public int getLevel() {
        return gameData.level;
    }

    public void Save()
    {
        //Create a binary formatter which can read binary files
        BinaryFormatter formatter = new BinaryFormatter();

        //Create a route from the program to the file
        FileStream file = File.Create(Application.persistentDataPath + "/player.dat");

        //Create a copy of save data
        GameData data = new GameData();
        data = gameData;

        //Actually save the data in the file
        formatter.Serialize(file, data);

        //Close the data stream
        file.Close();
    }

    public void Load()
    {
        //Check if the save game file exists
        if (File.Exists(Application.persistentDataPath + "/player.dat"))
        {
            //Create a Binary Formatter
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/player.dat", FileMode.Open);
            file.Position = 0;
            if (file.Length > 0) {
                gameData = formatter.Deserialize(file) as GameData;
                file.Close();
                return;
            }
            
        }

        gameData = new GameData();
        gameData.level = 0;
    }

    void OnApplicationQuit()
    {
        Save();
    }

    void OnDisable()
    {
        Save();
    }
}
