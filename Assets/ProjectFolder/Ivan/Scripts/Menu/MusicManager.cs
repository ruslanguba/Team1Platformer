using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    public AudioSource MusicSource;
    public List<AudioClip> Playlist; // Список треков для плейлиста

    private int _currentTrackIndex = 0;
    private Coroutine _playlistRoutine;  

    private void Start()
    {
        if (Playlist.Count > 0)
        {
            _playlistRoutine = StartCoroutine(PlayPlaylist());
        }
    }

    private void OnDestroy()
    {
        if (_playlistRoutine != null)
        {
            StopCoroutine(_playlistRoutine);
        }       
    }

    private IEnumerator PlayPlaylist()
    {
        while (true) // Бесконечный цикл плейлиста
        {
            // Воспроизводим текущий трек
            MusicSource.clip = Playlist[_currentTrackIndex];
            MusicSource.Play();

            // Ждем, пока трек не закончится
            yield return new WaitForSeconds(Playlist[_currentTrackIndex].length);

            // Переходим к следующему треку (с зацикливанием)
            _currentTrackIndex = (_currentTrackIndex + 1) % Playlist.Count;
        }
    }
}
