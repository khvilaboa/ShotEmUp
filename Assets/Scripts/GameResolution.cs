using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameResolution : MonoBehaviour {
    public int fpsLimit = 45;

    void Awake()
    {
        Application.targetFrameRate = fpsLimit;
        #if UNITY_EDITOR
            QualitySettings.vSyncCount = 0;  // VSync must be disabled
            Application.targetFrameRate = fpsLimit;
        #endif
        }
}
