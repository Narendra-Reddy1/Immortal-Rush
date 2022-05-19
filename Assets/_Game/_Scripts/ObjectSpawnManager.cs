using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// This Script is responsible for spawning the given object at given positions.
/// </summary>
public class ObjectSpawnManager
{

    #region Variables 

    //[SerializeField] private Transform m_parent;
    //[SerializeField] private GameObject m_targetObjectToSpawn;
    //[SerializeField] private List<Transform> m_positionsToSpawn;

    #endregion

    #region Unity Built-In Methods

    //private void Start()
    //{
    //      _SpawnObjectsAtPositions();
    //}

    #endregion


    #region Custom Methods

    public static void SpawnObjectsAtPositions(GameObject targetObjectToSpawn, List<Transform> positionsToSpawn, Transform parent=null)
    {
        int count = positionsToSpawn.Count;
        for (int i = 0; i < count; i++)
        {
            Object.Instantiate(targetObjectToSpawn, positionsToSpawn[i].position, Quaternion.identity, parent);
        }
    }
    public static void SpawnObjectsAtPositions(GameObject targetObjectToSpawn, List<Transform> positionsToSpawn, List<GameObject> spawnedObjectsList, Transform parent=null)
    {
        int count = positionsToSpawn.Count;
        for (int i = 0; i < count; i++)
        {
            spawnedObjectsList.Add(Object.Instantiate(targetObjectToSpawn, positionsToSpawn[i].position, Quaternion.identity, parent));
        }   
    }

    #endregion


}
