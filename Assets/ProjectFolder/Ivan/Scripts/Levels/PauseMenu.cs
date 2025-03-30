using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PauseMenu : UIManager
{
    [SerializeField] private GameObject pauseMenu;
    private bool isPaused = false;
    private Coroutine pauseCheckCoroutine;

    private void Start()
    {
        pauseMenu.SetActive(false);
    }

    private void OnEnable()
    {
        pauseCheckCoroutine = StartCoroutine(CheckForPauseInput());
    }

    private void OnDisable()
    {
        if (pauseCheckCoroutine != null)
        {
            StopCoroutine(pauseCheckCoroutine);
        }
    }

    private IEnumerator CheckForPauseInput()
    {
        while (true)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (isPaused)
                {
                    Resume();
                }
                else
                {
                    Pause();
                }
            }
            yield return null; // ∆дЄм следующий кадр
        }
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void BackToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
