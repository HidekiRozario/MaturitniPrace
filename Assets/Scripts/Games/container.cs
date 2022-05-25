using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class container : MonoBehaviour
{
    public int score;
    public int tasksRepaired;
    public string levelName;

    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
