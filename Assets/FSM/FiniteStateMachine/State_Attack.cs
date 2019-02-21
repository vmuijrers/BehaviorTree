﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_Attack : State {

    // Use this for initialization
    void Start() {

    }

    public override void OnStateEnter(FSM fsm) {
        Debug.Log("Attack (Enter)");
    }

    public override void OnStateExit(FSM fsm) {
        Debug.Log("Attack (exit)");
    }

    public override void OnStateUpdate(FSM fsm) {
        Debug.Log("Gotcha!");
        fsm.SwitchToState(StateType.Hide);
    }
}
