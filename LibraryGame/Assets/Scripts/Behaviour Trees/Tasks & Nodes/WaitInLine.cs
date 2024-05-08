using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitInLine : Task
{
    public string EnterPathKey;
    public string IsInLineKey;
    public string IndexKey;
    public override NodeResult Execute()
    {
        bool IsInLine = (bool)tree.GetValue(IsInLineKey);

        if (IsInLine == false)
        {
            return NodeResult.SUCCESS;
        }

        int NextIndex = (int)tree.GetValue(IndexKey) + 1;

        Path EnterPath = (Path)tree.GetValue(EnterPathKey);

        if (EnterPath.IsPointAtIndexOccupied(NextIndex))
        {
            return NodeResult.RUNNING;
        }
        else
        {
            EnterPath.TogglePointOccupation(NextIndex - 1);
            EnterPath.TogglePointOccupation(NextIndex);

            return NodeResult.SUCCESS;
        }

    }

    public override void Reset()
    {
       
    }
}
