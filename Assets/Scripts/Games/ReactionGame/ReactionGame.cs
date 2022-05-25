using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactionGame : Game
{
    [SerializeField] private Transform hit1Offset;
    [SerializeField] private Transform hit2Offset;
    [SerializeField] private Transform hit3Offset;

    [SerializeField] private float[] hit1MaxOffset;
    [SerializeField] private float[] hit2MaxOffset;
    [SerializeField] private float[] hit3MaxOffset;

    [SerializeField] private Transform cursor1Offset;
    [SerializeField] private Transform cursor2Offset;
    [SerializeField] private Transform cursor3Offset;

    private Vector3 cursor1Reset;

    [SerializeField] private CursorReaction[] hitColliders;

    private bool goingRight = true;
    private int round = 0;
    private float rotationDone = 0;
    private bool canMove = false;
    [SerializeField] private float speed = 2f;

    public void Start()
    {
        cursor1Reset = cursor1Offset.localPosition;
        speed = speed * (difficulty);
    }

    public override void Update()
    {
        if(round >= 4)
        {
            SetBroken(false);
            SetWasBroken(false);
            round = 0;
        }

        if(wasBroken != isBroken)
        {
            round++;
            canMove = true;
            SetWasBroken(isBroken);
            RestartGame();
        }

        if (isBroken && canMove)
        {
            switch (round)
            {
                case 1:
                    if (cursor1Offset.localPosition.z > hit1MaxOffset[0] && goingRight)
                    {
                        goingRight = false;
                    }
                    else if(cursor1Offset.localPosition.z < hit1MaxOffset[1] && !goingRight) 
                        goingRight = true;

                    if (goingRight)
                        cursor1Offset.localPosition = new Vector3(cursor1Offset.localPosition.x, cursor1Offset.localPosition.y, cursor1Offset.localPosition.z + (hit1MaxOffset[0] * Time.deltaTime * speed));
                    else
                        cursor1Offset.localPosition = new Vector3(cursor1Offset.localPosition.x, cursor1Offset.localPosition.y, cursor1Offset.localPosition.z + (hit1MaxOffset[1] * Time.deltaTime * speed));
                    break;

                case 2:
                    cursor2Offset.Rotate(new Vector3(-speed * Time.deltaTime * 50f, 0, 0), Space.Self);
                    break;

                case 3:
                    if (rotationDone > 180f && goingRight)
                    {
                        goingRight = false;
                        rotationDone = 0;
                    }
                    else if (rotationDone > 180f && !goingRight)
                    {
                        goingRight = true;
                        rotationDone = 0;
                    }

                    if (goingRight)
                        cursor3Offset.Rotate(new Vector3(-speed * Time.deltaTime * 100f, 0, 0), Space.Self);
                    else
                        cursor3Offset.Rotate(new Vector3(speed * Time.deltaTime * 100f, 0, 0), Space.Self);

                    rotationDone += speed * Time.deltaTime * 100f;
                    break;
            }
        }

        base.Update();
    }

    public void GameCheck()
    {
        canMove = false;

        if (round >= 3 && hitColliders[round - 1].isTouching)
        {
            round++;
            goingRight = true;
            return;
        } else if(round >= 3 && !hitColliders[round - 1].isTouching)
        {
            RestartGame();
            return;
        }

        if (hitColliders[round - 1].isTouching)
        {
            round++;
            canMove = true;
            goingRight = true;
        } 
        else
        {
            RestartGame();
        }
    }

    private void RestartGame()
    {
        if (this.enabled)
        {
            round = 1;
            goingRight = true;
            rotationDone = 0f;
            canMove = true;

            cursor1Offset.localPosition = cursor1Reset;

            cursor2Offset.localRotation = Quaternion.Euler(0, 0, 0);
            cursor3Offset.localRotation = Quaternion.Euler(180f, 0, 0);

            hit1Offset.localPosition = new Vector3(hit1Offset.localPosition.x, hit1Offset.localPosition.y, Random.Range(hit1MaxOffset[0], hit1MaxOffset[1]));
            hit2Offset.localRotation = Quaternion.Euler(Random.Range(hit2MaxOffset[0], hit2MaxOffset[1]), 0, 0);
            hit3Offset.localRotation = Quaternion.Euler(Random.Range(hit3MaxOffset[0], hit3MaxOffset[1]), 0, 0);
        }
    }
}
