using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Twister : Game
{
    private float energy = 0;
    private int levelState = 0;
    private bool isCharging = false;

    [SerializeField] private Transform[] chargeZones;
    [SerializeField] private Transform chargeVis;
    [SerializeField] private Material charged;
    [SerializeField] private Material dead;
    [SerializeField] private Vector3 chargeZoneNext;
    [SerializeField] private float energySpeed = 1f;

    [SerializeField] private float energyDrain = 5f;
    [SerializeField] private float energyCharge = 5f;

    public override void Awake()
    {
        base.Awake();

        chargeZoneNext.y = chargeZones[levelState].localRotation.eulerAngles.y;
    }

    private void Start()
    {
        energySpeed += difficulty * 10;
        energyDrain += difficulty * 10;
    }

    public override void Update()
    {
        base.Update();

        if(isBroken != wasBroken)
        {
            chargeZones[0].GetComponentInChildren<BoxCollider>().enabled = true;
            chargeZones[0].transform.GetChild(0).GetComponent<MeshRenderer>().material = dead;
            for (int i = 1; i < chargeZones.Length; i++)
            {
                chargeZones[i].transform.GetChild(0).GetComponent<MeshRenderer>().material = dead;
                chargeZones[i].GetComponentInChildren<BoxCollider>().enabled = false;
            }
            levelState = 0;
            energy = 0;
            SetWasBroken(true);
        }

        if (isBroken && !isDestroyed)
        {
            Debug.Log(chargeZones[levelState].localRotation.eulerAngles.x + " + " + chargeZones[levelState].localRotation.eulerAngles.y + " + " + chargeZones[levelState].localRotation.eulerAngles.z);

            chargeVis.localScale = new Vector3(1, 1, energy / 100);

            if (chargeZones[levelState].localRotation.eulerAngles.y <= chargeZoneNext.y + 5f && chargeZones[levelState].localRotation.eulerAngles.y >= chargeZoneNext.y - 5f)
                chargeZoneNext = new Vector3(chargeZones[levelState].localRotation.eulerAngles.x, Random.Range(0f, 180f), chargeZones[levelState].localRotation.eulerAngles.z);

            if(chargeZones[levelState].localRotation.eulerAngles.y - chargeZoneNext.y >= 0f)
                chargeZones[levelState].localRotation = Quaternion.Euler(chargeZones[levelState].localRotation.eulerAngles.x, chargeZones[levelState].localRotation.eulerAngles.y - energySpeed * Time.deltaTime, chargeZones[levelState].localRotation.eulerAngles.z);
            else
                chargeZones[levelState].localRotation = Quaternion.Euler(chargeZones[levelState].localRotation.eulerAngles.x, chargeZones[levelState].localRotation.eulerAngles.y + energySpeed * Time.deltaTime, chargeZones[levelState].localRotation.eulerAngles.z);

            if (!isCharging && energy > 0)
                energy -= energyDrain * Time.deltaTime;
            else if(isCharging)
                energy += energyCharge * Time.deltaTime;

            if (energy >= 100 && levelState == chargeZones.Length - 1)
            {
                chargeZones[levelState].transform.GetChild(0).GetComponent<MeshRenderer>().material = charged;
                SetBroken(false);
                SetWasBroken(false);
            }
            else if (energy >= 100 && levelState + 1 < chargeZones.Length)
            {
                chargeZones[levelState].GetComponentInChildren<BoxCollider>().enabled = false;
                chargeZones[levelState].transform.GetChild(0).GetComponent<MeshRenderer>().material = charged;
                chargeZones[levelState + 1].GetComponentInChildren<BoxCollider>().enabled = true;
                isCharging = false;
                levelState++;
                energy = 0;
            }
        }

    }

    public void SetCharging(bool _state)
    {
        isCharging = _state;
    }
}
