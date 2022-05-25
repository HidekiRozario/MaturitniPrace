using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerScore
{
    public int score;
    public int tasksRepaired;
    public string levelName;

    public PlayerScore(int _score, int _tasksRepaired, string _levelName)
    {
        score = _score;
        tasksRepaired = _tasksRepaired;
        levelName = _levelName;
    }
    public PlayerScore()
    {
        score = 0;
        tasksRepaired = 0;
        levelName = "none";
    }
}

[System.Serializable]
public class PlayerData
{
    public List<PlayerScore> scores;

    public PlayerData() { scores = new List<PlayerScore>(); }

    public PlayerData(int _score, int _tasksRepaired, string _levelName)
    {
        scores = new List<PlayerScore>();
        scores.Add(new PlayerScore(_score, _tasksRepaired, _levelName));
    }

    public void AddScore(int _score, int _tasksRepaired, string _levelName)
    {
        if(scores != null)
        scores.Add(new PlayerScore(_score, _tasksRepaired, _levelName));
        else
        {
            scores = new List<PlayerScore>();
            scores.Add(new PlayerScore(_score, _tasksRepaired, _levelName));
        }
    }

    public void AddScore(PlayerScore score)
    {
        scores.Add(score);
    }

    public void SortScores() => scores.Sort((x, y) => y.score.CompareTo(x.score));

    public PlayerScore GetScore(int index) { 
        if (index <= scores.Count) 
            return scores[index];
        else
        {
            return new PlayerScore();
        }
    }

    public List<PlayerScore> GetScores() => scores;

    public bool IsEmpty()
    {
        if (scores == null)
        {
            return true;
        }
        else
            return false;
    }
}
