using System.Collections;
using System.Collections.Generic;
using UnityEngine;







public class FSM_KernMod_CallBack : MonoBehaviour {

//    private delegate void SomeDelegate();
//    private SomeDelegate someDelegateCallBack;

    private System.Action OnEnterState;
    private System.Action OnUpdateState;
    private System.Action OnExitState;

    private System.Action OnDead;
    private Vector3 target;
    private float alertedDistance;

    public void Initialize(System.Action OnDeadCallBack) {
        OnDead = OnDeadCallBack;
    }

    public void OnUpdate() {
        if(this.OnUpdateState != null) {
            this.OnUpdateState();
        }
    }

    public void SwitchState(System.Action OnEnterState, System.Action OnUpdateState, System.Action OnExitState) {

        if (this.OnExitState != null) {
            this.OnExitState();
        }
        this.OnEnterState = OnEnterState;
        this.OnUpdateState = OnUpdateState;
        this.OnExitState = OnExitState;

        if(this.OnEnterState != null) {
            this.OnEnterState();
        }
    }

    public void OnEnterIdle() {

    }

    public void OnUpdateIdle() {

        if(Vector3.Distance(target, transform.position) < alertedDistance) {
            SwitchState(OnEnterAlerted, OnUpdateAlerted, OnExitIdle);
        }
    }

    public void OnExitIdle() {

    }

    public void OnEnterAlerted() {

    }

    public void OnUpdateAlerted() {

    }

    public void OnExitAlerted() {

    }

}


public class FSM_KernMod_Naive : MonoBehaviour {

    public enum StateType {
        Idle = 0,
        Alerted = 1,
        Attacking = 2

    }
    public GameObject target;
    public StateType state = StateType.Idle;

    private float alertedDistance = 10;
    private float attackDistance;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        CheckForTransitions();
        ExecuteStates();
    }
    private void ExecuteStates() {
        switch (state) {
            case StateType.Idle:
                Debug.Log("Idle State");
                break;
            case StateType.Alerted:
                Debug.Log("Alerted State");
                break;
            case StateType.Attacking:
                Debug.Log("Attacking State");
                break;
        }
    }

    private void CheckForTransitions() {
        if (target == null) { return; }

        float distanceToTarget = Vector3.Distance(transform.position, target.transform.position);
        switch (state) {
            case StateType.Idle:
                if (distanceToTarget < alertedDistance) {
                    state = StateType.Alerted;
                }
                break;
            case StateType.Attacking:
                if (distanceToTarget > attackDistance) {
                    state = StateType.Alerted;
                }
                break;
            case StateType.Alerted:
                if (distanceToTarget < attackDistance) {
                    state = StateType.Attacking;
                }
                if (distanceToTarget > alertedDistance) {
                    state = StateType.Idle;
                }
                break;
        }
    }
}
