using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverGameStateController : Game
{
    private bool match = false;

    [SerializeField] private string[] states = new string[5];

    [SerializeField] private GameObject[] statesObj;
    [SerializeField] private GameObject[] playerStatesObj;
    [SerializeField] private string[] playerStatesStr;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public override void Update()
    {
        if (!isBroken)
        {
            foreach(GameObject stateObj in statesObj)
            {
                stateObj.GetComponent<StateController>().SetState("none");
            }
        }

        if(wasBroken != isBroken)
        {
            SetWasBroken(true);
            GenerateStates();
        }
        
        if(!match && isBroken)
        {
            GetPlayerStates();
            bool matching = true;
            int i = 0;
            foreach(string state in states)
            {
                if(playerStatesStr[i] != state)
                {
                    matching = false;
                }
                i++;
            }
            if (matching)
            {
                match = true;
                SetBroken(false);
                SetWasBroken(false);
            }
        }

        base.Update();
    }

    private void GetPlayerStates()
    {
        int i = 0;
        foreach(GameObject playerState in playerStatesObj)
        {
            playerStatesStr[i] = playerState.GetComponent<LeverInteractable>().GetLeverState();
            i++;
        }
    }

    private void GenerateStates()
    {
        for(int i = 0; i < playerStatesObj.Length; i++)
        {
            int rnd = Random.Range(1, 4);
            if (rnd == 1)
            {
                statesObj[i].GetComponent<StateController>().SetState("up");
                states[i] = "up";
            }
            else if (rnd == 2)
            {
                statesObj[i].GetComponent<StateController>().SetState("down");
                states[i] = "down";
            }
            else
            {
                statesObj[i].GetComponent<StateController>().SetState("none");
                states[i] = "none";
            }
        }

        match = false;
    }
}
