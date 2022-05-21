using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    #region Variables

    [SerializeField] private float m_bulletSpeed;

    #endregion


    #region Unity Built-In Methods
    private void Update()
    {
        transform.Translate(transform.forward * m_bulletSpeed * Time.deltaTime, Space.Self);
    }

    #endregion
}
