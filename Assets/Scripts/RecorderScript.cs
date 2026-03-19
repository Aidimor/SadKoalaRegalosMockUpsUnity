using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Profiling;
using UnityEditor.Recorder;
using UnityEditor;
using UnityEngine.UI;

public class RecorderScript : MonoBehaviour
{
    Recorder behaviourUpdateRecorder;

    public bool Graba;


    private RecorderWindow GetRecorderWindow()
    {
        return (RecorderWindow)EditorWindow.GetWindow(typeof(RecorderWindow));
    }
    // Start is called before the first frame update
    void Start()
    {
        //RecorderWindow recorderWindow = GetRecorderWindow();



    }

    // Update is called once per frame
    void Update()
    {

        //RecorderWindow recorderWindow = GetRecorderWindow();
 
    }

    public void StartRecordingVoid()
    {
        StartCoroutine(Recording());
    }

    public IEnumerator Recording()
    {
        this.GetComponent<ControladorTaza>().ParentPanel.SetActive(false);
        this.GetComponent<ControladorTaza>().Camara.GetComponent<Camera>().rect = new Rect(0, 0, 1, 1);
        this.GetComponent<ControladorTaza>().Slider.GetComponent<Slider>().value = 0;
        this.GetComponent<ControladorTaza>().rotationOn = true;
        this.GetComponent<ControladorTaza>().Recording = true;
        RecorderWindow recorderWindow = GetRecorderWindow();
        recorderWindow.StartRecording();

        while (this.GetComponent<ControladorTaza>().OnAxis <= 359)
        {
            yield return null;
        }

        this.GetComponent<ControladorTaza>().ParentPanel.SetActive(true);
        this.GetComponent<ControladorTaza>().Camara.GetComponent<Camera>().rect = new Rect(0.5f, 0, 1, 1);
 
        this.GetComponent<ControladorTaza>().rotationOn = false;
        this.GetComponent<ControladorTaza>().Recording = false;

        recorderWindow.StopRecording();
    }







}
