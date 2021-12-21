using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtons : MonoBehaviour
{
    public bool isClicked = false;

    public bool isActivate = false;

    [SerializeField] GameObject[] objectsToHide;
    [SerializeField] GameObject[] objectsToShow;

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
        }
    }

    public void ButtonOnClick()
    {
        if (!isClicked)
        {
            isClicked = true;
        }
    }
}
