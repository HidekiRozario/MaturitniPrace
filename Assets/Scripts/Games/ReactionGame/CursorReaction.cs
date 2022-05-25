using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorReaction : MonoBehaviour
{
    public bool isTouching = false;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Cursor-Reaction")
        {
            isTouching = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Cursor-Reaction")
        {
            isTouching = false;
        }
    }
}
