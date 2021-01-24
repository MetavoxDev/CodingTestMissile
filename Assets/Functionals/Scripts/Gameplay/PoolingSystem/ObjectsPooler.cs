using System.Collections.Generic;
using UnityEngine;

//Lets do a pooling system for explosions and missiles
public class ObjectsPooler : MonoBehaviour
{
    private enum PoolingType
    {
        Static,
        Scalable
    }

    [SerializeField] private PoolingType poolingType = PoolingType.Static;

    [SerializeField] private List<GameObject> pooledObjects = null;

    [SerializeField] private GameObject prefabObject = null;
    [SerializeField] private int poolCapacity = 20;

    private void Start()
    {
        pooledObjects = new List<GameObject>(poolCapacity);

        for (int i = 0; i < poolCapacity; i++)
        {
            CreateNewPooledObject();
        }
    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }

        if(poolingType == PoolingType.Scalable)
        {
            return CreateNewPooledObject();
        }
        else
        {
            return null;
        }
    }

    //Utilities
    private GameObject CreateNewPooledObject()
    {
        GameObject obj = (GameObject)Instantiate(prefabObject, this.transform);
        obj.SetActive(false);
        pooledObjects.Add(obj);

        return obj;
    }
}
