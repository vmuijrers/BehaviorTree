using Pada1.BBCore.Tasks;
using Pada1.BBCore;
using UnityEngine;

namespace BBUnity.Actions
{

    [Action("GameObject/ClosestGameObjectWithTagWithinRange")]
    [Help("Finds the closest game object with a given tag")]
    public class ClosestGameObjectWithTagWithinRange : GOAction
    {
        [InParam("tag")]
        [Help("Tag of the target game object")]
        public string tag;

        [InParam("sqrRange")]
        [Help("Range as Squared Distance")]
        public float sqrRange;

        [OutParam("foundGameObject")]
        [Help("The closest game object with the given tag")]
        public GameObject foundGameObject;

        private float elapsedTime;

        public override void OnStart()
        {
            float dist = float.MaxValue;
            foreach(GameObject go in GameObject.FindGameObjectsWithTag(tag))
            {
                float newdist = (go.transform.position - gameObject.transform.position).sqrMagnitude;
                if(newdist < dist && newdist <= sqrRange)
                {
                    dist = newdist;
                    foundGameObject = go;
                }
            }
        }

        public override TaskStatus OnUpdate()
        {
            return TaskStatus.COMPLETED;
        }
    }
}
