using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    public static VolumeSettings Instance; // Синглтон

    public AudioMixer audioMixer; // Ссылка на AudioMixer
    public Slider musicSlider;    // Слайдер для музыки 
    public Slider SFXSlider;    // Слайдер для звуков 

    private const string MusicVolumeKey = "MusicVolume";
    private const string SFXVolumeKey = "SFXVolume";

    private void Awake()
    {
        // Реализация синглтона
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Не уничтожать при загрузке новой сцены
        }
        else
        {
            Destroy(gameObject); // Уничтожить дубликат
            return;
        }

        // Загружаем сохраненные значения громкости
        float savedMusicVolume = PlayerPrefs.GetFloat(MusicVolumeKey, 0.5f);
        float savedSFXVolume = PlayerPrefs.GetFloat(SFXVolumeKey, 0.5f);

       

        // Если слайдеры есть на текущей сцене (стартовое меню), настраиваем их
        if (musicSlider != null && SFXSlider != null)
        {
            musicSlider.value = savedMusicVolume;
            SFXSlider.value = savedSFXVolume;

            // Подписываемся на изменения слайдеров
            musicSlider.onValueChanged.AddListener(SetMusicVolume);
            SFXSlider.onValueChanged.AddListener(SetSFXVolume);
        }
        // Применяем настройки громкости
        SetMusicVolume(savedMusicVolume);
        SetSFXVolume(savedSFXVolume);
    }

    public void SetMusicVolume(float volume)
    {
        // Преобразуем значение слайдера в логарифмическую шкалу (в децибелы)
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20);
        // Сохраняем значение в PlayerPrefs
        PlayerPrefs.SetFloat(MusicVolumeKey, volume);
    }

    public void SetSFXVolume(float volume)
    {
        // Преобразуем значение слайдера в логарифмическую шкалу (в децибелы)
        audioMixer.SetFloat("SFXVolume", Mathf.Log10(volume) * 20);
        // Сохраняем значение в PlayerPrefs
        PlayerPrefs.SetFloat(SFXVolumeKey, volume);
    }
}
