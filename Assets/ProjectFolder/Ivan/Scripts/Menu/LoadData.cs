using UnityEngine;

public class LoadData : DataNames
{
    public float LoadTimer(int level)
    {
        string Key = TimerName + level;
        float timer = PlayerPrefs.GetFloat(Key, 0);
        return timer;
    }

    public int LoadDeths(int level)
    {
        string Key = DethsName + level;
        int deths = PlayerPrefs.GetInt(Key, 0);
        return deths;
    }

    public  int LoadCollect(int level)
    {
        string Key = CollectName + level;
        int collects = PlayerPrefs.GetInt(Key, 0);
        return collects;
    }

    public int LoadStar(int level) 
    {
        string Key = StarName + level;
        int star = PlayerPrefs.GetInt(Key, 0);
        return star;
    }
}
