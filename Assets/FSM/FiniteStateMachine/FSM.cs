using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

public enum StateType {
    Idle = 0,
    Hide = 1,
    Attack = 2,
    Move   = 3
}

public class FSM : MonoBehaviour
{
    internal NavMeshAgent agent;
    internal Vector3 targetPos;
    internal GameObject player;
    public GameObject[] obstacles;
    internal GameObject currentObstacle;
    public State currentState;
    public Dictionary<StateType, State> states;
    public float moveSpeed = 6f;
    public float dotProductNeededToHide = 0.9f;
    // Use this for initialization
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = moveSpeed;
        player = GameObject.FindGameObjectWithTag("Player");
        obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
        states = new Dictionary<StateType, State>();
 
        GetComponents<State>().ToList().ForEach(x => states.Add(x.type, x));
    }

    // Update is called once per frame
    void Update()
    {
        currentState.OnStateUpdate(this);
    }

    public void SwitchToState(StateType stateType)
    {
        State newState;
        if (states.ContainsKey(stateType))
        {
            Debug.Log("Assigned new State");
            newState = states[stateType];
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
