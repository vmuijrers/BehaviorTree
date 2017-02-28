using Pada1.BBCore;
using UnityEngine;

namespace BBUnity.Conditions
{
    [Condition("Perception/IsInSightOfTargetUnobstructed")]
    [Help("Checks whether a target is in sight depending on a given angle")]
    public class IsInSightOfTargetUnobstructed : GOCondition
    {
        [InParam("target")]
        [Help("Target to check the angle")]
        public GameObject target;

        [InParam("angle")]
        [Help("The view angle to consider that the target is in sight")]
        public float angle;

        public override bool Check()
        {
            Vector3 dir = (gameObject.transform.position - target.transform.position);
            RaycastHit hit;
            if (Physics.Raycast(target.transform.position + new Vector3(0, 0.1f, 0), dir, out hit))
            {
                return hit.collider.gameObject == gameObject && Vector3.Angle(dir, target.transform.forward) < angle * 0.5f;
            }
            return false;
        }
    }
}
