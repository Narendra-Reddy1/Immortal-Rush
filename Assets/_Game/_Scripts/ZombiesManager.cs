using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Naren_Dev
{
    public class ZombiesManager : MonoBehaviour
    {
        #region Variables

        [Tooltip("This list consists hords zombies which appear on the platform at end of the levels")]
        [SerializeField] private List<GameObject> m_zombiesHordeList;

        [Tooltip("This list consists isolate zombies which appear at middle of the levels")]
        [SerializeField] private List<ZombieBehaviour> m_isolateZombiesList;

        [Space(2)]
        [SerializeField] private ZombieEventsSO m_zombieEventsSO;

        private int index = 0;

        #endregion


        #region Unity Built-In Methods

        private void OnEnable()
        {
            m_zombieEventsSO.TriggerZombie += _TriggerZombiesHord;
            m_zombieEventsSO.TriggerIsolateZombie += _TriggerIsolateZombie;
        }
        private void OnDisable()
        {
            m_zombieEventsSO.TriggerZombie -= _TriggerZombiesHord;
            m_zombieEventsSO.TriggerIsolateZombie -= _TriggerIsolateZombie;

        }

        #endregion

        #region Custom Methods

        private void _TriggerZombiesHord()
        {
            foreach (GameObject zombie in m_zombiesHordeList)
            {
                zombie.SetActive(true);

            }
        }


        private void _TriggerIsolateZombie()
        {
            if (index >= m_isolateZombiesList.Count) return;
            m_isolateZombiesList[index].TriggerIsolateZombies();
            index++;
        }

        #endregion
    }
}