using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreMonitor : MonoBehaviour
{
    [SerializeField] private GameController gc;
    [SerializeField] private TMP_Text scoreText;

    // Update is called once per frame
    void Update()
    {
        scoreText.text = Mathf.Round(gc.GetScore()).ToString() + "$";
    }
}
