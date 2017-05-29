using UnityEngine;

public class GameOptions : MonoBehaviour {
    static public bool isVolumeEnabled = true;
    static public bool areSoundEffectsEnabled = true;
    public enum InputMode
    {
        OnlyKeyboard,
        OnlyGesture,
        KeyboardAndGesture
    };
    static public InputMode inputModeSelected = InputMode.OnlyGesture;

    public bool IsVolumeEnabled
    {
        get
        {
            return isVolumeEnabled;
        }

        set
        {
            isVolumeEnabled = value;
        }
    }

    public bool AreSoundEffectsEnabled
    {
        get
        {
            return areSoundEffectsEnabled;
        }

        set
        {
            areSoundEffectsEnabled = value;
        }
    }

    public InputMode InputModeSelected
    {
        get
        {
            return inputModeSelected;
        }

        set
        {
            inputModeSelected = value;
        }
    }
}
