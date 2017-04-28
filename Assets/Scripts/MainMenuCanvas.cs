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
        Debug.Log("You have option clicked the button!");
    }

    void TaskOnExitButtonClick()
    {
        Application.Quit();
    }
}