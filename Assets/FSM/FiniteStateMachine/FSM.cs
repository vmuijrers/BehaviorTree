using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class FSM : MonoBehaviour
{
    internal NavMeshAgent agent;
    internal Vector3 targetPos;
    internal GameObject player;
    public GameObject[] obstacles;
    internal GameObject currentObstacle;
    public State currentState;
    public Dictionary<string, State> states;
    public float moveSpeed = 6f;
    public float sightRange = 0.9f;
    // Use this for initialization
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = moveSpeed;
        player = GameObject.FindGameObjectWithTag("Player");
        obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
        states = new Dictionary<string, State>();
        State[] allStates = GetComponents<State>();
        foreach (State s in allStates)
        {
            states.Add(s.name, s);
            Debug.Log(s.name + " Added");
        }
    }

    // Update is called once per frame
    void Update()
    {
        currentState.OnStateUpdate(this);
    }

    public void SwitchToState(string stateName)
    {
        State newState;
        if (states.ContainsKey(stateName))
        {
            Debug.Log("Assigned new State");
            newState = states[stateName];
        }
        else
        {
            Debug.Log("Key not Found!");
            return;
        }

        if (currentState != null)
        {
            currentState.OnStateExit(this);
        }
        currentState = newState;
        currentState.OnStateEnter(this);

    }


    public GameObject GetNearestObstacle(bool includeSelf, float range)
    {
        GameObject targetObs = null;
        float nearestObjDist = 1000000;
        foreach (GameObject go in obstacles)
        {
            float dist = Vector3.Distance(transform.position, go.transform.position);
            if (!includeSelf && go == currentObstacle)
            {
                continue;
            }
            if (dist < nearestObjDist)
            {
                nearestObjDist = dist;
                targetObs = go;
            }
        }
        return targetObs;
    }

    public GameObject GetNearestObstacleTowardsPlayer(bool includeSelf, float range)
    {
        GameObject targetObs = null;
        float nearestObjDist = 100000;
        float playerDist = 100000;
        foreach (GameObject go in obstacles)
        {
            float dist = Vector3.Distance(transform.position, go.transform.position);
            float distToPlayer = Vector3.Distance(player.transform.position, go.transform.position);
            if (!includeSelf && go == currentObstacle)
            {
                continue;
            }
            if (dist < range && distToPlayer < playerDist)
            {
                nearestObjDist = dist;
                playerDist = distToPlayer;
                targetObs = go;
            }
        }
        return targetObs;
    }
}
