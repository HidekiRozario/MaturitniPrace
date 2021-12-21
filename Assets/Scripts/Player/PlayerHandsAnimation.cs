using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandsAnimation : MonoBehaviour
{
    [SerializeField] private Animator leftHandAnim;
    [SerializeField] private Animator rightHandAnim;
    [SerializeField] private InputManager playerInput;

    // Start is called before the first frame update
    void Start()
    {
        playerInput = GetComponentInChildren<InputManager>();
    }

    // Update is called once per frame
    void Update()
    {
        leftHandAnim.SetFloat("TriggerPress", playerInput.triggerButtonValueLeft);
        rightHandAnim.SetFloat("TriggerPress", playerInput.triggerButtonValueRight);
        leftHandAnim.SetFloat("GripPress", playerInput.selectButtonValueLeft);
        rightHandAnim.SetFloat("GripPress", playerInput.selectButtonValueRight);
    }
}
