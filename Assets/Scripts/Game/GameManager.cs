using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager singleton;

    public bool isPaused = false;

    public GameObject losePanel;

    private void Awake()
    {
        if (singleton == null)
        {
            singleton = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // TODO: start level
    }

    public void Lose()
    {
        Time.timeScale = 0;
        isPaused = true;

        losePanel.SetActive(true);
    }

    public void Win()
    {
        Debug.Log("Win");

        if (LevelManager.singleton.IsNextLevelAvailable())
        {
            LevelManager.singleton.currentLevel += 1;
            LevelManager.singleton.SaveCurrentLevel();
            SceneChanger.singleton.LoadScene(LevelManager.singleton.GetLevelScene());
        }
        else
        {
            SceneChanger.singleton.LoadScene("MenuScene");
        }
    }

    public void RestartGame()
    {
        SceneChanger.singleton.LoadScene(LevelManager.singleton.GetLevelScene(), () => Time.timeScale = 1);
    }
}
