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

    public void RestartGame()
    {
        
    }
}
