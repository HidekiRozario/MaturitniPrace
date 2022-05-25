using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetActiveObject : MonoBehaviour
{
    [SerializeField] private GameObject objectToSet;

    public void SetObjectActive(bool _state) => objectToSet.SetActive(_state);
}
