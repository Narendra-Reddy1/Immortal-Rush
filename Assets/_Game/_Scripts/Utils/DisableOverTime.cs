using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableOverTime : MonoBehaviour
{
    [SerializeField] private float m_timer;

    private void OnEnable()
    {
        Invoke(nameof(_Disable), m_timer);
    }
    private void Reset()
    {
        m_timer = 1f;
    }
    private void _Disable()
    {
        gameObject.SetActive(false);
    }
}
