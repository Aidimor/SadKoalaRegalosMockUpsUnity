using System.Collections;
using UnityEngine;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;
#endif

// Add this using directive
using UnityEngine.Networking;

public class ControladorImagenes2 : MonoBehaviour
{
    string path;
    public RawImage image;

#if UNITY_EDITOR
    [ContextMenu("Open Image Explorer")]
    public void OpenExplorer()
    {
        path = EditorUtility.OpenFilePanel("Overwrite with png", "", "png");
        GetImage();
    }
#endif

    void GetImage()
    {
        if (!string.IsNullOrEmpty(path))
        {
            UpdateImage();
        }
    }

    void UpdateImage()
    {
        StartCoroutine(LoadImageCoroutine());
    }

    IEnumerator LoadImageCoroutine()
    {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture("file:///" + path);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            Texture2D texture = DownloadHandlerTexture.GetContent(www);
            image.texture = texture;
        }
        else
        {
            Debug.LogError("Error loading image: " + www.error);
        }
    }
}