using System;
using UnityEngine;
using UnityEngine.Events;

namespace Naren_Dev
{
    [CreateAssetMenu(fileName = "New PawnEventsSO", menuName = "ScriptableObjects/PawnEvents")]
    public class PawnEventsSo : BaseScriptableObject
    {
        [Space]
        public Action<object> OnPawnStartFollowingPlayer;
        public UnityEvent<object> OnPawnStoppedFollowingPlayer;
        public Action<object, UnityEngine.Object> OnPawnCollidedWithObstacle;



    }
}