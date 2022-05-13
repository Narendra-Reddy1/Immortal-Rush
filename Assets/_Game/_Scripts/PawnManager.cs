using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnManager : MonoBehaviour
{
    #region Variables


    [SerializeField] private Transform m_parent;
    [SerializeField] private GameObject m_targetObjectToSpawn;
    [SerializeField] private List<Transform> m_positionsToSpawn;
    [SerializeField] private List<GameObject> m_pawnList;

    #endregion

    #region Unity Built-In Methods
    private void Start()
    {
        ObjectSpawnManager.SpawnObjectsAtPositions(targetObjectToSpawn: m_targetObjectToSpawn, positionsToSpawn: m_positionsToSpawn, parent: m_parent);
    }
    #endregion


}
