using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TaskMonitor : MonoBehaviour
{
    [SerializeField] private int taskCount = 4;

    [SerializeField] private Color taskOn;
    [SerializeField] private Color taskOff;
    [SerializeField] private Color taskDead;

    [SerializeField] private TMP_Text[] monitorTasks;
    [SerializeField] private Image[] monitorStates;

    [SerializeField] private GameController gc;

    [SerializeField] private Game[] tasks;

    private void Awake()
    {
        taskCount = taskCount + 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (tasks.Length < 1)
        {
            tasks = gc.GetTasks();
            taskCount = tasks.Length;

            for (int i = 0; i < monitorTasks.Length; i++)
            {
                if (i < taskCount)
                    monitorTasks[i].text = tasks[i].GetTaskName();
                else
                {
                    monitorTasks[i].text = "None";
                    monitorStates[i].color = taskDead;
                }
            }
        }

        for (int i = 0; i < taskCount; i++)
        {
            if (!tasks[i].GetDestroyed())
            {
                if (!tasks[i].GetBroken())
                    monitorStates[i].color = taskOn;
                else if (tasks[i].GetBroken())
                    monitorStates[i].color = taskOff;
            }
            else
            {
                monitorStates[i].color = taskDead;
            }
        }
    }
}
