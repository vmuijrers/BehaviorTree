using Pada1.BBCore.Tasks;
using Pada1.BBCore;
using UnityEngine;

namespace BBUnity.Actions
{

    [Action("Navigation/MoveToGameObjectDistanceLeft")]
    [Help("Moves the game object towards a given target by using a NavMeshAgent and stops at a given distance")]
    public class MoveToGameObjectDistanceLeft : GOAction
    {
        [InParam("target")]
        [Help("Target game object towards this game object will be moved")]
        public GameObject target;

        [InParam("distance")]
        [Help("Target game object towards this game object will be moved")]
        public float distance;

        private UnityEngine.AI.NavMeshAgent navAgent;

        private Transform targetTransform;
        private Vector3 targetPos;
        public override void OnStart()
        {
            if (target == null)
            {
                Debug.LogError("The movement target of this game object is null", gameObject);
                return;
            }
            targetTransform = target.transform;

            navAgent = gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();
            if (navAgent == null)
            {
                Debug.LogWarning("The " + gameObject.name + " game object does not have a Nav Mesh Agent component to navigate. One with default values has been added", gameObject);
                navAgent = gameObject.AddComponent<UnityEngine.AI.NavMeshAgent>();
            }
            targetPos = targetTransform.position + (gameObject.transform.position - targetTransform.position).normalized * distance;
            navAgent.SetDestination(targetPos);
            navAgent.Resume();
        }

        public override TaskStatus OnUpdate()
        {
            if (target == null)
                return TaskStatus.FAILED;
            if (!navAgent.pathPending && navAgent.remainingDistance <= navAgent.stoppingDistance)
                return TaskStatus.COMPLETED;
            else if (navAgent.destination != targetPos)
                navAgent.SetDestination(targetPos);
            return TaskStatus.RUNNING;
        }

        public override void OnAbort()
        {
            navAgent.Stop();
        }
    }
}
