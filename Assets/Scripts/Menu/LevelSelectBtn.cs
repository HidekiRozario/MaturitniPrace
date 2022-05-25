using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectBtn : MenuButtons
{
    [SerializeField] private int sceneIndex = 0;
    public override void Update()
    {
        if(isActivate && isClicked)
        {
            SceneManager.LoadScene(sceneIndex, LoadSceneMode.Single);
            isClicked = false;
        }

        base.Update();
    }
}
