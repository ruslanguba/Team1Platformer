using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.PlayerPrefs;


public class SaveData :  DataNames
{
    [SerializeField] private DeathCounter _deathCounter;
    [SerializeField] private CollectTracker _collectTracker;
    [SerializeField] private Timer _timer;

    [Header("Номер уровня")]
    [SerializeField] private int _level;

    [SerializeField] private float _timeLimit = 5;

    private int _index;


    private void Start()
    {
        _index = SceneManager.GetActiveScene().buildIndex;
    }

    public void UnlockNewLevel()
    {
        if (_index >= GetInt("ReachedIndex"))
        {
            SetInt("ReachedIndex", _index + 1);
            SetInt("UnlockedLevel",GetInt("UnlockedLevel", 1) + 1);
            Save();
        }
    }

    public void SaveAll()
    {
        SaveTimer();
        SaveCollects();
        SaveDeaths();
        SaveStar();
    }

     void SaveTimer()
    {
        string Key = TimerName + _level;
        _timer.StopTimer();
        if(_timer.TimeElapsed > GetFloat(Key,0))
        {
            SetFloat(Key, _timer.TimeElapsed);
            Save();
        }
    }
     void SaveCollects()
    {
        string Key = CollectName + _level;
        if (_collectTracker.GetCurrentCollected() > GetInt(Key, 0))
        {
            SetInt(Key, _collectTracker.GetCurrentCollected());
            Save();
        }
    }
     void SaveDeaths()
    {
        string Key = DethsName + _level;
        if(_deathCounter.GetTotalDeaths() < GetInt(Key, 0))
        {
            SetInt(Key, _deathCounter.GetTotalDeaths());
            Save();
        }
    }

    void SaveStar()
    {
        string Key = StarName + _level;
        if(Stars(_timeLimit ) >= GetInt(Key, 0))
        {
            SetInt(Key, Stars(_timeLimit));
            Save();
        }
    }


    public int Stars(float timeLimit)
    {
        int star = 0;
        if (_timer.TimeElapsed <= timeLimit * 60)
        {
            star++;
        }
        if (_deathCounter.GetTotalDeaths() == 0)
        {
            star++;
        }
        if (_collectTracker.GetCurrentCollected() == 3)
        {
            star++;
        }
        return star;
    }
}
