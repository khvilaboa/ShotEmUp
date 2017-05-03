using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuCanvas : MonoBehaviour
{
    public Button startButton;
    public Button optionsButton;
    public Button exitButton;

    void Start()
    {
        startButton.GetComponent<Button>().onClick.AddListener(TaskOnStartButtonClick);
        optionsButton.GetComponent<Button>().onClick.AddListener(TaskOnOptionsButtonClick);
        exitButton.GetComponent<Button>().onClick.AddListener(TaskOnExitButtonClick);
    }

    void TaskOnStartButtonClick()
    {
        SceneManager.LoadScene("gameScene", LoadSceneMode.Single);
    }

    void TaskOnOptionsButtonClick()
    {
        GameObject.Find("MenuCanvas").GetComponent<Canvas>().enabled = false;
        GameObject.Find("OptionCanvas").GetComponent<Canvas>().enabled = true;
    }

    void TaskOnExitButtonClick()
    {
        Application.Quit();
    }
}