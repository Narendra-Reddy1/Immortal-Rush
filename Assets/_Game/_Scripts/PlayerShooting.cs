using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Naren_Dev
{
    public class PlayerShooting : MonoBehaviour
    {
        #region Variables

        [SerializeField] private GunStats m_gunStats;

        [Header("Shoot")]
        [Space(4)]
        [SerializeField] private Transform m_muzzleEnd;
        [SerializeField] private float m_fireRate;
        [SerializeField] private GameObject m_muzzleFlashEffect;
        [SerializeField] private PlayerVariables m_playerVariables;
        [Tooltip("Usually the zombie collider height. Give slight less value than collider height for accurate results")]
        [SerializeField] private float m_raycastOffsetHeight;
        private float m_fireRateDelay;
        [SerializeField] private LineRenderer m_lineRenderer;


        [Header("Ammunition")]
        [SerializeField] private GameObject m_bullet;
        [SerializeField] private int m_ammunitionPoolSize;
        [SerializeField] private List<GameObject> m_ammunitionPool;
        [SerializeField] private Transform m_ammunitionParent;

        [Header("Zombies")]
        [Space(4)]
        public Transform m_nearestEnemy;
        [SerializeField] private LayerMask m_enemyLayer;
        [SerializeField] private ZombieEventsSO m_zombieEvents;
        [SerializeField] private bool m_canSearchForEnemies = false;
        [SerializeField] private float m_enemyDetectionRange;
        [SerializeField] private List<Transform> m_activatedZombiesList;

        private Transform m_transform;
        private RaycastHit m_enemyHit;
        #endregion



        #region Unity Built-In Methods

        private void Awake()
        {
            _Init();
            //  InitializeAmmunitionPool();
        }
        private void OnEnable()
        {
            m_zombieEvents.OnZombieActivated.AddListener(SetActivatedEnemiesList);
            m_zombieEvents.OnZombieDead.AddListener(PopDeadZombieFromActivateList);
        }
        private void OnDisable()
        {
            m_zombieEvents.OnZombieActivated.RemoveListener(SetActivatedEnemiesList);
            m_zombieEvents.OnZombieDead.RemoveListener(PopDeadZombieFromActivateList);
        }

        private void Update()
        {
            _Shoot();
        }

        private void Reset()
        {
            //   m_ammunitionPoolSize = 30;
            m_raycastOffsetHeight = 1f;
        }


        private void OnDrawGizmos()
        {
            //if (m_nearestEnemy == null) return;
            //   Gizmos.DrawWireSphere(m_muzzleEnd.position, m_enemyDetectionRange);
            // Gizmos.color = Color.red;
            //Gizmos.DrawRay(m_muzzleEnd.position, ((m_nearestEnemy.position - m_muzzleEnd.position)));
        }

        #endregion

        #region Custom Methods

        private void _Init()
        {
            m_transform = transform;
            m_fireRate = m_gunStats.fireRate;
            m_fireRateDelay = m_fireRate;
        }

        //Ammunition 
        private void InitializeAmmunitionPool()
        {
            ObjectPooler.InitializePool(m_bullet, m_ammunitionPoolSize, m_ammunitionPool, m_ammunitionParent);
        }

        private GameObject GetBullet()
        {
            return ObjectPooler.GetObjectFromPool(m_ammunitionPool);
        }

        private ZombieBehaviour zombieBehaviour;
        private void _Shoot()
        {
            if (Time.time < m_fireRateDelay)
            {
                m_lineRenderer.enabled = false;
                return;
            }
            else if (m_activatedZombiesList.Count <= 0)
            {
                m_nearestEnemy = null;
                m_playerVariables.isPlayerShooting = false;
                return;
            }
            FetchForNearbyEnemy();
            if (!m_nearestEnemy.Equals(prevZombie))
            {
                zombieBehaviour = m_nearestEnemy.GetComponent<ZombieBehaviour>();
            }
            m_playerVariables.isPlayerShooting = true;
            m_transform.LookAt(m_nearestEnemy);
            m_muzzleEnd.LookAt(m_nearestEnemy);
            if (Physics.Raycast(m_muzzleEnd.position, (m_nearestEnemy.position - m_muzzleEnd.position) + Vector3.up, out m_enemyHit, m_nearestEnemy.position.sqrMagnitude / 2, m_enemyLayer.value))
            {

                m_lineRenderer.SetPosition(0, m_muzzleEnd.position);
                m_lineRenderer.enabled = true;
                m_fireRateDelay = m_fireRate + Time.time;
                m_lineRenderer.SetPosition(1, m_enemyHit.point);
                OnEnemyHitWithBullet(zombieBehaviour);
                prevZombie = m_nearestEnemy;
            }
        }
        private bool isSameEnemy = false;
        private Transform prevZombie;
        private void OnEnemyHitWithBullet(ZombieBehaviour zombie)
        {
            try
            {

                zombie.OnHitWithBullet(m_gunStats.bulletDamage);
            }
            catch (System.Exception e)
            {
                SovereignUtils.LogError($"Error From PlayerShooting: {System.Reflection.MethodBase.GetCurrentMethod().Name} {e.Message}");
            }
        }

        private void PopDeadZombieFromActivateList(object args)
        {
            if (m_activatedZombiesList.Contains((Transform)args))
            {
                m_activatedZombiesList.Remove((Transform)args);
            }
        }
        private void SetActivatedEnemiesList(object args)
        {
            List<Transform> zombieList = (List<Transform>)args;
            m_activatedZombiesList.AddRange(zombieList);
            SovereignUtils.Log($"Activated Zombies: {m_activatedZombiesList.Count}");
        }

        private void FetchForNearbyEnemy()
        {
            //   float dist = 0;
            int count = m_activatedZombiesList.Count;

            if (count == 1)
            {
                m_nearestEnemy = m_activatedZombiesList[0];
                return;
            }

            for (int i = 0; i < count - 1; i++)
            {
                if (Vector3.Distance(transform.position, m_activatedZombiesList[i].position) <
                    Vector3.Distance(transform.position, m_activatedZombiesList[i + 1].position))
                {
                    m_nearestEnemy = m_activatedZombiesList[i];
                }
            }
        }

        #endregion
    }
}