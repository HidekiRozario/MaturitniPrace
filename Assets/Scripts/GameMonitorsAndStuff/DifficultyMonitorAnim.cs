using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DifficultyMonitorAnim : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private GameController gc;
    [SerializeField] private TMP_Text diffText;

    private void Start()
    {
        gc = GameObject.Find("GameController").GetComponent<GameController>();
    }

    private void Update()
    {
        if (gc.enabled)
        {
            this.gameObject.SetActive(false);
        }
        else
        {
            if (gc.GetDifficulty() == 1)
                diffText.text = "Difficulty: Kitten";
            else if (gc.GetDifficulty() == 2)
                diffText.text = "Difficulty: Home Cat";
            else
                diffText.text = "Difficulty: Stray Cat";
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            anim.SetBool("isPlayerClose", true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            anim.SetBool("isPlayerClose", false);
        }
    }
}
