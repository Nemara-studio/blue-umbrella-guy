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

    public void Play()
    {
        SceneChanger.singleton.LoadScene(LevelManager.singleton.GetLevelScene());
    }
}
