using Pada1.BBCore.Framework;
using Pada1.BBCore;
using UnityEngine;
namespace BBCore.Conditions
{
    [Condition("Basic/CheckBool")]
    [Help("Checks whether two booleans have the same value")]
    public class CheckBool : ConditionBase
    {
        [InParam("valueA")]
        [Help("First value to be compared")]
        public bool valueA;

        [InParam("valueB")]
        [Help("Second value to be compared")]
        public bool valueB;

        public override bool Check()
        {
            Debug.Log("Checking Bool!" + (valueA == valueB));
            return valueA == valueB;
        }
    }
}