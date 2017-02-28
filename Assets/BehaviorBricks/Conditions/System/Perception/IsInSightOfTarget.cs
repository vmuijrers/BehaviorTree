using Pada1.BBCore;
using UnityEngine;

namespace BBUnity.Conditions
{
    [Condition("Perception/IsInSightOfTarget")]
    [Help("Checks whether a target is in sight depending on a given angle")]
    public class IsInSightOfTarget : GOCondition
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

            return Vector3.Angle(dir, target.transform.forward) < angle * 0.5f;

        }
    }
}
