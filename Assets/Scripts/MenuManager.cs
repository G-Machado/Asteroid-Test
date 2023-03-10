using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public static MenuManager instance;
    private void Awake()
    {
        instance = this;
    }

    public GameObject StartPanel, GameOverPanel, GameplayPanel;

    public Text[] currentScoreText;
    public Text[] highestScoreText;

    void Start()
    {
        StartPanel.SetActive(true);
        GameOverPanel.SetActive(false);
        GameplayPanel.SetActive(false);
    }

    public void StartGame()
    {
        StartPanel.SetActive(false);
        GameOverPanel.SetActive(false);
        GameplayPanel.SetActive(true);
    }

    public void GameOver()
    {
        GameplayPanel.SetActive(false);
        GameOverPanel.SetActive(true);
    }

    public void UpdateScoreTexts(int score)
    {
        for (int i = 0; i < currentScoreText.Length; i++)
        {
            currentScoreText[i].text = score.ToString();
        }
    }

    public void UpdateHighscoreTexts(int score)
    {
        for (int i = 0; i < highestScoreText.Length; i++)
        {
            highestScoreText[i].text = score.ToString();
        }
    }
}
