using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuController : MonoBehaviour
{
    public void OnStartclick()
    {
        SceneManager.LoadScene("ParchmentPaper");
    }

    public void OnExitClick()
    {
#if UNITY_EDITOR
    UnityEditor.EditorApplication.isPlaying = false;
#endif
    Application.Quit();
    }

     public void OnGameStartClick()
    {
        SceneManager.LoadScene("Scene1");
    }

    public void OnGameOptionClick()
    {
        SceneManager.LoadScene("Scene3");
    }
}
