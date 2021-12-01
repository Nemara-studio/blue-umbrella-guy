using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager singleton;

    public bool isTest = false;
    public bool isPaused = false;

    public GameObject losePanel;
    public GameObject endPanel;

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

    private void Update()
    {
        if (isTest)
        {
            TestInput();
        }
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

        Time.timeScale = 0;
        isPaused = true;

        if (LevelManager.singleton.IsNextLevelAvailable())
        {
            LevelManager.singleton.currentLevel += 1;
            LevelManager.singleton.SaveCurrentLevel();
            SceneChanger.singleton.LoadScene(LevelManager.singleton.GetLevelScene());
        }
        else
        {
            endPanel.SetActive(true);
        }
    }

    public void BackToMenu()
    {
        SceneChanger.singleton.LoadScene("MenuScene");
    }

    public void RestartGame()
    {
        SceneChanger.singleton.LoadScene(gameObject.scene.name, () => Time.timeScale = 1);
    }

    private void TestInput()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Win();
        }
    }
}
