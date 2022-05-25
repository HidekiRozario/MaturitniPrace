using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LightSwitch : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip lightsOffAudio;
    [SerializeField] private AudioClip lightsOnAudio;
    [SerializeField] private GameObject mapLights;
    [SerializeField] private float offset = 10f;

    private void Update()
    {
        if (270 < transform.localRotation.eulerAngles.z && transform.localRotation.eulerAngles.z < 280)
        {
            audioSource.clip = lightsOnAudio;
            if (!mapLights.active)
                audioSource.Play();

            LightOnOff(true);
        }
        else if (90 > transform.localRotation.eulerAngles.z && transform.localRotation.eulerAngles.z > 80)
        {
            audioSource.clip = lightsOffAudio;
            if (mapLights.active)
                audioSource.Play();

            LightOnOff(false);
        }
    }

    public void LightOnOff(bool _state)
    {
        mapLights.SetActive(_state);
    }

    public void SwitchLighting(bool _state)
    {
        if (!_state)
            transform.localRotation = Quaternion.Euler(transform.localRotation.eulerAngles.x, transform.localRotation.eulerAngles.y, 85f);
        LightOnOff(_state);
    }
}
