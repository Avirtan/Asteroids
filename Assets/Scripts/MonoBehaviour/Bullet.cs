using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MonoBeh
{
    public class Bullet : Entity
    {
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private float speed;

        public void Shot(Vector3 dir)
        {
            _rigidbody.AddForce(dir * speed);
        }
    }
}