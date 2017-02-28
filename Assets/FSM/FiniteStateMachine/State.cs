using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State : MonoBehaviour, IState {
    public string name;
    public abstract void OnStateEnter(FSM fsm);
    public abstract void OnStateExit(FSM fsm);
    public abstract void OnStateUpdate(FSM fsm);

}
