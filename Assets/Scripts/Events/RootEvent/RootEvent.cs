using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootEvent : Event
{
    [SerializeField] private GameObject rootsPrefab;
    [SerializeField] private Transform[] rootsPlaces;
    [SerializeField] private int eventScore = 1000;
    [SerializeField] private GameObject[] tasks;
    [SerializeField] private List<Collider> taskColliders;

    private bool isFixed = true;
    private List<GameObject> roots;

    public override void SetActive(bool _state)
    {
        if (_state)
        {
            isFixed = false;
            GenerateRoots();
        }
        else
        {
            foreach (Collider coll in taskColliders)
            {
                coll.enabled = true;
            }
            gc.AddScore(eventScore);
        }

        base.SetActive(_state);
    }

    private void GenerateRoots()
    {
        if (taskColliders.Count <= 0)
        {
            tasks = GameObject.FindGameObjectsWithTag("Task-Collider");

            foreach (GameObject task in tasks)
            {
                taskColliders.AddRange(task.GetComponents<Collider>());
            }
        }

        roots = new List<GameObject>();

        for (int i = 0; i < rootsPlaces.Length; i++)
        {
            GameObject _roots = Instantiate(rootsPrefab, rootsPlaces[i].position, rootsPlaces[i].rotation);
            foreach (Transform child in _roots.transform)
            {
                roots.Add(child.gameObject);
            }
        }

        foreach(Collider coll in taskColliders)
        {
            coll.enabled = false;
        }
    }

    private void Update()
    {
        if(GetActive() && isFixed)
        {
            GenerateRoots();
        }

        if (GetActive())
        {
            isFixed = true;

            foreach (GameObject root in roots)
            {
                if (root != null)
                    isFixed = false;
            }

            if (isFixed)
                SetActive(false);
        }
    }
}
