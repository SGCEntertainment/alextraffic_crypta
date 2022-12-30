using System;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private int score ;

    private GameObject _last = null;

    [SerializeField] GameObject getStarted;
    [SerializeField] GameObject menu;
    [SerializeField] GameObject game;
    [SerializeField] GameObject result;
    [SerializeField] GameObject levels;

    [Space(10)]
    [SerializeField] GameObject settings;

    [Space(10)]
    [SerializeField] Text scoreText;
    [SerializeField] Text levelText;

    [Space(10)]
    [SerializeField] GameObject lose;
    [SerializeField] GameObject win;

    [Space(10)]
    [SerializeField] LevelManager levelManager;

    public static Action<bool> OnGameEnd { get; set; }

    private void Awake()
    {
        OpenWindow(0);

        Ball.OnCollided += () =>
        {
            if (!GameManager.GameStarted)
            {
                return;
            }

            scoreText.text = $"{++score}/{LevelManager.currentKicks}";
            if(score >= LevelManager.currentKicks)
            {
                ShowResult(true);
            }
        };
    }

    public void OpenMenu()
    {
        OpenWindow(1);
        game.SetActive(false);

        GameManager.Instance.DestroyOld();
    }

    public void StartGameOnClick()
    {
        score = 0;

        scoreText.text = $"{score}/{LevelManager.currentKicks}";
        levelText.text = $"Level {LevelManager.levelIndex + 1}";

        GameManager.Instance.StartGame();
        OpenWindow(2);
    }

    public void OpenWindow(int windowIndex)
    {
        if(_last && windowIndex != 4)
        {
            _last.SetActive(false);
        }

        switch(windowIndex)
        {
            case 0: _last = getStarted; break;
            case 1: _last = menu; break;
            case 2: _last = game; break;
            case 3: _last = levels; break;
            case 4: _last = result; break;
        }

        _last.SetActive(true);
        if(windowIndex == 1)
        {
            FindObjectOfType<RewardManager>().Check();
        }
    }

    public void ShowOptions(bool IsOpen)
    {
        settings.SetActive(IsOpen);
    }

    public void ShowResult(bool IsWin)
    {
        win.SetActive(false);
        lose.SetActive(false);

        if(IsWin)
        {
            win.SetActive(true);
        }
        else
        {
            lose.SetActive(true);
        }

        GameManager.GameStarted = false;
    }

    public void StartNextLevel()
    {
        levelManager.NextLevel();
        StartGameOnClick();
    }
}