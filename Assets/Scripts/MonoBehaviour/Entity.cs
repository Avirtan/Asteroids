using Leopotam.EcsLite;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MonoBeh
{
    public class Entity : MonoBehaviour
    {
        [SerializeField] protected int _idEntity;
        protected EcsWorld _world;
        public int IdEntity { get => _idEntity; set => _idEntity = value; }
        public EcsWorld World { get => _world; set => _world = value; }

    }
}