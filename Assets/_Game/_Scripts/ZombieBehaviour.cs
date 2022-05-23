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

        private Transform m_transform;
        // private ParameterType m_parameterType;
        #endregion


        #region Unity Built-In Methods


        private void Awake()
        {
            _Init();
        }

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
           // m_zombieAnimator.enabled = true;

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
                _ApplyAnimations(ParameterType.BOOL, "canAttack", true);
            }
            else
                _ApplyAnimations(ParameterType.BOOL, "canAttack", false);
            //Debug.Log($"Boolean: {Vector3.Distance(m_transform.position, m_targetToAttack.position) <= m_stoppingDistance}, Distance: {Vector3.Distance(m_transform.position, m_targetToAttack.position)}, Stopping Distance: {m_stoppingDistance}");
        }

        public void OnHealthIsZero()
        {
            _ApplyAnimations(ParameterType.TRIGGER, "Dead");
        }
        private void _ApplyAnimations(ParameterType parameterType, string id, object value = null)
        {


            switch (parameterType)
            {
                case ParameterType.INT:

                    m_zombieAnimator.SetInteger(id, (int)value);

                    break;
                case ParameterType.FLOAT:

                    m_zombieAnimator.SetFloat(id, (float)value);
                    break;
                case ParameterType.BOOL:
                    m_zombieAnimator.SetBool(id, (bool)value);
                    break;
                case ParameterType.TRIGGER:
                    m_zombieAnimator.SetTrigger(id);

                    break;
            }


        }






        private void OnTriggerEnter(Collider other)
        {
            switch (other.tag)
            {
                case "Bullet":
                    HealthManager healthManager = GetComponent<HealthManager>();
                    healthManager.TakeDamage(.5f);
                    if (healthManager.isDead)
                        OnHealthIsZero();

                    break;

                default:
                    break;
            }
        }

        #endregion



    }

    //Animation Parameter Type
    public enum ParameterType
    {
        INT,
        FLOAT,
        BOOL,
        TRIGGER
    }
}