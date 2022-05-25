using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TwistFix : XRSimpleInteractable
{
    private float x;
    private float z;

    void Start()
    {
        x = transform.rotation.eulerAngles.x;
        z = transform.rotation.eulerAngles.z;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(x, transform.rotation.eulerAngles.y, z);
    }
}
