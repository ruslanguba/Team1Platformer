using UnityEngine;
using UnityEngine.SceneManagement;

public class UIButtons : MonoBehaviour
{
    [SerializeField] private GameObject _pausePanel;
    [SerializeField] private GameObject _hintPanel;
    [SerializeField] private GameObject _scoreMenu;
    private bool _isPoused;

    private void Start()
    {
        _pausePanel.SetActive(false);
        _scoreMenu.SetActive(false);
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
        _pausePanel?.SetActive(false);
        _scoreMenu?.SetActive(false);
        Resume();
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnClickMenu()
    {
        _pausePanel?.SetActive(false);
        Resume();
        SceneManager.LoadScene(1);
    }

    private void Pause()
    {
        _pausePanel?.SetActive(true);
        _scoreMenu?.SetActive(true);
        _hintPanel?.SetActive(false);

        _isPoused = true;
        Time.timeScale = 0;
    }
    
    private void Resume()
    {
        _pausePanel?.SetActive(false);
        _scoreMenu?.SetActive(false);
        _hintPanel?.SetActive(true);
        _isPoused = false;
        Time.timeScale = 1.0f;
    }
}
