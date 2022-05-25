using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CustomDetachGrabInteractible : MonoBehaviour
{
    XRGrabInteractable interactible;
    [SerializeField] private float distance = 1f;

    private void Start()
    {
        interactible = GetComponent<XRGrabInteractable>();
    }

    private void Update()
    {
        if (interactible.selectingInteractor != null)
        {
            if (Vector3.Distance(transform.position, interactible.selectingInteractor.transform.position) > distance)
            {
                interactible.enabled = false;
            }
        } 
        else
        {
            interactible.enabled = true;
        }
    }

}
