using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatsLoader : MonoBehaviour
{
    [SerializeField] private string levelName = "nuclear";
    [SerializeField] private float scrollValue = 0.05f;
    [SerializeField] private TMP_Text scoreText;

    private PlayerData data;

    private float scrollYLimit;

    private void OnEnable()
    {
        scrollYLimit = scoreText.transform.position.y;
        data = SaveSystem.loadScore();
        ShowStats();
    }

    private void ShowStats()
    {
        Debug.Log("CALL");
        PlayerData levelData = new PlayerData();

        if (!data.IsEmpty())
        {
            Debug.Log("SET");
            foreach (PlayerScore score in data.GetScores())
            {
                if (score.levelName == levelName)
                {
                    levelData.AddScore(score);
                }
            }

            levelData.SortScores();

            scoreText.text = "";

            for (int i = 0; i < levelData.scores.Count; i++)
            {
                if (levelData.GetScores().Count > i)
                    scoreText.text += (i + 1) + ". score: " + levelData.GetScore(i).score + " tasks: " + levelData.GetScore(i).tasksRepaired + "\n";
            }
        }
    }

    public void Scroll(int dir)
    {
        scoreText.transform.position = new Vector3(scoreText.transform.position.x, scoreText.transform.position.y + (dir * scrollValue), scoreText.transform.position.z);
        if(scoreText.transform.position.y < scrollYLimit)
        {
            scoreText.transform.position = new Vector3(scoreText.transform.position.x, scrollYLimit, scoreText.transform.position.z);
        }
    }
}
