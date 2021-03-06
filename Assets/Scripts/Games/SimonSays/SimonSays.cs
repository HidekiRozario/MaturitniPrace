using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimonSays : Game
{
    [SerializeField] private GameObject[] simonSays;
    [SerializeField] private GameObject[] Buttons;

    private int[] simonSequence;
    private int[] playerSequence;
    private int position = 0;
    private int limit;

    private float delayCooldown = 0.4f;
    private float delayTime = 0;
    private int positionFlash = 0;
    private bool isOn = false;
    private bool isOnTest = false;
    private bool isWrong = false;
    [SerializeField] private Material flashOn;
    [SerializeField] private Material flashFail;
    [SerializeField] private Material flashDefault;

    private void Start()
    {
        limit = difficulty + 4;
    }

    public override void Update()
    {
        if(wasBroken != isBroken)
        {
            SetWasBroken(true);
            CreateSequence();
        }

        if (delayTime > 0)
            delayTime -= Time.deltaTime;

        if (isWrong && delayTime <= 0 && !isDestroyed)
        {
            int i = 0;
            foreach (GameObject simon in simonSays)
            {
                if (!isOnTest)
                {
                    simon.GetComponent<MeshRenderer>().material = flashFail;
                    Buttons[i].GetComponentInChildren<SimonSaysButton>().isChanged = true;
                }
                else
                {
                    simon.GetComponent<MeshRenderer>().material = flashDefault;
                    Buttons[i].GetComponentInChildren<SimonSaysButton>().isChanged = false;
                }
                if (simon == simonSays[8] && Buttons[8].GetComponentInChildren<SimonSaysButton>().isChanged)
                    isOnTest = !isOnTest;

                if (simon == simonSays[8] && !Buttons[8].GetComponentInChildren<SimonSaysButton>().isChanged)
                {
                    isWrong = false;
                    isOnTest = !isOnTest;
                    positionFlash = 0;
                }
                i++;
            }
            delayTime = delayCooldown;
        }

        if (positionFlash >= limit && delayTime <= 0 && !isWrong && !isDestroyed)
        {
            foreach (GameObject simon in simonSays)
            {
                if (!isOnTest)
                {
                    simon.GetComponent<MeshRenderer>().material = flashOn;
                }
                else
                {
                    simon.GetComponent<MeshRenderer>().material = flashDefault;
                    if (simon == simonSays[8])
                        positionFlash = 0;
                }
                if (simon == simonSays[8])
                {
                    isOnTest = !isOnTest;
                }
            }
            delayTime = delayCooldown;
        }

        if(delayTime <= 0 && positionFlash < limit && isBroken && !isWrong && !isDestroyed)
        {
            if (!isOn) {
                simonSays[simonSequence[positionFlash]].GetComponent<MeshRenderer>().material = flashOn;
                isOn = true;
            }
            else {
                simonSays[simonSequence[positionFlash]].GetComponent<MeshRenderer>().material = flashDefault;
                isOn = false;
                positionFlash++;
            }
            delayTime = delayCooldown;
        }

        base.Update();
    }

    private void SimonSaysLoop(bool outcome)
    {
        if (isBroken && !isDestroyed)
        {
            if (!outcome)
            {
                Reset();
                return;
            }

            if (position == limit)
            {
                foreach (GameObject simon in simonSays)
                {
                    simon.GetComponent<MeshRenderer>().material = flashDefault;
                }
                SetBroken(false);
                SetWasBroken(false);
                return;
            }
        }
    }

    private void Reset()
    {
        foreach (GameObject simon in simonSays)
        {
            simon.GetComponent<MeshRenderer>().material = flashDefault;
        }
        CreateSequence();
        position = 0;
    }

    public void ButtonPress(int id)
    {
        if (isBroken && !isWrong && !isDestroyed)
        {
            position++;
            playerSequence[position - 1] = id;

            if (playerSequence[position - 1] != simonSequence[position - 1])
            {
                isWrong = true;
                SimonSaysLoop(false);
            }

            SimonSaysLoop(true);
        }
    }

    private void CreateSequence()
    {
        simonSequence = new int[limit];
        playerSequence = new int[limit];
        position = 0;
        positionFlash = 0;
        delayTime = delayCooldown;
        isOn = false;

        for (int i = 0; i < limit; i++)
        {
            simonSequence[i] = Random.Range(0, 9);
        }
    }
}
