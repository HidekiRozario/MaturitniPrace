using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialController : MonoBehaviour
{
    [SerializeField] private Animator platformAnim;
    [SerializeField] private GameObject[] tasks;
    [SerializeField] private GameObject[] taskTutorials;

    private int taskPos = 0;
    private int taskPosLast = 0;
    private bool animFinished = true;

    public void AnimFinished()
    {
        animFinished = true;
        taskTutorials[taskPosLast].SetActive(false);
        taskTutorials[taskPos].SetActive(true);

        GetComponent<BoxCollider>().enabled = false;

        taskPosLast = taskPos;
    }

    public void NextTask(int i)
    {
        if (taskPos + i < tasks.Length && taskPos + i > 0 && animFinished)
        {
            taskPos += i;
            platformAnim.Play("TasksUp");
            animFinished = false;
            GetComponent<BoxCollider>().enabled = true;
        }
    }

    public void BreakTask()
    {
        tasks[taskPos].GetComponent<Game>().SetBroken(true);
    }

    public void ChangeTask()
    {
        tasks[taskPosLast].SetActive(false);
        tasks[taskPos].SetActive(true);
    }
}
