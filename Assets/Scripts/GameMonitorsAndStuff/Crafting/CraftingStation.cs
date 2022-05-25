using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CraftingStation : MonoBehaviour
{
    private int resourceCount = 0;

    [SerializeField] private int resourcePrice = 3;
    [SerializeField] private string resourceTag = "Crafting-Resource";
    [SerializeField] private TMP_Text resourceText;
    [SerializeField] private TMP_Text costText;
    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private Transform itemSpawn;

    // Update is called once per frame
    void Update()
    {
        resourceText.text = "Resources: " + resourceCount;
        costText.text = "Cost: " + resourcePrice;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == resourceTag)
        {
            resourceCount += other.gameObject.GetComponent<Item>().resourceCount;
            Destroy(other.gameObject);
        }
    }

    public void CraftItem()
    {
        if (resourceCount >= resourcePrice)
        {
            Instantiate(itemPrefab, itemSpawn.position, Quaternion.identity);
            resourceCount -= resourcePrice;
        }
    }
}
