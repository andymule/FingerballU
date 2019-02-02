using System.Collections;
using UnityEngine;

public class MobileUtilsScript : MonoBehaviour
{
    private int FramesPerSec;
    private float frequency = 1.0f;
    private string fps;
    GUIStyle style;
    void Start()
    {
        style = new GUIStyle();
        style.alignment = TextAnchor.UpperLeft;
        style.fontSize = 200;
        style.normal.textColor = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        StartCoroutine(FPS());
    }

    private IEnumerator FPS()
    {
        for (; ; )
        {
            // Capture frame-per-second
            int lastFrameCount = Time.frameCount;
            float lastTime = Time.realtimeSinceStartup;
            yield return new WaitForSeconds(frequency);
            float timeSpan = Time.realtimeSinceStartup - lastTime;
            int frameCount = Time.frameCount - lastFrameCount;
            // Display it
            fps = string.Format("FPS: {0}", Mathf.RoundToInt(frameCount / timeSpan));
        }
    }

    void OnGUI()
    {
        Rect rect = new Rect(0, 0, Screen.width, Screen.height * 2 / 100);
        GUI.Label(new Rect(0, 0, 250, 250), fps, style);
    }
}