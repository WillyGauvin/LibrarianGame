using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CheckBookAI : BehaviorTree
{
    [Header("Properties")]
    public float Speed;
    public float TurnSpeed;
    public float Accuracy;

    [Space(20)]
    [Header("Through Code Only")]
    public Path EnterPath;
    public Path ExitPath;

    public GameObject[] EnterPoints;
    public GameObject[] ExitPoints;

    public GameObject Target;

    public int index = 0;
    public bool IsInLine = true;



    string EnterPathKey = "EnterPath";
    string ExitPathKey = "ExitPath";
    string TargetKey = "Target";
    string SpeedKey = "Speed";
    string TurnSpeedKey = "TurnSpeed";
    string AccuracyKey = "Accuracy";
    string IndexKey = "Index";
    string IsInLineKey = "IsInLine";


    //construct the actual tree
    void Start()
    {
        EnterPoints = EnterPath.Points;
        ExitPoints = ExitPath.Points;

        // create nodes
        Sequence CheckBook = new Sequence();

        Selector Waiting = new Selector();
        Sequence Movement = new Sequence();

        FrontOfLine FrontLine = new FrontOfLine();
        WaitInLine WaitLine = new WaitInLine();

        SelectNextGameObject PickNextWP = new SelectNextGameObject();
        MoveTo MoveToWP = new MoveTo();
        
        // create blackboard keys and initialize them with values
        // NOTE - SHOULD BE USING CONSTANTS
        SetValue(EnterPathKey, EnterPath);
        SetValue(ExitPathKey, ExitPath);
        SetValue(TargetKey, Target);

        SetValue(SpeedKey, Speed);
        SetValue(TurnSpeedKey, TurnSpeed);
        SetValue(AccuracyKey, Accuracy);

        SetValue(IndexKey, index);
        SetValue(IsInLineKey, IsInLine);

        // set node parameters - connect them to the blackboard
        FrontLine.EnterPathKey = EnterPathKey;
        FrontLine.IsInLineKey = IsInLineKey;
        FrontLine.IndexKey = IndexKey;

        WaitLine.EnterPathKey = EnterPathKey;
        WaitLine.IsInLineKey = IsInLineKey;
        WaitLine.IndexKey = IndexKey;

        PickNextWP.EnterPathKey = EnterPathKey;
        PickNextWP.ExitPathKey = ExitPathKey;
        PickNextWP.IsInLineKey = IsInLineKey;
        PickNextWP.IndexKey = IndexKey;
        PickNextWP.TargetKey = TargetKey;

        MoveToWP.TargetKey = TargetKey;
        MoveToWP.SpeedKey = SpeedKey;
        MoveToWP.TurnSpeedKey = TurnSpeedKey;
        MoveToWP.AccuracyKey = AccuracyKey;


        // connect tasks

        Waiting.children.Add(FrontLine);
        Waiting.children.Add(WaitLine);

        Movement.children.Add(PickNextWP);
        Movement.children.Add(MoveToWP);

        CheckBook.children.Add(Waiting);
        CheckBook.children.Add(Movement);

        Waiting.tree = this;
        Movement.tree = this;

        FrontLine.tree = this;
        WaitLine.tree = this;

        PickNextWP.tree = this;
        MoveToWP.tree = this;

        root = CheckBook;

    }

    // we don't need an update - the base class will deal with that.
    // but, since we have one, we can use it to set some of the moveto parameters on the fly
    // which lets us tweak them in the inspector
    public override void Update()
    {
        //SetValue("Speed", Speed);
        //SetValue("TurnSpeed", TurnSpeed);
        //SetValue("Accuracy", Accuracy);
        base.Update();
    }
}
