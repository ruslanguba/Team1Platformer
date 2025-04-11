using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class IntroVideo : MonoBehaviour
{
    [SerializeField] private bool _isLoadingMenu;
    private VideoPlayer _videoPlayer;
    private string _mainMenuName = "0_Menu"; // �������� ��������� �����
    private string _firstLevelName = "lvl01"; // �������� ��������� �����

    void Start()
    {
        if(LevelMusic.Instance != null)
        {
            Destroy(LevelMusic.Instance);
        }
        if (_videoPlayer == null)
        {
            _videoPlayer = GetComponent<VideoPlayer>();
        }

        _videoPlayer.loopPointReached += OnVideoFinished;
    }

    private void OnDisable()
    {
        _videoPlayer.loopPointReached -= OnVideoFinished;
    }

    void OnVideoFinished(VideoPlayer vp)
    {
        LoadNextScene();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            LoadNextScene();
        }
    }
    
    private void LoadNextScene()
    {
        if (_isLoadingMenu)
        {
            SceneManager.LoadScene(_mainMenuName); // ��������� ����
        }
        else
        {
            SceneManager.LoadScene(_firstLevelName);
        }
    }
}
