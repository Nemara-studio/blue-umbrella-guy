using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager singleton;

    private string levelDataName = "Level";
    [HideInInspector] public int currentLevel;
    public List<string> levelScene;

    private void Awake()
    {
        if (singleton == null)
        {
            singleton = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        currentLevel = 0;
        Load();
    }

    public bool CheckSavedData()
    {
        return PlayerPrefs.HasKey(levelDataName);
    }

    public string GetLevelScene()
    {
        return levelScene[currentLevel];
    }

    public bool IsNextLevelAvailable()
    {
        if (currentLevel + 1 < levelScene.Count)
        {
            return true;
        }

        return false;
    }

    public void SaveCurrentLevel()
    {
        PlayerPrefs.SetInt(levelDataName, currentLevel);
    }

    public void Load()
    {
        if (!PlayerPrefs.HasKey(levelDataName)) return;

        currentLevel = PlayerPrefs.GetInt(levelDataName);
    }
}
