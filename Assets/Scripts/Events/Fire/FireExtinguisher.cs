using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class FireExtinguisher : XRGrabInteractable
{
    private bool isActivated = false;

    [SerializeField] private ParticleSystem particles;
    [SerializeField] private float distance = 0.25f;
    [SerializeField] private string tagHit = "Event-Fire";
    [SerializeField] private Transform shootPoint;

    private void Update()
    {
        if (isActivated)
        {
            particles.Play();

            RaycastHit hit;

            if(Physics.Raycast(shootPoint.position, shootPoint.forward, out hit, distance))
            {
                if (hit.collider.tag == tagHit)
                {
                    Destroy(hit.collider.gameObject);
                }
            }
        }
        else
        {
            particles.Stop();
        }
    }

    protected override void OnActivated(ActivateEventArgs args)
    {
        isActivated = true;

        base.OnActivated(args);
    }

    protected override void OnDeactivated(DeactivateEventArgs args)
    {
        isActivated = false;
        base.OnDeactivated(args);
    }
}
