using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private void Start()
    {
        EventManager.instance.nextEvent += () => NextLevel();

        EventManager.instance.retryEvent += () => RetryLevel();
    }

    private void NextLevel()
    {
        int sceneNumber = PlayerPrefs.GetInt("Scene", 0);

        sceneNumber++;

        if (sceneNumber >= SceneManager.sceneCountInBuildSettings)
            sceneNumber = 0;

        PlayerPrefs.SetInt("Scene", sceneNumber);

        SceneManager.LoadScene(sceneNumber);
    }

    private void RetryLevel() => SceneManager.LoadScene(PlayerPrefs.GetInt("Scene", 0));
}
