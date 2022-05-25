using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private ShopItem preset;
    public string itemName = "";
    public int resourceCount = 0;

    private void Start()
    {
        itemName = preset.itemName;
        resourceCount = preset.resourceCount;
    }
}
