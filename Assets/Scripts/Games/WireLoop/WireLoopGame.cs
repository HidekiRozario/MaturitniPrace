using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireLoopGame : Game
{
    [SerializeField] private WireLoopEnds[] ends;
    [SerializeField] private RelativeHandle[] tools;

    public override void Update()
    {
        if (wasBroken != isBroken) 
        {
            SetWasBroken(isBroken);
            foreach(WireLoopEnds end in ends)
            {
                end.done = false;
            }
            foreach (RelativeHandle tool in tools)
            {
                tool.ResetTool();
            }
        }

        if(ends[0].done && ends[1].done && ends[2].done)
        {
            SetBroken(false);
            SetWasBroken(false);
        }

        base.Update();
    }
}
