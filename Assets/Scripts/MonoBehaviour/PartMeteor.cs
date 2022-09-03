using Util;
using UnityEngine;
using Service;

namespace MonoBeh
{
    public class PartMeteor : Entity
    {
        [SerializeField] Rigidbody2D _rb;
        [SerializeField] float moveSpeed;
        public void MoveMeteor(Vector2 dirRoot)
        {
            var dirX = Random.Range(-1.0f, 1.0f) > 0 ? 1 : -1;
            var dirY = Random.Range(-1.0f, 1.0f) > 0 ? 1 : -1;
            var dir = new Vector2(Random.Range(1.0f, 2.0f) * dirX, Random.Range(1.0f, 2.0f) * dirY);
            _rb.AddForce(dir * moveSpeed);
        }
    }
}
