using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Naren_Dev
{
    [CreateAssetMenu(fileName = "ZombieEvents", menuName = "ScriptableObjects/Events/Zombie Events")]
    public class ZombieEventsSO : BaseScriptableObject
    {

        [Space(2.5f)]
        public UnityEvent<object> OnZombieActivated;
        public UnityEvent OnZombieAttacked;
        public UnityEvent<object> OnZombieDead;
        public Action TriggerZombie;
        public Action TriggerIsolateZombie;

    }
}