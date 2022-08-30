using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MonoBeh
{
    public class Entity : MonoBehaviour
    {
        [SerializeField] private int idEntity;
        public int IdEntity { get => idEntity; set => idEntity = value; }
    }
}