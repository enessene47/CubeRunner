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

    [SerializeField] GameObject startButton;
    [SerializeField] GameObject retryButton;
    [SerializeField] GameObject nextButton;

    [SerializeField] TextMeshProUGUI mainLevelText;

    [SerializeField] TextMeshProUGUI coinText;

    private void Start()
    {
        SetStartLevelText();

        UpdateCoinText();

        EventManager.instance.startEvent += () => startButton.SetActive(false);

        EventManager.instance.failEvent += () => retryButton.SetActive(true);

        EventManager.instance.successEvent += () => nextButton.SetActive(true);

        EventManager.instance.nextEvent += () => IncreaseLevelText();
    }

    private void SetStartLevelText() => mainLevelText.text = "Level " + PlayerPrefs.GetInt("Level", 1);

    public void UpdateCoinText(int score = 0)
    {
        int coin = PlayerPrefs.GetInt("Coin", 0) + score;

        PlayerPrefs.SetInt("Coin", coin);

        coinText.text = coin.ToString();
    }

    private void IncreaseLevelText() => PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);
}
