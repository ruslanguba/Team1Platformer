using UnityEngine;
using UnityEngine.SceneManagement;

public class UIButtons : MonoBehaviour
{
    [SerializeField] private GameObject GameMenu;

    private void Start()
    {
        GameMenu.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (!GameMenu.activeSelf)
            {
                GameMenu.SetActive(true);
            }
            else
            {
                GameMenu.SetActive(false);
            }
        }
    }
    public void OnClickExit()
    {
        Application.Quit();
    }

    public void OnClickRestart()
    {
        GameMenu.SetActive(false);
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnClickMenu()
    {
        GameMenu.SetActive(false);
        SceneManager.LoadScene(0);
    }
}
