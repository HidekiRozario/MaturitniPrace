using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateController : MonoBehaviour
{
    [SerializeField] private Material activatedUpMat;
    [SerializeField] private Material activatedDownMat;
    [SerializeField] private Material notActivatedMat;

    [SerializeField] private MeshRenderer up;
    [SerializeField] private MeshRenderer down;

    private string stateStr = "none";

    public string GetState() => stateStr;

    public void SetState(string _state)
    {
        switch (_state)
        {
            case "up":
                up.material = activatedUpMat;
                down.material = notActivatedMat;
                break;
            case "down":
                up.material = notActivatedMat;
                down.material = activatedDownMat;
                break;
            case "none":
                up.material = notActivatedMat;
                down.material = notActivatedMat;
                break;
        }
    }
}
