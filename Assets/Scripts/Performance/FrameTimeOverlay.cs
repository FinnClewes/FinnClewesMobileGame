using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FrameTimeOverlay : MonoBehaviour
{
    const float WindowMs = 30000f;            // measure over the last 30 seconds
    readonly Queue<float> frames = new();
    readonly List<float> sorted = new();
    float totalMs, timer, avg, p99;
    GUIStyle style;

    void Update()
    {
        float ms = Time.unscaledDeltaTime * 1000f;
        frames.Enqueue(ms);
        totalMs += ms;
        while (totalMs > WindowMs) totalMs -= frames.Dequeue();

        timer += Time.unscaledDeltaTime;
        if (timer >= 1f) { timer = 0f; Recalc(); }   // refresh the numbers once a second
    }

    void Recalc()
    {
        avg = totalMs / frames.Count;
        sorted.Clear();
        sorted.AddRange(frames);
        sorted.Sort();
        p99 = sorted[(int)(0.99f * (sorted.Count - 1))];
    }

    void OnGUI()
    {
        style ??= new GUIStyle(GUI.skin.label) { fontSize = 36 };
        var urp = UniversalRenderPipeline.asset;
        GUI.Label(new Rect(20, 20, 1200, 300),
            $"avg {avg:0.0} ms  ({1000f / Mathf.Max(avg, 0.01f):0} fps)\n" +
            $"99th {p99:0.0} ms\n" +
            $"target {Application.targetFrameRate}  scale {(urp ? urp.renderScale : 1f):0.0}\n" +
            $"panel {Screen.currentResolution.refreshRateRatio.value:0} Hz", style);
    }
}