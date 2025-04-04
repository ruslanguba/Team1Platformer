using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMusic : MusicManager
{
    public static LevelMusic Instance;

    private void Awake()
    {
        // ���� ��� ���� ������ MusicManager (��������, �� ����), ���������� ���
        if (Instance != null && Instance != this)
        {
            Destroy(Instance.gameObject);
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        // ������������� �� ������� ����� �����
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // ���� ����������� ���� (����� ��������� �� ����� ��� ����), ���������� ���� ������
        if (scene.name == "0_Menu" || scene.buildIndex == 1 || scene.buildIndex == 9 || scene.buildIndex == 10)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            Destroy(gameObject);
        }
    }
    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
