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

    public void updateSession(GameSession session, DateTime dateTime)
    {
        GameSession saveSession = gameSessions.Find(s => s == session);

        if (saveSession == null)
        {
            gameSessions.Add(session);
            saveSession = session;
        }
        saveSession.lastDatePlayed = dateTime;

    }

    public GameData clone()
    {
        return new GameData(gameSessions.ConvertAll(s => new GameSession(s.level, s.lastDatePlayed)));
    }
}

[System.Serializable]
public class GameSession
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
    private GameData _gameData;
    private GameSession _activeSession;

    void Awake() {
        if (instance == null)  {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        } else {
            Destroy(this.gameObject);
        }
        Load();

        GlobalEventManager.onLoadScene.AddListener((sceneName, session) => {
            if (sceneName == "Game Scene") {
                _activeSession = session == null ? new GameSession(0, DateTime.Now) : session;
            } else {
                Save();
                _activeSession = null;
            }
        });
    }

    public List<GameSession> getSavedSessions() {
        return _gameData.gameSessions;
    }

    public int getLevel() {
        return _gameData.getLastSession()?.level ?? 0;
    }

    public GameSession? getLastSession() {
        return _gameData.getLastSession();
    }

    public void patchSession(int level) {
        if (_activeSession == null) {
            return;
        }

        _activeSession.level = level;
    }

    public void Save() {
        if (_activeSession == null) {
            return;
        }

        _gameData.updateSession(_activeSession, DateTime.Now);
        //Create a copy of save data
        GameData data = _gameData.clone();
        SaveProcess(data);
    }

    void SaveProcess(GameData data) {
        //Create a binary formatter which can read binary files
        BinaryFormatter formatter = new BinaryFormatter();

        //Create a route from the program to the file
        Debug.Log(Application.persistentDataPath);
        FileStream file = File.Create(Application.persistentDataPath + "/player.dat");


        //Actually save the data in the file
        formatter.Serialize(file, data);

        //Close the data stream
        file.Close();
    }

    public void Load() {
        //Check if the save game file exists
        if (File.Exists(Application.persistentDataPath + "/player.dat")) {
            //Create a Binary Formatter
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/player.dat", FileMode.Open);
            file.Position = 0;
            if (file.Length > 0) {
                _gameData = formatter.Deserialize(file) as GameData;
                GameSession session = _gameData.getLastSession();

                // _activeSession = session;
                file.Close();
                return;
            }
            
        }

        _gameData = new GameData(new List<GameSession>());
        // _activeSession = new GameSession(0, DateTime.Now);
    }

    void OnApplicationQuit() {
        Save();
    }

    void OnDisable() {
        Save();
    }
}
