using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_Idle : State {

    public float attackRange = 1;
    public float senseRange = 10;

    // Use this for initialization
    void Start() {

    }

    public override void OnStateEnter(FSM fsm) {
        Debug.Log("Do something Idle (Enter)");
    }

    public override void OnStateExit(FSM fsm) {
        Debug.Log("Do something Idle (exit)");
    }

    public override void OnStateUpdate(FSM fsm) {

        float distToPlayer = Vector3.Distance(transform.position, fsm.player.transform.position);
        Vector3 playerForward = fsm.player.transform.forward;
        Vector3 playerDir = (transform.position - fsm.player.transform.position).normalized;
        float dot = Vector3.Dot(playerForward, playerDir);

        if (dot > fsm.dotProductNeededToHide) {
            fsm.SwitchToState(StateType.Hide);
        } else if (distToPlayer < attackRange) {
            fsm.SwitchToState(StateType.Attack);
        } else if (distToPlayer < senseRange) {
            fsm.targetPos = fsm.player.transform.position;
            fsm.agent.SetDestination(fsm.targetPos);
        } else if (fsm.agent.remainingDistance < 0.1f) {
            fsm.currentObstacle = fsm.GetNearestObstacleTowardsPlayer(false, senseRange);
            fsm.targetPos = fsm.currentObstacle.transform.position + (fsm.currentObstacle.transform.position - fsm.player.transform.position).normalized * 1.5f; ;
            fsm.agent.SetDestination(fsm.targetPos);
        }
    }

}
