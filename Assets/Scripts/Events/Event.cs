using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event : MonoBehaviour
{
    [SerializeField] private bool isActive = false;
    [SerializeField] public GameController gc;

    public virtual void SetActive(bool _state) => isActive = _state;
    public bool GetActive() => isActive;
}
