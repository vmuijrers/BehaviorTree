using Pada1.BBCore.Framework;
using Pada1.BBCore;
using UnityEngine;
namespace BBCore.Conditions
{
    [Condition("Basic/CheckBoolAND")]
    [Help("Checks whether two booleans have the same value")]
    public class CheckBoolAND : ConditionBase
    {
        [InParam("valueA")]
        [Help("First value to be compared")]
        public bool valueA;

        [InParam("valueB")]
        [Help("Second value to be compared")]
        public bool valueB;

        [InParam("valueC")]
        [Help("First value to be compared")]
        public bool valueC;

        [InParam("valueD")]
        [Help("Second value to be compared")]
        public bool valueD;
        public override bool Check()
        {
            //Debug.Log("Checking Bool!" + (valueA == valueB));
            return valueA == valueB && valueC == valueD;
        }
    }
}