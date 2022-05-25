using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightsOutPiece : MonoBehaviour
{
    [SerializeField] private LightsOutPiece[] neighbours;
    public bool isOn = false;
    [SerializeField] private MeshRenderer rend;
    [SerializeField] private Material on;
    [SerializeField] private Material off;
    private int colliders = 0;

    private void Awake()
    {
        rend = GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        if (isOn)
            rend.material = on;
        else
            rend.material = off;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(LayerMask.LayerToName(other.gameObject.layer) == "PlayerHandsCollision")
        {
            colliders++;
            if(colliders == 1)
            touched();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (LayerMask.LayerToName(other.gameObject.layer) == "PlayerHandsCollision")
        {
            colliders--;
        }
    }

    public void touched()
    {
        isOn = !isOn;
        foreach(LightsOutPiece piece in neighbours)
        {
            piece.isOn = !piece.isOn;
        }
    }
}
