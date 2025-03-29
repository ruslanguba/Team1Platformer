using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class ButtonController : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    public UnityEvent OnSoundFinished; // Событие, которое будет вызвано после завершения звука
    private Button _button; //Кнопка нажатия   

    SFXMenu MenuAudio;

    private void Awake()
    {
        //MenuAudio = GameObject.FindGameObjectWithTag("Audio").GetComponent<SFXMenu>();
        MenuAudio = FindFirstObjectByType<SFXMenu>();
    }

    //private void Start()
    //{       
    //    _button = GetComponent<Button>();

    //    if (_button != null)
    //    {
    //        // Добавляем обработчик нажатия на кнопку
    //        _button.onClick.AddListener(OnButtonClick);
    //    }
    //}

    //private void OnDestroy()
    //{
    //    _button.onClick.RemoveAllListeners();
    //}

    // Метод, вызываемый при наведении курсора на кнопку
    public void OnPointerEnter(PointerEventData eventData)
    {
        // Проверяем, включен ли звук и назначен ли звук наведения
        if (MenuAudio.SFXSource != null && MenuAudio.HoverSound != null)
        {
            MenuAudio.PlaySFX(MenuAudio.HoverSound);
        }
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        MenuAudio.PlaySFX(MenuAudio.ClickSound);
    }

    // Метод, вызываемый при клике по кнопке
    //private void OnButtonClick()
    //{
    //    if (MenuAudio.SFXSource != null && MenuAudio.ClickSound != null)
    //    {
    //        // Проигрываем звук
    //        MenuAudio.PlaySFX(MenuAudio.ClickSound);

    //        // Запускаем корутину, которая ждет завершения звука
    //        StartCoroutine(WaitForSoundToFinish());
    //    }
    //    else
    //    {
    //        // Если звук не задан, сразу вызываем событие
    //        OnSoundFinished.Invoke();
    //    }
    //}

    //private System.Collections.IEnumerator WaitForSoundToFinish()
    //{
    //    // Ждем, пока звук не закончится
    //    while (MenuAudio.SFXSource.isPlaying)
    //    {
    //        yield return null;
    //    }

    //    // Вызываем событие после завершения звука
    //    OnSoundFinished.Invoke();
    //}

}
