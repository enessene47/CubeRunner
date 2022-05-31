using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

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

    [SerializeField] Image powerProcessImage;
    [SerializeField] Button powerProcessButton;

    private void Start()
    {
        SetStartLevelText();

        UpdateCoinText();

        EventManager.instance.startEvent += () => startButton.SetActive(false);

        EventManager.instance.failEvent += () => retryButton.SetActive(true);

        EventManager.instance.successEvent += () =>
        {
            nextButton.SetActive(true);

            powerProcessButton.interactable = false;
        };

        EventManager.instance.nextEvent += () => IncreaseLevelText();
    }

    private void SetStartLevelText() => mainLevelText.text = "Level " + PlayerPrefs.GetInt("Level", 1);

    public void UpdateCoinText(int score = 0)
    {
        int coin = PlayerPrefs.GetInt("Coin", 0) + score;

        PlayerPrefs.SetInt("Coin", coin);

        coinText.text = coin.ToString();
    }

    private void IncreaseLevelText() => PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level", 1) + 1);

    public void PowerProcessUpdate(float fillAmount)
    {
        powerProcessImage.fillAmount = fillAmount;

        powerProcessButton.interactable = fillAmount < 1 ? false : true;
    }

    public void ProcessAction()
    {
        StartCoroutine(ObjectManager.instance.GetPlayerController.Power());

        powerProcessButton.gameObject.SetActive(false);
    }
}
