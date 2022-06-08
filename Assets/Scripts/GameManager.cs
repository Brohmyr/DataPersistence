using System.IO;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Singleton

    public static GameManager Instance;

    private void Awake()
    {
        if (Instance != null) 
            Destroy(gameObject);

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    #endregion

    [SerializeField] private TMP_Text _name;
    public string _playerName;
    public string _bestPlayerName;
    public int _bestScore;
    
    public void SavePlayerName() => _playerName = _name.text;

    public string GetPlayerName() => _playerName;

    public void StartGame()
    {
        SavePlayerName();
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
        #if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
        #else
        Application.Quit();
        #endif
    }

    [System.Serializable]
    class SaveData
    {
        public string bestPlayerName;
        public int bestScore;
    }

    public void SaveBestScore()
    {
        SaveData data = new SaveData();
        data.bestPlayerName = _bestPlayerName;
        data.bestScore = _bestScore;

        string json = JsonUtility.ToJson(data);
        
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadBestScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            _bestPlayerName = data.bestPlayerName;
            _bestScore = data.bestScore;
        }
    }
}
