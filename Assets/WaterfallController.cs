using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterfallController : MonoBehaviour
{
    [SerializeField] private Game[] levelTasks;
    [SerializeField] private float speed = 5f;
    [SerializeField] private Vector3 endY;
    [SerializeField] private AudioSource waterfallSound;
    private Vector3 startY;
    bool broken = false;

    private void Start()
    {
        startY = transform.position;
    }

    private void FixedUpdate()
    {
        broken = false;

        foreach (Game task in levelTasks)
        {
            if (task.GetBroken() || task.GetDestroyed())
            {
                broken = true;
            }
        }

        if (broken)
        {
            transform.position = Vector3.MoveTowards(transform.position, endY, speed * Time.deltaTime);
            waterfallSound.enabled = false;
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, startY, speed * Time.deltaTime);
            waterfallSound.enabled = true;
        }
    }
}
