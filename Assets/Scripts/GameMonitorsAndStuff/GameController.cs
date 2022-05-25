using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    //----------------Generation----------------------
    private int difficulty = 1; //1 - easy, 2 - medium, 3 - hard
    [SerializeField] string levelName = "";

    [SerializeField] private GameObject[] defaultTasks;
    [SerializeField] private Game[] levelTasks;
    [SerializeField] private GameObject[] randomTasks;
    [SerializeField] private Game[] tasks;
    [SerializeField] private Event[] gameEvents;
    [SerializeField] private int fireEventIndex = 0;

    [SerializeField] private Transform[] taskPlaces;
    //------------------------------------------------
    //-----------------GameLoopStuff------------------
    [SerializeField] private int chanceToBreak = 5;
    [SerializeField] private bool destroyDEBUG = false;
    private bool gameOver = false;
    private bool gameOverDone = false;
    private float time = 0;

    [SerializeField] private float breakMult = 1.025f;
    [SerializeField] private float breakTime = 5f;
    [SerializeField] private float breakTimeSharpness = 0.1f;
    [SerializeField] private float breakTimeStartOffset = -0.2f;

    private float breakDeltaTime = 0;
    //------------------------------------------------
    //------------------ScalingSystem-----------------
    [SerializeField] private float damageMult = 1f;
    [SerializeField] private float damageMultSharpness = 2f; //Every "damageMultSharpness" seconds one HP+ to damageMult
    [SerializeField] private float cooldownDeltaTime = 10f;
    private float difficultyScaleDeltaTime = 0;
    //------------------------------------------------
    //-----------------ScoreSystem--------------------
    [SerializeField] private float score = 0;
    [SerializeField] private int tasksRepaired = 0;
    private float scoreMultiplier = 1f;

    [SerializeField] private bool[] tasksStates;

    [SerializeField] private container container;
    //------------------------------------------------
    //---------------------EVENTS---------------------
    private float eventTime;
    [SerializeField] private float maxEventTime = 60f;
    [SerializeField] private float minEventTime = 15f;
    [SerializeField] private GameObject[] alarmLights;
    //-----------------------------------------------
    [SerializeField] private GameOverAnimatorController gmo;

    private void Start()
    {
        chanceToBreak += difficulty;
        tasksStates = new bool[taskPlaces.Length + levelTasks.Length];
        container = GameObject.Find("container").GetComponent<container>();

        foreach (Game lv in levelTasks)
        {
            lv.enabled = true;
        }

        StartGame();

        switch (difficulty)
        {
            case 1:
                breakTimeSharpness = 0.02f;
                chanceToBreak = 5;
                break;
            case 2:
                breakTimeSharpness = 0.03f;
                chanceToBreak = 4;
                break;
            case 3:
                breakTimeSharpness = 0.04f;
                chanceToBreak = 3;
                break;
        }

    }

    private void Update()
    {
        //----------------DEBUG--------------------
        if (destroyDEBUG)
        {
            foreach(Game g in tasks)
            {
                g.SetDestroyed(true, true);
            }
        }
        //--------------GameLoopStuff-----------------
        //--------------------------------------------
        time += Time.deltaTime;
        //------------------Timers----------------------
        if(breakDeltaTime > 0)
        {
            breakDeltaTime -= Time.deltaTime;
        }

        if(difficultyScaleDeltaTime > 0)
        {
            difficultyScaleDeltaTime -= Time.deltaTime;
        }
        //-----------------------------------------------

        //--------------Break Task Check------------------
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

            //Checks if task can get broken
            while (!isOK)
            {
                rndTask = Random.Range(0, tasks.Length);
                if(!tasks[rndTask].GetBroken() && !tasks[rndTask].GetDestroyed())
                {
                    isOK = true;
                }
            }

            //Breaks a task after break check
            if(rndBreak == 1 && isOK && !tasks[rndTask].GetBroken())
            {
                tasks[rndTask].SetBroken(true);
            }

            breakDeltaTime = breakTime;
        }
        //----------------------------------------------

        //----------------GameOverCheck------------------
        int tasksCheck = 0;

        foreach(Game task in tasks)
        {
            //GAMEOVER BASED ON DIFFICULTY
            if (!task.GetDestroyed())
            {
                tasksCheck++;
            }
        }

        if(tasksCheck <= difficulty + 1)
        {
            foreach (GameObject alarmLight in alarmLights)
                alarmLight.SetActive(true);
        }

        if (tasksCheck > difficulty)
            gameOver = false;
        else 
            gameOver = true;
        //---------------------------------------------------
        //--------------TaskDamagingAndScaling---------------
        breakTime = (1 / (((time / 60) * breakTimeSharpness) - breakTimeStartOffset)) + 3f;

        if (difficultyScaleDeltaTime <= 0)
        {
            damageMult = ((time / 60) / damageMultSharpness) + damageMult;
            foreach (Game task in tasks)
            {
                task.SetHpMultiplier(damageMult);
            }
        }
        //---------------------------------------
        //------------ScoreCalculation-----------
        if (!gameOver)
            ScoreCalculation();
        else if (!gameOverDone)
        {
            foreach (GameObject alarmLight in alarmLights)
                alarmLight.SetActive(true);
            GameOver();
        }
        //---------------------------------------

        //---------------------------------------
        //----------------EVENTS-----------------
        bool eventEnded = true;

        foreach(Event gameEvent in gameEvents)
        {
            if(gameEvent.GetActive())
            eventEnded = false;
        }

        if (eventEnded && tasksCheck > difficulty + 1)
        {
            foreach (GameObject alarmLight in alarmLights)
                alarmLight.SetActive(false);
        }

        if (!eventEnded && gameEvents[fireEventIndex].GetActive())
        {
            foreach(Game task in tasks)
            {
                task.SetBroken(true, true);
            }
        }

        if (eventTime <= 0 && eventEnded)
        {
            StartEvent();
            eventTime = Random.Range(minEventTime, maxEventTime);
        }
        else if(eventEnded)
        {
            eventTime -= Time.deltaTime;
        }
        //---------------------------------------
    }

    private void StartEvent()
    {
        foreach (GameObject alarmLight in alarmLights)
            alarmLight.SetActive(true);
        gameEvents[Random.Range(0, gameEvents.Length)].SetActive(true);
    }

    private void GameOver()
    {
        gameOverDone = true;
        container.score = Mathf.Abs((int)score);
        container.tasksRepaired = tasksRepaired;
        container.levelName = levelName;
        //----TO-DO ANIMATION--------

        //---------------------------
        //WHEN ANIMATION END GO TO MENU
        gmo.GameOver();
    }

    private void ScoreCalculation()
    {
        score += Time.deltaTime * scoreMultiplier * 10;

        if (difficultyScaleDeltaTime <= 0)
        {
            scoreMultiplier = damageMult * (1 + difficulty / 50f);
            difficultyScaleDeltaTime = cooldownDeltaTime;
        }


        for (int i = 0; i < tasks.Length; i++)
        {
            if (tasksStates[i] != tasks[i].GetBroken() && tasksStates[i] && !tasks[i].GetBroken() && !tasks[i].GetDestroyed())
            {
                AddScore(tasks[i].score);
                tasksRepaired++;
            }

            tasksStates[i] = tasks[i].GetBroken();
        }
    }

    public void AddScore(int _scoreToAdd)
    {
        score += (_scoreToAdd * scoreMultiplier);
    }

    public float GetScore() => score;

    void StartGame()
    {
        eventTime = maxEventTime;

        for(int h = 0; h < tasksStates.Length; h++)
        {
            tasksStates[h] = false;
        }    

        tasks = new Game[taskPlaces.Length + levelTasks.Length];
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

        int Lcount = tasks.Length - 1;

        foreach(Game Ltask in levelTasks)
        {
            tasks[Lcount] = Ltask;
            Lcount--;
        }

        foreach (Game task in tasks)
        {
            task.SetDifficulty(difficulty);
        }
    }

    public Game[] GetTasks() => tasks;

    public void SetDifficulty(int _diff) => difficulty = _diff;

    public int GetDifficulty() => difficulty;
}
