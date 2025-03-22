using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Button[] buttons;
    public GameObject levelButtons;
    private void Awake()
    {
        ButtonsToArray();
        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = false;
        }

        for (int i = 0; i < unlockedLevel; i++)
        {
            buttons[i].interactable = true;
        }
    }
    public void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void OpenLevel(int levelID)
    {
        if(levelID < 10)
        {
            string levelName = "lvl0" + levelID;
            SceneManager.LoadScene(levelName);
        }
        else
        {
            string levelName = "lvl" + levelID;
            SceneManager.LoadScene(levelName);
        }
    }

    void ButtonsToArray()
    {
        int childCount = levelButtons.transform.childCount;

        buttons = new Button[childCount];

        for (int i = 0; i < childCount; i++)
        { 
            buttons [i] = levelButtons.transform.GetChild(i).gameObject.GetComponent<Button>();
        }
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
