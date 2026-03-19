using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.Recorder;
#endif

public class RecorderScript : MonoBehaviour
{
#if UNITY_EDITOR
    Recorder behaviourUpdateRecorder;
#endif

    public bool Graba;

#if UNITY_EDITOR
    private RecorderWindow GetRecorderWindow()
    {
        return (RecorderWindow)EditorWindow.GetWindow(typeof(RecorderWindow));
    }
#endif

    void Start()
    {
    }

    void Update()
    {
    }

    public void StartRecordingVoid()
    {
        StartCoroutine(Recording());
    }

    public IEnumerator Recording()
    {
        var taza = this.GetComponent<ControladorTaza>();

        taza.ParentPanel.SetActive(false);
        taza.Camara.GetComponent<Camera>().rect = new Rect(0, 0, 1, 1);
        taza.Slider.GetComponent<Slider>().value = 0;
        taza.rotationOn = true;
        taza.Recording = true;

#if UNITY_EDITOR
        RecorderWindow recorderWindow = GetRecorderWindow();
        recorderWindow.StartRecording();
#endif

        while (taza.OnAxis <= 359)
        {
            yield return null;
        }

        taza.ParentPanel.SetActive(true);
        taza.Camara.GetComponent<Camera>().rect = new Rect(0.5f, 0, 1, 1);
        taza.rotationOn = false;
        taza.Recording = false;

#if UNITY_EDITOR
        recorderWindow.StopRecording();
#endif
    }
}