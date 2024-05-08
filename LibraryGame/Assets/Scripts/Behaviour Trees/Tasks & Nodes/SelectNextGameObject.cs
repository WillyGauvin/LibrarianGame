using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SelectNextGameObject : Node
{
    public string EnterPathKey;
    public string ExitPathKey;

    public string IsInLineKey;

    public string IndexKey;
    public string TargetKey;

    // Use this for initialization

    public override NodeResult Execute()
    {
        bool IsInLine = (bool)tree.GetValue(IsInLineKey);

        //If we are still in line
        if (IsInLine)
        {
            Path EnterPath = (Path)tree.GetValue(EnterPathKey);
            GameObject[] Points = EnterPath.Points;
            int Index = (int)tree.GetValue(IndexKey);

            //Check if we are at the front of the line.
            if (Index == Points.Length - 1)
            {
                Path ExitPath = (Path)tree.GetValue(ExitPathKey);
                GameObject[] ExitPoints = ExitPath.Points;
                Index = 0;
                tree.SetValue(TargetKey, ExitPoints[Index]);
                tree.SetValue(IndexKey, Index);
                tree.SetValue(IsInLineKey, false);
            }
            else
            {
                Index++;
                tree.SetValue(IndexKey, Index);
                tree.SetValue(TargetKey, Points[Index]);
            }

            return NodeResult.SUCCESS;

        }
        else
        {
            Path ExitPath = (Path)tree.GetValue(ExitPathKey);
            GameObject[] Points = ExitPath.Points;
            int Index = (int)tree.GetValue(IndexKey);

            //Check if we are at the front of the line. 
            if (Index == Points.Length - 1)
            {
                NPC npc = tree.gameObject.GetComponent<NPC>();
                npc.LeftLibrary();
            }
            else
            {
                Index++;
                tree.SetValue(IndexKey, Index);
                tree.SetValue(TargetKey, Points[Index]);
            }

            return NodeResult.SUCCESS;

        }
    }

    public override void Reset()
    {
        Path EnterPath = (Path)tree.GetValue(EnterPathKey);
        GameObject[] Points = EnterPath.Points;
        int Index = (int)tree.GetValue(IndexKey);

        tree.SetValue(TargetKey, Points[Index]);
        base.Reset();
    }
}
