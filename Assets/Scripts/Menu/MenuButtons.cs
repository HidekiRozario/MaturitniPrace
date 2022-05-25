using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MenuButtons : MonoBehaviour
{
    public bool isClicked = false;

    public bool isActivate = false;

    [SerializeField] GameObject[] objectsToHide;
    [SerializeField] GameObject[] objectsToShow;

    [SerializeField] GameObject selectionPaws;

    [SerializeField] private UnityEvent onClick;

    // Update is called once per frame
    public virtual void Update()
    {
        if (isClicked)
        {
            isClicked = false;
            if (!isActivate)
            {
                foreach(GameObject obj in objectsToHide)
                {
                    obj.SetActive(false);
                }

                foreach (GameObject obj in objectsToShow)
                {
                    obj.SetActive(true);
                }
            }
            else
            {
                onClick.Invoke();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Cursor-Menu" && selectionPaws != null)
        {
            selectionPaws.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Cursor-Menu" && selectionPaws != null)
        {
            selectionPaws.SetActive(false);
        }
    }

    public virtual void ButtonOnClick()
    {
        if (!isClicked)
        {
            isClicked = true;
        }
    }
}
