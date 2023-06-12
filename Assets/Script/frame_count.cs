using UnityEngine;
using System.Collections;

public class frame_count : MonoBehaviour
{
    public static int FrameRate=30;
    float deltaTime = 0.0f;
    void Update()
    {
        if (Time.timeScale != 0)
        {
            deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        }
        else
        {
            return;
        }
    }

   /* void OnGUI()
    {
        int w = Screen.width, h = Screen.height;

        GUIStyle style = new GUIStyle();

        Rect rect = new Rect(w-(h*30/100), h-(h*8/100), w, h * 2 / 100);
        style.alignment = TextAnchor.UpperLeft;
        style.fontSize = h * 4 / 100;
        style.normal.textColor = new Color(1.0f, 1.0f, 0.5f, 1.0f);
        float msec = deltaTime * 1000.0f;
        float fps = 1.0f / deltaTime;
        string text = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);
        GUI.Label(rect, text, style);
    }*/
    private void FixedUpdate()
    {
        Application.targetFrameRate = FrameRate;
        QualitySettings.vSyncCount = 0;
      
    }
  
}
