using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireLoopEnds : MonoBehaviour
{
    public bool done = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Tool")
        {
            done = true;
        }
    }
}
