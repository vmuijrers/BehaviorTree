using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_Move : State
{
    public float moveSpeed = 3f;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void OnStateEnter(FSM fsm)
    {
        Debug.Log("Move (Enter)");
        fsm.agent.SetDestination(fsm.targetPos);
        fsm.SwitchToState("Idle");

    }

    public override void OnStateExit(FSM fsm)
    {
        Debug.Log("Move (exit)");
    }

    public override void OnStateUpdate(FSM fsm)
    {
        //Debug.Log("Do something Attack (Update)");

    }
}
