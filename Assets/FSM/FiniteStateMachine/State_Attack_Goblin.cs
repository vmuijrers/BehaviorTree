﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_Attack_Goblin : State
{

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
        Debug.Log("Do something Attack (Enter)");
    }

    public override void OnStateExit(FSM fsm)
    {
        Debug.Log("Do something Attack (exit)");
    }

    public override void OnStateUpdate(FSM fsm)
    {
        // Debug.Log("Do something Attack (Update)");
    }
}
