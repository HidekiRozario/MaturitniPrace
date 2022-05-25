using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopController : MonoBehaviour
{
    private GameController gc;

    [SerializeField] private TMP_Text moneyText;
    [SerializeField] private TMP_Text selectedText;
    [SerializeField] private TMP_Text selectedPrice;
    [SerializeField] private Image selectedImage;
    [SerializeField] private ShopItem[] items;
    [SerializeField] private Transform itemShopTransform;

    private ShopItem selectedItem;
    private int selectedIndex = 0;

    void Start()
    {
        gc = GameObject.Find("GameController").GetComponent<GameController>();
        selectedItem = items[selectedIndex];
        selectedText.text = selectedItem.itemName;
        selectedImage.sprite = selectedItem.itemImage;
        selectedPrice.text = selectedItem.itemPrice.ToString() + "$";
    }

    void Update()
    {
        moneyText.text = "Money: " + gc.GetScore() + "$";
    }

    public void NextItem(int _nextIndex)
    {
        if (selectedIndex + _nextIndex >= 0 && selectedIndex + _nextIndex < items.Length)
            selectedIndex += _nextIndex;
        else if (selectedIndex + _nextIndex < 0)
            selectedIndex = items.Length - 1;
        else if (selectedIndex + _nextIndex >= items.Length)
            selectedIndex = 0;

        selectedItem = items[selectedIndex];
        selectedText.text = selectedItem.itemName;
        selectedImage.sprite = selectedItem.itemImage;
        selectedPrice.text = selectedItem.itemPrice.ToString() + "$";
    }

    public void BuyItem()
    {
        if (gc.GetScore() >= selectedItem.itemPrice)
        {
            Instantiate(selectedItem.itemPrefab, itemShopTransform.position, Quaternion.identity);
            gc.AddScore((int)-selectedItem.itemPrice);
        }
    }
}
