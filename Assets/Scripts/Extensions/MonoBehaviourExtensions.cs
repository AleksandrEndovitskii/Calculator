using System;
using System.Collections;
using System.Linq;
using UnityEngine;

namespace Extensions
{
    public static class MonoBehaviourExtensions
    {
        private static float _allConditionsAreMetCheckingAttemptIntervalSeconds = 0.1f;

        public static void InvokeActionAfterAllConditionsAreMet(this MonoBehaviour monoBehaviour, 
            Action action, params Func<bool>[] conditions)
        {
            var invokeActionAfterAllConditionsAreMetCoroutine =
                monoBehaviour.StartCoroutine(InvokeActionAfterAllConditionsAreMetCoroutine(action, conditions));
        }

        private static IEnumerator InvokeActionAfterAllConditionsAreMetCoroutine(Action action, params Func<bool>[] conditions)
        {
            while (conditions.Any(c => c() != true))
            {
                yield return new WaitForSeconds(_allConditionsAreMetCheckingAttemptIntervalSeconds);
            }
            //yield return new WaitUntil(() => conditions.All(c => c() == true));

            action?.Invoke();
        }
    }
}
