using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState {
    void OnStateEnter(FSM fsm);
    void OnStateUpdate(FSM fsm);
    void OnStateExit(FSM fsm);

}
