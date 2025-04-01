using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.PlayerPrefs;


public class SaveData :  DataNames
{
    [HideInInspector]public int Star = 0;
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
        SaveTimer(_timeLimit);
        SaveCollects();
        SaveDeaths();
        SaveStar(Star);
    }

     void SaveTimer(float timeLimit)
    {
        string Key = TimerName + _level;
        _timer.StopTimer();
        if(_timer.TimeElapsed < GetFloat(Key,99999))
        {
            SetFloat(Key, _timer.TimeElapsed);

            if (GetFloat(Key, 0) <= timeLimit * 60)
            {
                Star++;
            }

            Save();
        }
    }
     void SaveCollects()
    {
        string Key = CollectName + _level;
        if (_collectTracker.GetCurrentCollected() > GetInt(Key, 0))
        {
            SetInt(Key, _collectTracker.GetCurrentCollected());
            if (GetInt(Key, 0) == 3)
            {
                Star++;
            }

            Save();
        }
    }
     void SaveDeaths()
    {
        string Key = DethsName + _level;
        if(_deathCounter.GetTotalDeaths() < GetInt(Key, 999))
        {
            SetInt(Key, _deathCounter.GetTotalDeaths());
            if (GetInt(Key, 999) <= 0)
            {
                Star++;
            }

            Save();
        }
    }

    void SaveStar(int starCurent)
    {
        string Key = StarName + _level;
        if(starCurent > GetInt(Key, 0))
        {
            SetInt(Key, starCurent);
            Save();
        }
    }
    public void SaveFinish()
    {
        if(GetInt(AchivementName, 0) != 1)
        SetInt(AchivementName, 1);
    }
}
