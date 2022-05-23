using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    #region Variables

    [SerializeField] private float m_bulletSpeed;
    [SerializeField] private Rigidbody m_bulletRb;


    #endregion


    #region Unity Built-In Methods

    private void Awake()
    {
        _Init();
        //  m_bulletRb.AddForce(transform.forward * m_bulletSpeed * Time.deltaTime, ForceMode.Impulse);
    }

    private void Update()
    {
        // transform.Translate(transform.forward * m_bulletSpeed * Time.deltaTime, Space.Self);
    }

    #endregion


    #region Custom Methods

    public void Fire(Vector3 direction)
    {

        m_bulletRb.AddForce(direction * m_bulletSpeed * Time.deltaTime, ForceMode.Impulse);
    }
    private void _Init()
    {
        if (m_bulletRb == null) TryGetComponent(out m_bulletRb);
    }


    #endregion
}
