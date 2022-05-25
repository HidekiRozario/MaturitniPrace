using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MathGame : Game
{
    [SerializeField] private TMP_Text[] answersText = new TextMeshPro[3];
    [SerializeField] private TMP_Text questionText;
    [SerializeField] private GameObject timer;
    [SerializeField] private float timerCooldown = 4f;
    private float timerTime = 4f;

    [SerializeField] private MeshRenderer[] questionRends;
    [SerializeField] private Material questionAnswered;
    [SerializeField] private Material questionOn;
    [SerializeField] private Material questionOff;

    [SerializeField] private int maxRound = 3;
    private int round = 0;
    private int rightAnswer = -1;

    private void Start()
    {
        maxRound = 4 + difficulty;
    }

    // Update is called once per frame
    public override void Update()
    {
        if(timerTime > 0 && isBroken)
        {
            timerTime -= Time.deltaTime;
            timer.transform.localScale = new Vector3(timer.transform.localScale.x, timer.transform.localScale.y, timerTime / timerCooldown);
        } 
        else if (timerTime <= 0)
        {
            ResetRounds();
        }

        if(wasBroken != isBroken)
        {
            for(int i = 0; i < maxRound; i++)
            {
                questionRends[i].material = questionOn;
            }
            questionText.text = GetNewQuestion();
            SetWasBroken(isBroken);
        }

        if(round >= maxRound || !isBroken)
        {
            for (int i = 0; i < maxRound; i++)
            {
                questionRends[i].material = questionOff;
            }

            foreach (TMP_Text text in answersText)
            {
                text.text = null;
            }

            questionText.text = null;
            timer.transform.localScale = new Vector3(1, 1, 1);
            rightAnswer = -1;
            round = 0;
            timerTime = timerCooldown;

            SetBroken(false);
            SetWasBroken(false);
        }

        base.Update();
    }

    private string GetNewQuestion()
    {
        foreach(TMP_Text text in answersText)
        {
            text.text = null;
        }
        rightAnswer = -1;

        int a = Random.Range(0, 10);
        int b = Random.Range(0, 10);
        string _operator;
        int answer;

        if(Random.Range(1, 3) == 1)
        {
            answer = a + b;
            _operator = " + ";
        } 
        else
        {
            answer = a - b;
            _operator = " - ";
        }

        for(int i = 0; i < answersText.Length; i++)
        {
            int rnd = 0;
            if (i == 0)
            {
                rnd = Random.Range(0, 3);

                answersText[rnd].text = answer.ToString();
                rightAnswer = rnd;
            } 

            if(answersText[i].text == null)
            {
                while (rnd == 0) 
                {
                    rnd = Random.Range(-5, 6);
                }

                answersText[i].text = (answer + rnd).ToString();
            }
        }

        return (a + _operator + b);
    }

    public void ResetRounds()
    {
        for (int i = 0; i < maxRound; i++)
        {
            questionRends[i].material = questionOn;
        }
        timerTime = timerCooldown;
        rightAnswer = 0;
        round = 0;
        questionText.text = GetNewQuestion();
    }

    public void Answer(int _input)
    {
        if (_input == rightAnswer)
        {
            timerTime = timerCooldown;
            questionRends[round].material = questionAnswered;
            round++;
            questionText.text = GetNewQuestion();
            return;
        }
        ResetRounds();
    }
}
