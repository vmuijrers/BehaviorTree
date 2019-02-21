using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_Hide : State {

    public float hideOffSet = 1.5f;
    // Use this for initialization
    void Start() { }

    public override void OnStateEnter(FSM fsm) {
        fsm.currentObstacle = fsm.GetNearestObstacle(true, 50);
        fsm.targetPos = fsm.currentObstacle.transform.position + (fsm.currentObstacle.transform.position - fsm.player.transform.position).normalized * hideOffSet;
        fsm.agent.SetDestination(fsm.targetPos);
    }

    public override void OnStateExit(FSM fsm) {
        Debug.Log("Hide (exit)");
    }

    public override void OnStateUpdate(FSM fsm) {
        Vector3 playerForward = fsm.player.transform.forward;
        Vector3 playerDir = (transform.position - fsm.player.transform.position).normalized;
        float dot = Vector3.Dot(playerForward, playerDir);

        //not in sight
        if (dot < fsm.dotProductNeededToHide || (fsm.currentObstacle != null && fsm.agent.remainingDistance < 0.1f)) {
            fsm.SwitchToState(StateType.Idle);
        }
    }

}
