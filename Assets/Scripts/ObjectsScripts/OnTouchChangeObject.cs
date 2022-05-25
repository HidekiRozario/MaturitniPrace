using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTouchChangeObject : MonoBehaviour
{
    [SerializeField] private GameObject disable;
    [SerializeField] private GameObject enable;

    [SerializeField] private LayerMask toCollideWith;

    private void OnTriggerEnter(Collider coll)
    {
        if(toCollideWith == (toCollideWith | (1 << coll.gameObject.layer)))
        {
            disable.SetActive(false);
            enable.SetActive(true);
        }
    }
}
