using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundOnCollision : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] audioClips;
    [SerializeField] private LayerMask activeLayer;

    private void Start()
    {
        audioSource.enabled = false;
        StartCoroutine(LateCall(1));
    }

    IEnumerator LateCall(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        audioSource.enabled = true; 
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (audioSource.enabled && activeLayer == (activeLayer | (1 << collision.collider.gameObject.layer)))
        {
            audioSource.clip = audioClips[Random.Range(0, audioClips.Length)];
            audioSource.Play();
        }
    }
}
