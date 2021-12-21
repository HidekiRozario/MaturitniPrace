using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    //------------------------------------------------
    //----------------Generation----------------------
    private int difficulty = 1;

    [SerializeField] private GameObject[] defaultTasks;
    [SerializeField] private GameObject[] randomTasks;
    [SerializeField] private Game[] tasks;

    [SerializeField] private Transform[] taskPlaces;

    //------------------------------------------------

    //------------------------------------------------
    //-----------------GameLoopStuff------------------
    private int chanceToBreak;

    private float breakDeltaTime = 0;
    [SerializeField] private float breakTime = 5f;
    [SerializeField] private float breakMult = 1.025f;
    [SerializeField] private float damageMult = 1f;

    [SerializeField] private float cooldownDeltaTime = 10f;
    private float damageDeltaTime = 0;

    //------------------------------------------------
    //-----------------ScoreSystem--------------------
    [SerializeField] private float score = 0;
    [SerializeField] private int tasksRepaired = 0;
    private float scoreMultiplier = 1f;

    [SerializeField] private bool[] tasksStates;
    //------------------------------------------------




    private void Start()
    {
        chanceToBreak = 5 - difficulty;
        tasksStates = new bool[taskPlaces.Length];
        StartGame();
    }

    private void Update()
    {
        //----GameLoopStuff----
        if(breakDeltaTime > 0)
        {
            breakDeltaTime -= Time.deltaTime;
        }

        if(damageDeltaTime > 0)
        {
            damageDeltaTime -= Time.deltaTime;
        }

        if(breakDeltaTime <= 0)
        {
            int rndBreak = Random.Range(1, chanceToBreak);
            int rndTask = Random.Range(0, tasks.Length);
            bool isOK = false;

            foreach (Game task in tasks)
            {
                if (!task.GetDestroyed())
                {

                    if (task.GetBroken())
                    {
                        isOK = true;
                    }
                    else
                    {
                        isOK = false;
                        break;
                    }
                }
                else isOK = true;
            }

            while (!isOK)
            {
                rndTask = Random.Range(0, tasks.Length);
                if(!tasks[rndTask].GetBroken() && !tasks[rndTask].GetDestroyed())
                {
                    isOK = true;
                }
            }

            if(rndBreak == 1 && isOK && !tasks[rndTask].GetBroken())
            {
                tasks[rndTask].SetBroken(true);
            }

            breakDeltaTime = breakTime;
        }

        damageMult = (Time.deltaTime / 100) + damageMult;
        if (damageDeltaTime <= 0)
        {
            breakTime /= breakMult;

            foreach (Game task in tasks)
            {
                task.SetHpMultiplier(damageMult);
            }
        }
        //---------------------------------------
        //------------ScoreCalculation-----------
        score += Time.deltaTime * scoreMultiplier * 10;

        if(damageDeltaTime <= 0)
        {
            scoreMultiplier = damageMult * (1 + difficulty / 50f);
            damageDeltaTime = cooldownDeltaTime;
        }


        for(int i = 0; i < tasks.Length; i++)
        {
            if(tasksStates[i] != tasks[i].GetBroken() && tasksStates[i] && !tasks[i].GetBroken() && !tasks[i].GetDestroyed())
            {
                AddScore(tasks[i].score);
                tasksRepaired++;
            }

            tasksStates[i] = tasks[i].GetBroken();
        }
        //---------------------------------------
    }

    public void AddScore(int _scoreToAdd)
    {
        score += _scoreToAdd;
    }

    void StartGame()
    {
        for(int h = 0; h < tasksStates.Length; h++)
        {
            tasksStates[h] = false;
        }    

        tasks = new Game[taskPlaces.Length];
        randomTasks = new GameObject[taskPlaces.Length];

        GameObject taskRnd;

        bool done = false;
        int j = 0;

        while (!done) {
            taskRnd = defaultTasks[Random.Range(0, defaultTasks.Length)];
            bool found = false;

            foreach (GameObject task in randomTasks)
            {
                if (taskRnd == task)
                {
                    found = true;
                }
                if (task != null)
                {
                    done = true;
                }
                else done = false;
            }

            if (!found)
            {
                if(j < randomTasks.Length)
                randomTasks[j] = taskRnd;
                done = false;
                j++;
            } 
            
        }

        int i = 0;

        foreach(Transform place in taskPlaces)
        {
            tasks[i] = Instantiate(randomTasks[i], taskPlaces[i].position, taskPlaces[i].rotation).GetComponent<Game>();
            tasks[i].transform.Translate(-Mathf.Abs(taskPlaces[i].position.x - tasks[i].gameObject.GetComponent<Renderer>().bounds.center.x), -Mathf.Abs(taskPlaces[i].position.y - tasks[i].gameObject.GetComponent<Renderer>().bounds.center.y), Mathf.Abs(taskPlaces[i].position.z - tasks[i].gameObject.GetComponent<Renderer>().bounds.center.z), Space.Self);
            tasks[i].transform.Translate(tasks[i].spawnOffset, Space.Self);
            i++;
        }
    }
}
