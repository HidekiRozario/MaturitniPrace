using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimonSaysButton : MonoBehaviour
{
    // Start is called before the first frame update
    private ButtonSystem btnScript;
    public bool isChanged = false;
    [SerializeField] private SimonSays simonSays;

    private void Start()
    {
        btnScript = GetComponent<ButtonSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (btnScript.isActive)
        {
            simonSays.ButtonPress(btnScript.btnIds);
            btnScript.isActive = false;
        }
    }
}
