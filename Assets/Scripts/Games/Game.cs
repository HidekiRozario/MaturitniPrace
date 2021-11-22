using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    private int hp = 100;
    protected int difficulty = 1; // 1-easy 2-medium 3-hard

    [SerializeField] protected bool isBroken = false;
    protected bool wasBroken = false;
    protected bool isDestroyed = false;

    public void SetDifficulty(int _difficulty)
    {
        difficulty = _difficulty;
    }

    public void SetBroken(bool _isBroken)
    {
        isBroken = _isBroken;
    }

    public void SetWasBroken(bool _wasBroken)
    {
        wasBroken = _wasBroken;
    }
}
