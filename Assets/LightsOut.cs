using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightsOut : Game
{
    [SerializeField] private LightsOutPiece[] pieces;

    public override void Update()
    {
        base.Update();

        if(isBroken != wasBroken && !wasBroken)
        {
            SetWasBroken(isBroken);
            StartGame();
        }

        if (isBroken && !isDestroyed)
        {
            bool isDone = true;

            foreach(LightsOutPiece piece in pieces)
            {
                if (!piece.isOn)
                {
                    isDone = false;
                    break;
                }
            }

            if (isDone)
            {
                SetBroken(false);
                SetWasBroken(false);
            }
        }
    }

    private void StartGame()
    {
        for(int i = 0; i < Random.Range(5, 10); i++)
        {
            pieces[Random.Range(0, pieces.Length)].touched();
        }
    }
}
