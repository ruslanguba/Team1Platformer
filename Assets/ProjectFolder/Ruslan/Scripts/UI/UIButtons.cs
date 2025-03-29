using UnityEngine;
using UnityEngine.SceneManagement;

public class UIButtons : MonoBehaviour
{
    [SerializeField] private GameObject GameMenu;
    private bool _isPoused;

    private void Start()
    {
        GameMenu.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (!_isPoused)
            {
                Pause();
            }
            else
            {
                Resume();
            }
        }
    }
    public void OnClickExit()
    {
        Application.Quit();
    }

    public void OnClickResume()
    {
        Resume();
    }

    public void OnClickRestart()
    {
        GameMenu.SetActive(false);
        Resume();
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnClickMenu()
    {
        GameMenu.SetActive(false);
        Resume();
        SceneManager.LoadScene(0);
    }

    private void Pause()
    {
        GameMenu.SetActive(true);
        _isPoused = true;
        Time.timeScale = 0;
    }
    
    private void Resume()
    {
        GameMenu.SetActive(false);
        _isPoused = false;
        Time.timeScale = 1.0f;
    }
}
