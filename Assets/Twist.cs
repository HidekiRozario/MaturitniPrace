using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Twist : MonoBehaviour
{
    [SerializeField] private Twister twister;

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Twister-Charge")
        {
            twister.SetCharging(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Twister-Charge")
        {
            twister.SetCharging(false);
        }
    }
}
