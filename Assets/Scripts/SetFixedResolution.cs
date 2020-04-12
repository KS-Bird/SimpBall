using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetFixedResolution : MonoBehaviour
{
    void Awake()
    {
        Camera camera = GetComponent<Camera>();
        Rect rect = camera.rect;
        double scaleheight = ((double)Screen.width / Screen.height) / ((double)9 / 16); // (가로 / 세로)
        double scalewidth = 1f / scaleheight;
        if (scaleheight < 1) // 9 : 16의 height의 비율이 더 작으면
        {
            rect.height = (float)scaleheight;
            rect.y = (float)(1f - scaleheight) / 2f;
        }
        else
        {
            rect.width = (float)scalewidth;
            rect.x = (float)(1f - scalewidth) / 2f;
        }
        camera.rect = rect;
    }

    void OnPreCull() => GL.Clear(true, true, Color.black); //비율이 안 맞아서 남는곳을 칠한다.
}

