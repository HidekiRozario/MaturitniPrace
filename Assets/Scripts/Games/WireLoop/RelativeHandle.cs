using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class RelativeHandle : XRSimpleInteractable
{
    bool isHolding = false;
    public bool canHold = false;

    private GameObject handPos;
    private Transform toolT;
    private Vector3 startPos;
    private Quaternion startRot;
    [SerializeField] private Transform endPos;

    private void Start()
    {
        toolT = transform.parent;
        startPos = transform.position;
        startRot = transform.rotation;
        EndTool();
    }

    private void Update()
    {
        if (isHolding)
        {
            transform.parent = handPos.transform;
        }
        else transform.parent = toolT;
    }

    public void ResetTool()
    {
        canHold = true;
        transform.position = startPos;
        transform.rotation = startRot;
        handPos = null;
    }

    public void EndTool()
    {
        isHolding = false;
        handPos = null;
        canHold = false;
        transform.position = endPos.position;
        transform.rotation = startRot;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Rail")
        {
            isHolding = false;
            handPos = null;
            transform.position = startPos;
            transform.rotation = startRot;
        }
        if (other.gameObject.tag == "End")
        {
            EndTool();
        }
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        if (handPos == null && canHold)
        {
            handPos = args.interactor.gameObject;
            isHolding = true;
            base.OnSelectEntered(args);
        }
    }
    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        handPos = null;
        isHolding = false;
        base.OnSelectExited(args);
    }
}
