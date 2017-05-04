using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionMenuCanvas : MonoBehaviour {
    public Button backButton;

    void Start()
    {
        backButton.GetComponent<Button>().onClick.AddListener(TaskOnBackButtonClick);
    }

    void TaskOnBackButtonClick()
    {
        GameObject.Find("MenuCanvas").GetComponent<Canvas>().enabled = true;
        GameObject.Find("OptionCanvas").GetComponent<Canvas>().enabled = false;
    }
}