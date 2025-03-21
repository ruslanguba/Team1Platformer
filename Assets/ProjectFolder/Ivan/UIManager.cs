using System;
//using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }
    public void QuitApplication()
    {
#if UNITY_EDITOR
        // Если игра запущена в редакторе Unity, используем его
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // Если игра собрана и запущена на платформе
        Application.Quit();
#endif
    }
}
