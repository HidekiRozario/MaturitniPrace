using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverInteractable : MonoBehaviour
{
    private bool activatedUp = false;
    private bool activatedDown = false;
    private bool isGrabbed = false;

    [SerializeField] private Material activatedUpMat;
    [SerializeField] private Material activatedDownMat;
    [SerializeField] private Material deactivatedMat;
    private MeshRenderer rend;

    private void Start()
    {
        rend = GetComponent<MeshRenderer>();
    }

    void Update()
    {
        if(transform.localRotation.eulerAngles.x >= 25 && transform.localRotation.eulerAngles.x <= 40 && !isGrabbed)
        {
            activatedUp = true;
            activatedDown = false;
            rend.material = activatedUpMat;
        } else if (transform.localRotation.eulerAngles.x <= 345 && transform.localRotation.eulerAngles.x >= 325 && !isGrabbed)
        {
            activatedDown = true;
            activatedUp = false;
            rend.material = activatedDownMat;
        } else
        {
            activatedDown = false;
            activatedUp = false;
            rend.material = deactivatedMat;
        }
    }

    public void Grabbed() => isGrabbed = !isGrabbed;
}
