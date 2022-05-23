using System;
using UnityEngine;
using UnityEngine.Events;

namespace Naren_Dev
{
    [CreateAssetMenu(fileName = "New PawnEventsSO", menuName = "ScriptableObjects/Events/Pawn Events")]
    public class PawnEventsSO : BaseScriptableObject
    {
        [Space]
        public Action<object> OnPawnStartFollowingPlayer;
        public UnityEvent<object> OnPawnStoppedFollowingPlayer;
        public Action<object, UnityEngine.Object> OnPawnCollidedWithObstacle;



    }
}