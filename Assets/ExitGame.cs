using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitGame : MonoBehaviour
{
    private Quaternion startRot;
    [SerializeField] private float offsetToQuit = 45f;

    private void Start()
    {
        startRot = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.rotation.eulerAngles.y <= startRot.eulerAngles.y - offsetToQuit)
        {
            Debug.Log("Close");
            Application.Quit();
        }
    }
}
