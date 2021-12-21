using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Pattern", menuName = "Games/ElectricityGame", order = 1)]
public class PatternObject : ScriptableObject
{
    public GameObject[] boardPattern;
}
