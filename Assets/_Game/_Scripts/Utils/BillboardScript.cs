using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardScript : MonoBehaviour
{
    [Tooltip("Target Camera the object to look at." +
        "If it is null it fetches MainCamera.")]
    [SerializeField]
    private Transform m_targetCamera;

    private Transform m_transform;

    private void Awake()
    {
        if (m_targetCamera == null) m_targetCamera = Camera.main.transform;
        m_transform = transform;
    }


    private void Update()
    {
        m_transform.LookAt(m_transform.position + m_targetCamera.rotation * Vector3.forward, m_targetCamera.rotation * Vector3.up);
        
    }

}
