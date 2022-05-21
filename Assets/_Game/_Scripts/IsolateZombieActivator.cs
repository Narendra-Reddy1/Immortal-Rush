using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Naren_Dev
{

    public class IsolateZombieActivator : MonoBehaviour
    {
        #region Variables

        public ZombieEventsSO zombieEventsSO;

        #endregion

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player") || other.CompareTag("Pawn"))
            {
                zombieEventsSO.TriggerIsolateZombie?.Invoke();
            }
        }

    }
}