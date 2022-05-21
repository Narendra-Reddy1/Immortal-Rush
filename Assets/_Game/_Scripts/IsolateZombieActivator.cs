using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Naren_Dev
{

    public class IsolateZombieActivator : MonoBehaviour
    {
        #region Variables

        [SerializeField] private ZombieEventsSO zombieEventsSO;

        [SerializeField] private List<Transform> m_zombiesList = new List<Transform>();

        #endregion

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player") || other.CompareTag("Pawn"))
            {
                //Debug.Log("<Color="Green">IsolateZombie Activated </Color>");
                SovereignUtils.Log($"IsolateZombies are Triggered");
                zombieEventsSO.TriggerIsolateZombie?.Invoke();
                zombieEventsSO.OnZombieActivated?.Invoke(m_zombiesList);
            }
        }

    }
}