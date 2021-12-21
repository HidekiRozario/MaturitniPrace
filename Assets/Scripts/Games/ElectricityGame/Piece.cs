using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    public bool up, left, right, down = false;
    public int rootI;
    public int rootJ;
    public int type = 0; // 1 - Straight, 2 - Curve, 3 - Cross, 4 - T
    [SerializeField] private MeshRenderer[] rends;
    [SerializeField] private Material wireOn;
    [SerializeField] private Material wireOff;

    public void SetWire(bool _on)
    {
        foreach(MeshRenderer rend in rends)
        if(_on)
            rend.material = wireOn;
        else
            rend.material = wireOff;
    }
}
