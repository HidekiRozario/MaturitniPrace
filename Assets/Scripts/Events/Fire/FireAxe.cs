using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class FireAxe : XRGrabInteractable
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] audioClips;
    [SerializeField] private string rootTag = "Event-Root";

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == rootTag && collision.gameObject != null)
        {
            Destroy(collision.gameObject);
            audioSource.clip = audioClips[Random.Range(0, audioClips.Length)];
            audioSource.Play();
        }
    }
}
