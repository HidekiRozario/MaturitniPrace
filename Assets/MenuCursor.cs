using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCursor : MonoBehaviour
{
    [SerializeField] GameObject button = null;

    public void OnClick()
    {
        if(button != null)
        {
            button.GetComponent<MenuButtons>().ButtonOnClick();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Button-Menu")
        {
            button = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Button-Menu")
        {
            button = null;
        }
    }
}
