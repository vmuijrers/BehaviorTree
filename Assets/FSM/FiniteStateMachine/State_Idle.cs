using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_Idle : State
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
        Debug.Log("Do something Idle (Enter)");
    }

    public override void OnStateExit(FSM fsm)
    {
        Debug.Log("Do something Idle (exit)");
    }

    public override void OnStateUpdate(FSM fsm)
    {
        //Debug.Log("Do something Idle (Update)");

        float distToPlayer = Vector3.Distance(transform.position, fsm.player.transform.position);
        Vector3 playerForward = fsm.player.transform.forward;
        Vector3 playerDir = (transform.position - fsm.player.transform.position).normalized;
        float dot = Vector3.Dot(playerForward, playerDir);

        if (dot > fsm.sightRange)
        {
            fsm.SwitchToState("Hide");
        }
        else if (distToPlayer < 1f)
        {
            fsm.SwitchToState("Attack");
        }
        else
        {
            if (distToPlayer < 10f)
            {
                fsm.targetPos = fsm.player.transform.position;
                fsm.agent.SetDestination(fsm.targetPos);
            }
            else


            //Advance sneakily to the player   
            if (fsm.agent.remainingDistance < 0.1f)
            {
                fsm.currentObstacle = fsm.GetNearestObstacleTowardsPlayer(false, 10);
                fsm.targetPos = fsm.currentObstacle.transform.position + (fsm.currentObstacle.transform.position - fsm.player.transform.position).normalized * 1.5f; ;
                fsm.agent.SetDestination(fsm.targetPos);
            }


        }


    }
}
