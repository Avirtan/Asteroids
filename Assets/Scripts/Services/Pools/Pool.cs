using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonoBeh;
using System.Linq;

namespace Service
{
    public class Pool : MonoBehaviour
    {
        [SerializeField] private List<Entity> pooledObjects;
        [SerializeField] private Entity objectToPool;
        [SerializeField] private int amountToPool;

        void Start()
        {
            pooledObjects = new List<Entity>();
            Entity obj;
            for (int i = 0; i < amountToPool; i++)
            {
                obj = Instantiate(objectToPool, transform);
                obj.gameObject.SetActive(false);
                pooledObjects.Add(obj);
            }
        }

        public Entity GetPooledObject()
        {
            for (int i = 0; i < pooledObjects.Count; i++)
            {
                if (!pooledObjects[i].gameObject.activeInHierarchy)
                {
                    return pooledObjects[i];
                }
            }
            var obj = Instantiate(objectToPool, transform);
            obj.gameObject.SetActive(false);
            pooledObjects.Add(obj);
            return obj;
        }
    }
}
