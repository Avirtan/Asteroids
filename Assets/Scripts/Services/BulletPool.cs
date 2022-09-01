using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonoBeh;
using System.Linq;

namespace Service
{
    public class BulletPool : MonoBehaviour
    {
        [SerializeField] private List<Bullet> pooledObjects;
        [SerializeField] private Bullet objectToPool;
        [SerializeField] private int amountToPool;

        void Start()
        {
            pooledObjects = new List<Bullet>();
            Bullet obj;
            for (int i = 0; i < amountToPool; i++)
            {
                obj = Instantiate(objectToPool, transform);
                obj.gameObject.SetActive(false);
                pooledObjects.Add(obj);
            }
        }

        public Bullet GetPooledObject()
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
