using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private float hp = 100;
    [SerializeField] private float hpMax = 100;
    [SerializeField] private float hpRepairKit = 250;
    private float hpMultiplier = 1f;
    private float lastTime = 0;
    protected int difficulty = 1; // 1-easy 2-medium 3-hard

    [SerializeField] private string taskName;
    [SerializeField] public int score = 500;
    [SerializeField] protected bool isBroken = false;
    [SerializeField] private float cantBreakCooldown = 5f;
    [SerializeField] private MeshRenderer signal;
    [SerializeField] private Material signalOn;
    [SerializeField] private Material signalOff;
    [SerializeField] private Material signalDead;

    public Vector3 spawnOffset;

    protected bool wasBroken = false;
    [SerializeField] protected bool isDestroyed = false;
    private bool cantBreak = false;

    //-------------------------------------------------
    //---------------DamageVisualizer------------------
    [SerializeField] private Transform damageVis;

    public virtual void Awake()
    {
        lastTime = Time.time;
        hpMax = hp;
        damageVis = transform.Find("Visualizer").Find("DamageVisualizer").GetComponent<Transform>();
    }

    public virtual void Update()
    {
        if (Time.time - lastTime < cantBreakCooldown)
        {
            cantBreak = true;
        }
        else cantBreak = false;

        if(hp > 0)
        {
            damageVis.localScale = new Vector3(1, 1, hp / hpMax);
        }

        if (isBroken && !isDestroyed)
        {
            hp -= Time.deltaTime * hpMultiplier;

            signal.material = signalOff;
        }
        else if (!isBroken && !isDestroyed)
        {
            signal.material = signalOn;
        }
        else
        {
            signal.material = signalDead;
            this.enabled = false;
        }

        if(hp <= 0)
        {
            isDestroyed = true;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.collider.transform.tag == "RepairKit" && !isDestroyed)
        {
            if(hp + hpRepairKit > hpMax)
                hp = hpMax;
            else
                hp += hpRepairKit;

            Destroy(other.collider.gameObject);
        }
    }

    public void SetHpMultiplier(float _add) => hpMultiplier = _add;
    public bool GetBroken() => isBroken;

    public bool GetDestroyed() => isDestroyed;

    public void SetDifficulty(int _difficulty)
    {
        difficulty = _difficulty;
    }

    public void SetBroken(bool _isBroken)
    {
        if (isBroken)
        {
            lastTime = Time.time;
        }

        if (!cantBreak && !isBroken)
        {
            isBroken = _isBroken;
        }
        else isBroken = false;
    }

    public void SetBroken(bool _isBroken, bool onlyBreak)
    {
        isBroken = _isBroken;
    }

    public void SetDestroyed(bool _isDestroyed, bool onlyBreak)
    {
        isDestroyed = _isDestroyed;
    }

    public void SetWasBroken(bool _wasBroken)
    {
        wasBroken = _wasBroken;
    }

    public string GetTaskName() => taskName;
}
