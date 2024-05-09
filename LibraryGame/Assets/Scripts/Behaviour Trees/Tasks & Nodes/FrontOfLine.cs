using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrontOfLine : Task
{
    public string EnterPathKey;
    public string IsInLineKey;
    public string IndexKey;
    public override NodeResult Execute()
    {
        bool IsInLine = (bool)tree.GetValue(IsInLineKey);
        
        if (IsInLine == false)
        {
            return NodeResult.FAILURE;
        }
        else
        {
            Path EnterPath = (Path)tree.GetValue(EnterPathKey);
            GameObject[] Points = EnterPath.Points;
            int Index = (int)tree.GetValue(IndexKey);

            if (Index != Points.Length - 1)
            {
                //If we aren't at the front of the line.
                return NodeResult.FAILURE;
            }
            else
            {
                NPC npc = tree.gameObject.GetComponent<NPC>();
                if (npc.CitizenType == NPCType.CheckOut)
                {
                    if (npc.NPCBook == null)
                    {
                        npc.isFrontOfLine = true;
                        return NodeResult.RUNNING;
                    }
                    else
                    {
                        EnterPath.TogglePointOccupation(Index);
                        npc.isFrontOfLine = false;
                        return NodeResult.SUCCESS;
                    }
                }
                else if (npc.CitizenType == NPCType.CheckIn)
                {
                    if (npc.NPCBook != null)
                    {
                        return NodeResult.RUNNING;
                    }
                    else
                    {
                        EnterPath.TogglePointOccupation(Index);
                        return NodeResult.SUCCESS;
                    }
                }
                else
                {
                    return NodeResult.FAILURE;
                }
            }
        }
    }

    public override void Reset()
    {

    }
}
