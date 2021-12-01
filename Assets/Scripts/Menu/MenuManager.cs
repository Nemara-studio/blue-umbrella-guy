using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public Button newGameButton;
    public Button continueButton;

    private void Start()
    {
        ResetUI();
    }

    private void ResetUI()
    {
        newGameButton.interactable = true;

        if (!LevelManager.singleton.CheckSavedData())
        {
            continueButton.interactable = false;
        }
    }

    public void StartNewGame()
    {
        LevelManager.singleton.currentLevel = 0;
        LevelManager.singleton.SaveCurrentLevel();
        SceneChanger.singleton.LoadScene(LevelManager.singleton.GetLevelScene());
    }

    public void ResumePlay()
    {
        if (!LevelManager.singleton.CheckSavedData()) return;

        LevelManager.singleton.Load();
        SceneChanger.singleton.LoadScene(LevelManager.singleton.GetLevelScene());
    }
}
