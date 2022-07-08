using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler
{

    public static void InitializePool(GameObject objectToPool, int sizeOfThePool, List<GameObject> poolList, Transform parent = null)
    {
        for (int i = 0; i < sizeOfThePool; i++)
        {
            GameObject obj = Object.Instantiate(objectToPool, parent);
            obj.SetActive(false);
            poolList.Add(obj);
        }
    }


    public static GameObject GetObjectFromPool(List<GameObject> poolList)
    {
        GameObject go = null;
        int count = poolList.Count;
        for (int i = 0; i < count; i++)
        {
            if (!poolList[i].activeInHierarchy)
            {
                go = poolList[i];
                break;
            }
        }
        return go;
    }


}
