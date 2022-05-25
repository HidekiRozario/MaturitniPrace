using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.Events;

public class Mouse : XRSimpleInteractable
{
    bool isHolding = false;
    public bool canHold = false;

    private GameObject handPos;
    private Transform toolT;
    private Transform hand;

    [SerializeField] private Vector3 startPos;
    [SerializeField] private GameObject cursor;
    [SerializeField] private UnityEvent onClick;

    [SerializeField] private float constraintX = 0.05f;
    [SerializeField] private float constraintZ = 0.05f;

    // Start is called before the first frame update
    void Start()
    {
        toolT = transform.parent;
        startPos = transform.position;
    }

    private void Update()
    {
        cursor.transform.localPosition = new Vector3(transform.localPosition.x * 2, transform.localPosition.z * 2, cursor.transform.localPosition.z);

        if (hand != null)
        {
            if (hand.position.x < startPos.x + constraintX && hand.position.x > startPos.x - constraintX)
                transform.position = new Vector3(hand.position.x, transform.position.y, transform.position.z);
            if (hand.position.z < startPos.z + constraintZ && hand.position.z > startPos.z - constraintZ)
                transform.position = new Vector3(transform.position.x, transform.position.y, hand.position.z);
        }
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        hand = args.interactor.transform;

        base.OnSelectEntered(args);
    }
    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        hand = null;
        base.OnSelectExited(args);
    }

    protected override void OnActivated(ActivateEventArgs args)
    {
        onClick.Invoke();
        base.OnActivated(args);
    }
}
