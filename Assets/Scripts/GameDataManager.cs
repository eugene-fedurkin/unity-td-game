using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[System.Serializable]
class GameData
{
    public List<GameSession> gameSessions;

    public GameData(List<GameSession> sessions) {
        gameSessions = sessions;
    }

    public GameSession getLastSession() {
        if (gameSessions.Count > 0) {
            GameSession session = gameSessions[0];
            gameSessions.ForEach(s => {
                if (session.lastDatePlayed < s.lastDatePlayed) {
                    session = s;
                }
            });

            return session;
        }

        return null;
    }

    public GameSession addSession() {
        GameSession newSession = new GameSession(0, DateTime.Now);
        gameSessions.Add(newSession);

        return newSession;
    }

    public GameData clone()
    {
        return new GameData(gameSessions.ConvertAll(s => new GameSession(s.level, s.lastDatePlayed)));
    }
}

[System.Serializable]
class GameSession
{
    public int level;
    public DateTime lastDatePlayed;

    public GameSession(int currLevel, DateTime date)
    {
        level = currLevel;
        lastDatePlayed = date;
    }
}

public class GameDataManager : MonoBehaviour
{

    public static GameDataManager instance;
    private static GameData _gameData;
    private GameSession _activeSession;
    private bool sessionIsNotSaved = true;
    private bool saveSessionIsAvalible = false;

    void Awake() {
        if (instance == null)  {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        } else {
            Destroy(this.gameObject);
        }
        Load();

        GlobalEventManager.onLoadScene.AddListener((sceneName) => {
            if (sceneName == "Game Scene") {
                saveSessionIsAvalible = true;
            }
        });
    }

    public int getLevel() {
        return _activeSession.level;
    }

    public DateTime? getLastSavedSessionDate() {
        return sessionIsNotSaved ? null : _activeSession.lastDatePlayed;
    }

    public void Save()
    {
        if (!saveSessionIsAvalible) {
            return;
        }

        _activeSession.lastDatePlayed = DateTime.Now;

        //Create a copy of save data
        GameData data = _gameData.clone();
        SaveProcess(data);
    }

    void SaveProcess(GameData data)
    {
        //Create a binary formatter which can read binary files
        BinaryFormatter formatter = new BinaryFormatter();

        //Create a route from the program to the file
        Debug.Log(Application.persistentDataPath);
        FileStream file = File.Create(Application.persistentDataPath + "/player.dat");


        //Actually save the data in the file
        formatter.Serialize(file, data);

        sessionIsNotSaved = false;

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
                _gameData = formatter.Deserialize(file) as GameData;
                GameSession session = _gameData.getLastSession();

                if (session == null)
                {
                    session = _gameData.addSession();
                    sessionIsNotSaved = true;
                } else
                {
                    sessionIsNotSaved = false;
                }

                _activeSession = session;
                file.Close();
                return;
            }
            
        }

        _gameData = new GameData(new List<GameSession>());
        _activeSession = _gameData.addSession();
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
