using UnityEngine;
using UnityEngine.UI;

public class AchivementManager : DataNames
{
    [SerializeField] private LoadData _loadData;
    [SerializeField] private LevelManager _levelManager;
    [SerializeField] private float _timeLimit = 20;

    [SerializeField] private GameObject _achievementFinish;
    [SerializeField] private GameObject _achievementCollects;
    [SerializeField] private GameObject _achievementTime;
    [SerializeField] private GameObject _achievementDeths;
  


    bool gameIsFinished = false;



    private void Start()
    {
        gameIsFinished = PlayerPrefs.GetInt(AchivementName, 0) != 0;

        TakeColor(_achievementFinish, gameIsFinished);
        TakeColor(_achievementCollects, TakeAchivementCollects());
        TakeColor(_achievementTime,TakeAchivementTime());
        TakeColor(_achievementDeths, TakeAchivementDeths());
    }
    void TakeColor(GameObject achivmentObject,bool condition)
    {
        Image image = achivmentObject.GetComponentInChildren<Image>();
        Color currentColor = image.color;
        currentColor.a = condition ? 0.8f : 0.3f;
        image.color = currentColor;
    }


    bool TakeAchivementTime()
    {
        bool achivementTime = false;
        if (gameIsFinished)
        {
            float allPlayTime = 0;
            for (int i = 1; i < _levelManager.buttons.Length; i++)
            {
                allPlayTime += _loadData.LoadTimer(i);
            }
            achivementTime = allPlayTime < _timeLimit;
        }         
        return achivementTime;
    }

     bool TakeAchivementDeths()
    {
        bool achivementDeths = false;
        if (gameIsFinished)
        {
            float allDeths = 0;

            for (int i = 1; i < _levelManager.buttons.Length; i++)
            {
                allDeths += _loadData.LoadDeths(i);
            }
            achivementDeths = allDeths <= 0;
        }        
        return achivementDeths;
    }

     bool TakeAchivementCollects()
    {
        bool achivementCollects = false;
        if(gameIsFinished)
        {
            float AllCollects = 0;
            for (int i = 1; i < _levelManager.buttons.Length; i++)
            {
                AllCollects += _loadData.LoadCollect(i);
            }
            achivementCollects = AllCollects >= 21;
        }      
        return achivementCollects;
    }
}


