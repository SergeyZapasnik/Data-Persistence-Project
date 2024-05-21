using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataManager : MonoBehaviour
{
    /// Start() and Update() methods deleted - we don't need them right now

    public static DataManager Instance;

    public string PlayerName { get; set; }
    public string CurrentPlayerName { get; set; }
    public int PlayerScore { get; set; }
    public int CurrentPlayerScore { get; set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadUserData();
    }

    [System.Serializable]
    class SaveData
    {
        public int playerScore;
        public string playerName;
    }

    public void SaveUserData()
    {
        int bestScore = GetBestScore();
        if (bestScore < CurrentPlayerScore)
        {
            SaveData data = new SaveData();
            data.playerScore = CurrentPlayerScore;
            data.playerName = CurrentPlayerName;

            string json = JsonUtility.ToJson(data);

            File.WriteAllText(Application.persistentDataPath + "/player_data_file.json", json);
        }
    }

    public void LoadUserData()
    {
        string path = Application.persistentDataPath + "/player_data_file.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            PlayerName = data.playerName;
            PlayerScore = data.playerScore;
        }
        Debug.Log(PlayerScore);
        Debug.Log(PlayerName);
    }

    public int GetBestScore()
    {
        string path = Application.persistentDataPath + "/player_data_file.json";
        if (File.Exists(path))
        {
            try
            {
                string json = File.ReadAllText(path);
                SaveData data = JsonUtility.FromJson<SaveData>(json);
                return data.playerScore;
            }
            catch (System.Exception ex)
            {
                Debug.LogError("Failed to read user data: " + ex.Message);
                return 0;
            }
        }
        else
        {
            Debug.LogWarning("Save file not found at: " + path);
            return 0;
        }
    }
}
