using Pada1.BBCore.Tasks;
using Pada1.BBCore;
using UnityEngine;

namespace BBUnity.Actions
{

    [Action("GameObject/ClosestGameObjectWithTagNearTargetWithinRange")]
    [Help("Finds the closest game object with a given tag")]
    public class ClosestGameObjectWithTagNearTargetWithinRange : GOAction
    {
        [InParam("tag")]
        [Help("Tag of the target game object")]
        public string tag;

        [OutParam("foundGameObject")]
        [Help("The closest game object with the given tag")]
        public GameObject foundGameObject;

        [InParam("avoidTarget")]
        [Help("The closest game object with the given tag")]
        public GameObject target;

        [InParam("range")]
        [Help("The closest game object with the given tag")]
        public float range;

        private float elapsedTime;

        public override void OnStart()
        {
            float dist = float.MaxValue;
            foreach(GameObject go in GameObject.FindGameObjectsWithTag(tag))
            {
                float distToTarget = (go.transform.position - target.transform.position).sqrMagnitude;
                float distToMe = (go.transform.position - gameObject.transform.position).sqrMagnitude;
                if (distToTarget < dist && distToMe <= range * range)
                {
                    dist = distToTarget;
                    foundGameObject = go;
                }
            }
        }

        public override TaskStatus OnUpdate()
        {
            if(foundGameObject == null)
            {
                return TaskStatus.FAILED;
            }

            return TaskStatus.COMPLETED;
        }
    }
}
