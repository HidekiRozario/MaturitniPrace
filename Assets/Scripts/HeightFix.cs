using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeightFix : MonoBehaviour
{
    private CharacterController playerCont;

    [SerializeField] private Transform playerCam;

    void Start()
    {
        playerCont = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        playerCont.height = playerCam.localPosition.y;
        playerCont.center = new Vector3(playerCam.localPosition.x, playerCam.localPosition.y / 2, playerCam.localPosition.z);
    }
}
