using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class IntroVideo : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public string nextSceneName; // Название следующей сцены

    void Start()
    {
        if (videoPlayer == null)
        {
            videoPlayer = GetComponent<VideoPlayer>();
        }
        videoPlayer.loopPointReached += OnVideoFinished;
    }

    private void OnDisable()
    {
        videoPlayer.loopPointReached -= OnVideoFinished;
    }

    void OnVideoFinished(VideoPlayer vp)
    {
        Debug.Log("Finished");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); // Загружаем следующую сцену
    }

    void Update()
    {
        if (Input.anyKey)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); // Загружаем следующую сцену
        }
    }
    
}
