using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class LeverInteractable : XRGrabInteractable
{
    private bool activatedUp = false;
    private bool activatedDown = false;
    private bool isGrabbed = false;

    [SerializeField] private Material activatedUpMat;
    [SerializeField] private Material activatedDownMat;
    [SerializeField] private Material deactivatedMat;
    private MeshRenderer rend;

    [SerializeField] private GameObject rightHandHolding;
    [SerializeField] private GameObject leftHandHolding;

    private void Start()
    {
        rend = GetComponent<MeshRenderer>();
    }

    public string GetLeverState()
    {
        if (activatedDown)
            return "down";
        if (activatedUp)
            return "up";
        else
            return "none";
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
        } else if (!isGrabbed)
        {
            activatedDown = false;
            activatedUp = false;
            rend.material = deactivatedMat;
        }

        if (!isGrabbed)
            GetComponent<Rigidbody>().isKinematic = true;
        else
            GetComponent<Rigidbody>().isKinematic = false;
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        if (!isGrabbed)
        {
            if (args.interactor.gameObject.tag == "RightHand")
            {
                rightHandHolding.SetActive(true);
                leftHandHolding.SetActive(false);
            }
            else if (args.interactor.gameObject.tag == "LeftHand")
            {
                rightHandHolding.SetActive(false);
                leftHandHolding.SetActive(true);
            }

            base.OnSelectEntered(args);
        }
        isGrabbed = true;
    }
    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        rightHandHolding.SetActive(false);
        leftHandHolding.SetActive(false);
        isGrabbed = false;

        base.OnSelectExited(args);
    }
}
