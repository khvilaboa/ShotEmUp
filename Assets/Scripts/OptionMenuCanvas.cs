using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionMenuCanvas : MonoBehaviour {
    public Button backButton;
    public Toggle volumeToggle;
    public Toggle soundEffectsToggle;
    public Dropdown inputDropdown;

    void Start()
    {
        volumeToggle.isOn = GameOptions.isVolumeEnabled;
        soundEffectsToggle.isOn = GameOptions.areSoundEffectsEnabled;
        if (GameOptions.inputModeSelected == GameOptions.InputMode.OnlyGesture)
        {
            inputDropdown.value = 0;
        }
        else
        {
            inputDropdown.value = 1;
        }
        backButton.GetComponent<Button>().onClick.AddListener(TaskOnBackButtonClick);
        volumeToggle.GetComponent<Toggle>().onValueChanged.AddListener(TaskOnVolumeToggleClick);
        soundEffectsToggle.GetComponent<Toggle>().onValueChanged.AddListener(TaskOnSoundEffectsToggleClick);
        inputDropdown.GetComponent<Dropdown>().onValueChanged.AddListener(TaskOnInputDropdownClick);
    }

    private void TaskOnInputDropdownClick(int value)
    {
        if (value == 0)
        {
            GameOptions.inputModeSelected = GameOptions.InputMode.OnlyGesture;
        }
        else if (value == 1)
        {
            GameOptions.inputModeSelected = GameOptions.InputMode.OnlyKeyboard;
        }
    }

    void TaskOnBackButtonClick()
    {
        GameObject.Find("MenuCanvas").GetComponent<Canvas>().enabled = true;
        GameObject.Find("OptionCanvas").GetComponent<Canvas>().enabled = false;
    }

    void TaskOnVolumeToggleClick(bool value)
    {
        GameOptions.isVolumeEnabled = value;
    }

    void TaskOnSoundEffectsToggleClick(bool value)
    {
        GameOptions.areSoundEffectsEnabled = value;
    }
}