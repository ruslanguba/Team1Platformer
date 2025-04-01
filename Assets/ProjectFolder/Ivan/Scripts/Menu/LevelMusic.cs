using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMusic : MusicManager
{
    public static LevelMusic Instance;

    private void Awake()
    {
        // Если уже есть другой MusicManager (например, из меню), уничтожаем его
        if (Instance != null && Instance != this)
        {
            Destroy(Instance.gameObject);
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        // Подписываемся на событие смены сцены
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Если загрузилось меню (можно проверять по имени или тегу), уничтожаем этот объект
        if (scene.name == "0_Menu" || scene.buildIndex >= 1)
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
