using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Naren_Dev
{
    public class PlayerShooting : MonoBehaviour
    {
        #region Variables

        [Header("Shoot")]
        [Space(4)]
        [SerializeField] private Transform m_muzzleEnd;
        [SerializeField] private float m_fireRate;
        [SerializeField] private GameObject m_bullet;
        private float m_fireRateDelay;

        [Header("Zombies")]
        [Space(4)]
        [SerializeField] private Transform m_nearestEnemy;
        [SerializeField] private LayerMask m_enemyLayer;
        [SerializeField] private ZombieEventsSO m_zombieEvents;
        [SerializeField] private bool m_canSearchForEnemies = false;
        [SerializeField] private float m_enemyDetectionRange;
        private List<Transform> m_activatedZombiesList;

        private RaycastHit m_enemyHit;

        private Transform m_transform;

        #endregion



        #region Unity Built-In Methods

        private void Awake()
        {
            _Init();
        }
        private void OnEnable()
        {
            m_zombieEvents.OnZombieActivated.AddListener(SetActivatedEnemiesList);
        }
        private void OnDisable()
        {
            m_zombieEvents.OnZombieActivated.RemoveListener(SetActivatedEnemiesList);
        }

        private void Update()
        {
            _Shoot();
        }


        private void OnDrawGizmos()
        {
            //if (m_nearestEnemy == null) return;
            Gizmos.DrawWireSphere(m_muzzleEnd.position, m_enemyDetectionRange);
            Gizmos.color = Color.red;
            //Gizmos.DrawRay(m_muzzleEnd.position, ((m_nearestEnemy.position - m_muzzleEnd.position)));
        }

        #endregion

        #region Custom Methods

        private void _Init()
        {
            m_transform = transform;
            m_fireRateDelay = m_fireRate;
            m_activatedZombiesList = new List<Transform>();
        }




        private void _Shoot()
        {
            ///Sphere cast for zombies when zombie activator is triggered. ===>Handled by GetNearbyEnemy method.
            ///Else stop searching for enemies.
            ///If zombies are found in searching, shoot at nearest zombies 
            ///
            //_GetNearbyEnemy(ref m_nearestEnemy);
            //if (m_nearestEnemy == null) return;
            if (m_activatedZombiesList.Count <= 0) return;


            // m_transform.LookAt(m_activatedZombiesList[0]);
            transform.rotation = Quaternion.Slerp(m_transform.rotation, m_activatedZombiesList[0].rotation, 1 * Time.deltaTime);

            if (Physics.Raycast(m_muzzleEnd.position, m_activatedZombiesList[0].position - m_muzzleEnd.position, m_activatedZombiesList[0].position.sqrMagnitude / 2
                , m_enemyLayer.value))
            {
                SovereignUtils.Log($"FireRate: {m_fireRate}, FireRateDelay {m_fireRateDelay}, Time.time: {Time.time}");
                if (Time.time < m_fireRateDelay) return;
                m_fireRateDelay = m_fireRate + Time.time;
                _FireBullet();
                Debug.DrawRay(m_muzzleEnd.position, m_activatedZombiesList[0].position - m_muzzleEnd.position, Color.red, 1f);
                SovereignUtils.Log("Shooting");
            }
        }

        //private void _GetNearbyEnemy(ref Transform enemy)
        //{
        //    //Physics.SphereCast()
        //    bool casted = Physics.SphereCast(m_transform.position, m_enemyDetectionRange, Vector3.forward, out m_enemyHit, m_enemyDetectionRange, m_enemyLayer);
        //    SovereignUtils.Log("Casted: " + casted);

        //    //SovereignUtils.Log("IsEnemyAvailable: " + isEnemyAvailbale);
        //    if (m_enemyHit.collider == null)
        //    {
        //        //m_canSearchForEnemies = false;
        //        // m_nearestEnemy = null;
        //        return;
        //    }
        //    m_nearestEnemy = m_enemyHit.collider.transform;
        //    SovereignUtils.Log("Nearby Enemy Name: " + m_nearestEnemy.name);
        //}

        private void _FireBullet()
        {
            Instantiate(m_bullet, m_muzzleEnd.position, Quaternion.identity, transform);
        }
        private void SetActivatedEnemiesList(object args)
        {
            List<Transform> zombieList = (List<Transform>)args;
            m_activatedZombiesList.AddRange(zombieList);
            SovereignUtils.Log($"Activated Zombies: {m_activatedZombiesList.Count}");
        }
        #endregion
    }
}