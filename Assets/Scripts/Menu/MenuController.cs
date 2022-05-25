using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuController : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreTXT;
    [SerializeField] private TMP_Text tasksRepairedTXT;

    [SerializeField] private PlayerData playerScores;

    [SerializeField] private Image playerTransitionImage;
    [SerializeField] private float timeImageDelta = 0;

    void Awake()
    {
        playerTransitionImage.enabled = true;

        playerScores = SaveSystem.loadScore();
        if (GameObject.Find("container") != null)
        {
            timeImageDelta = 1;
            ShowStats();
        }
        else
        {
            playerTransitionImage.color = new Color(playerTransitionImage.color.r, playerTransitionImage.color.g, playerTransitionImage.color.b, 0f);
            for (int i = 0; i < GameObject.Find("MenuWindows").transform.childCount; i++)
            {
                if (GameObject.Find("MenuWindows").transform.GetChild(i).name == "MainMenu")
                    GameObject.Find("MenuWindows").transform.GetChild(i).gameObject.SetActive(true);
                else
                    GameObject.Find("MenuWindows").transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }
    private void FixedUpdate()
    {
        if(timeImageDelta > 0)
        {
            timeImageDelta -= Time.deltaTime / 1f;
            playerTransitionImage.color = new Color(playerTransitionImage.color.r, playerTransitionImage.color.g, playerTransitionImage.color.b, timeImageDelta);
        }
    }

    private void ShowStats()
    {
        container cont = GameObject.Find("container").GetComponent<container>();

        scoreTXT.text += cont.score;
        tasksRepairedTXT.text += cont.tasksRepaired;

        for(int i = 0; i < GameObject.Find("MenuWindows").transform.childCount; i++)
        {
            if(GameObject.Find("MenuWindows").transform.GetChild(i).name == "Stats")
                GameObject.Find("MenuWindows").transform.GetChild(i).gameObject.SetActive(true);
            else
                GameObject.Find("MenuWindows").transform.GetChild(i).gameObject.SetActive(false);
        }

        //SAVE STATS
        playerScores.AddScore(cont.score, cont.tasksRepaired, cont.levelName);

        SaveSystem.SaveData(playerScores);
        Destroy(cont.gameObject);
    }
}
