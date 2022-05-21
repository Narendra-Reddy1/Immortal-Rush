using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Naren_Dev
{
    public class ZombieBehaviour : MonoBehaviour
    {
        #region Variables
        [SerializeField] private bool m_canChaseTarget = false;
        [SerializeField] private float m_stoppingDistance;

        [SerializeField] private Transform m_targetToAttack;
        [SerializeField] private NavMeshAgent m_agent;
        [SerializeField] private Animator m_zombieAnimator;
        [SerializeField] private WaitForSeconds m_waitForSeconds = new WaitForSeconds(2f);
        [Space(2)]
        public ZombieEventsSO m_zombieEventsSO;
        private Transform m_transform;

        #endregion


        #region Unity Built-In Methods


        private void Awake()
        {
            _Init();
        }

        //private void OnEnable()
        //{
        //    m_zombieEventsSO.TriggerIsolateZombie += _TriggerIsolateZombies;
        //}
        //private void OnDisable()
        //{
        //    m_zombieEventsSO.TriggerIsolateZombie -= _TriggerIsolateZombies;

        //}


        private void Update()
        {
            ChaseTarget();
        }

        private void OnDestroy()
        {
            StopAllCoroutines();
        }
        #endregion


        #region Custom Methods

        private void _Init()
        {

            TryGetComponent(out m_transform);
            if (m_zombieAnimator == null) TryGetComponent(out m_zombieAnimator);
            if (m_agent == null) TryGetComponent(out m_agent);

            m_agent.stoppingDistance = m_stoppingDistance;
        }

        public void TriggerIsolateZombies()
        {

            StartCoroutine(WaitUntilWakeUp());

        }


        private IEnumerator WaitUntilWakeUp()
        {
            m_zombieAnimator.enabled = true;
            ///Wake up Animation lasts for 2.25 seconds.
            yield return m_waitForSeconds;
            m_canChaseTarget = true;
        }

        private void ChaseTarget()
        {
            if (m_targetToAttack == null || !m_canChaseTarget) return;
            m_agent.SetDestination(m_targetToAttack.position);
            CheckForWalkOrStop();
        }


        /// <summary>
        /// If Distance is less than stopping distance then zombie will stop and attack animation is played
        /// else walk animation is played.
        /// </summary>
        private void CheckForWalkOrStop()
        {
            if (Vector3.Distance(m_transform.position, m_targetToAttack.position) <= m_stoppingDistance)
            {
                _ApplyAnimations("canAttack", true);
            }
            else
                _ApplyAnimations("canAttack", false);
            //Debug.Log($"Boolean: {Vector3.Distance(m_transform.position, m_targetToAttack.position) <= m_stoppingDistance}, Distance: {Vector3.Distance(m_transform.position, m_targetToAttack.position)}, Stopping Distance: {m_stoppingDistance}");

        }


        private void _ApplyAnimations(string id, bool value)
        {
            m_zombieAnimator.SetBool(id, value);
        }


        #endregion



    }
}