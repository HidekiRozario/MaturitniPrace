using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireScript : Event
{
    [SerializeField] private Transform[] fireLocations;
    [SerializeField] private GameObject[] firesTemplates;
    [SerializeField] private GameObject[] fires;
    [SerializeField] private LightSwitch light;
    [SerializeField] private int eventScore = 1500;

    bool isFixed = true;

    public override void SetActive(bool _state)
    {
        if (_state)
        {
            GenerateFlame();
        }
        else
        {
            gc.AddScore(eventScore);
        }

        base.SetActive(_state);
    }

    private void GenerateFlame()
    {
        isFixed = false;

        light.SwitchLighting(false);

        foreach(GameObject fire in fires)
        {
            Destroy(fire);
        }

        fires = new GameObject[fireLocations.Length];

        for (int i = 0; i < fireLocations.Length; i++)
        {
            fires[i] = Instantiate(firesTemplates[Random.Range(0, firesTemplates.Length)], fireLocations[i].position, Quaternion.identity);
        }
    }

    private void Update()
    {
        if(GetActive() && isFixed)
        {
            GenerateFlame();
        }

        if (GetActive())
        {
            isFixed = true;
            foreach (GameObject fire in fires)
            {
                if (fire != null)
                    isFixed = false;
            }

            if (isFixed)
                SetActive(false);
        }
    }
}
