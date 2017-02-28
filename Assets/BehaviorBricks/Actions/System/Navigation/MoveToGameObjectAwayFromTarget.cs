using Pada1.BBCore.Tasks;
using Pada1.BBCore;
using UnityEngine;

namespace BBUnity.Actions
{
    [Action("Navigation/MoveToGameObjectAwayFromTarget")]
    [Help("Moves the game object towards a given target blocking the line of sight to another target by using a NavMeshAgent")]
    public class MoveToGameObjectAwayFromTarget : GOAction
    {
        [InParam("target")]
        [Help("Target game object towards this game object will be moved")]
        public GameObject target;

        [InParam("avoidTarget")]
        [Help("Target to avoid (hide from)")]
        public GameObject avoidTarget;

        [InParam("offSet")]
        [Help("Offset from target")]
        public float offSet;

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

            targetPos = targetTransform.position + (target.transform.position - avoidTarget.transform.position).normalized * offSet;

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
