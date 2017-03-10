using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils {

	public static Vector2 GetViewDimensions() {
        Camera camera = Camera.main;
        float camWidth, camHeight, ratio;

        ratio = (float) camera.pixelWidth / camera.pixelHeight;

        camHeight = camera.orthographicSize * 2;
        camWidth = ratio * camHeight;

        return new Vector2(camWidth, camHeight);
    }
}
