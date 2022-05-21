using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOverTime : MonoBehaviour
{
    [SerializeField] private float m_timer;

    private void Awake()
    {
        Invoke(nameof(_Destroy), m_timer);
    }

    private void _Destroy()
    {
        Destroy(gameObject, m_timer);
    }
}
