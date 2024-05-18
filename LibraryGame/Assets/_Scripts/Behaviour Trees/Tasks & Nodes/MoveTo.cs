using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTo : Task
{
    public string TargetKey;
    
    public string SpeedKey;
    public string TurnSpeedKey;
    public string AccuracyKey;

    public override NodeResult Execute()
    { 
        GameObject go = tree.gameObject;
        GameObject target = (GameObject)tree.GetValue(TargetKey);

        float Speed = (float)tree.GetValue(SpeedKey); // should, like targetname, pass the variable names in.
        float TurnSpeed = (float)tree.GetValue(TurnSpeedKey);
        float Accuracy = (float)tree.GetValue(AccuracyKey);

        if (Vector3.Distance(go.transform.position,target.transform.position) < Accuracy)
        {
            return NodeResult.SUCCESS;
        }

        Vector3 direction = target.transform.position - go.transform.position;
        go.transform.rotation = Quaternion.Slerp(go.transform.rotation, Quaternion.LookRotation(direction), TurnSpeed * Time.deltaTime);

        if (Vector3.Distance(go.transform.position, target.transform.position) < Speed * Time.deltaTime)
        {
            go.transform.position = target.transform.position;
        }

        else
        {
            go.transform.Translate(0, 0, Speed * Time.deltaTime);
        }
        return NodeResult.RUNNING;
    }

}
