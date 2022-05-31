using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    #region Singleton

    public static UIManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    #endregion

    [SerializeField] GameObject startCanvas;

    [SerializeField] TextMeshProUGUI startLevelText;

    [SerializeField] TextMeshProUGUI scoreText;

    private void Start()
    {
        SetStartLevelText();

        UpdateScoreText();

        EventManager.instance.startEvent += () => startCanvas.SetActive(false);

        EventManager.instance.successEvent += () => IncreaseLevelText();
    }

    private void SetStartLevelText() => startLevelText.text = "Level " + PlayerPrefs.GetInt("Level", 1);

    private void UpdateScoreText(int score = 0) => scoreText.text = (PlayerPrefs.GetInt("Score", 0) + score).ToString();

    private void IncreaseLevelText() => PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);
}
