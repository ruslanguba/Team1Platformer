using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMusic : MonoBehaviour
{
    public static LevelMusic Instance;

    private AudioSource _audioSource;

    private void Awake()
    {
        // ������� ��������, ���� �� ����
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        _audioSource = GetComponent<AudioSource>();

        // ��������, ��� ������ ��������� �� ������ � ������
        _audioSource.loop = true;
        if (!_audioSource.isPlaying)
        {
            _audioSource.Play();
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // ���������� ������, ���� ��� ����� ����
        if (scene.name == "0_Menu" || scene.buildIndex == 1 || scene.buildIndex == 9 || scene.buildIndex == 10)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }
}
