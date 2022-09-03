using Component;
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

        private void OnCollisionEnter2D(Collision2D collision)
        {
            var entityEnemy = collision.gameObject.GetComponent<Entity>();
            var entityDestroy = _world.NewEntity();
            var destroyEnemyEventPool = _world.GetPool<DestroyEnemyEvent>();
            destroyEnemyEventPool.Add(entityDestroy);
            ref DestroyEnemyEvent destroyEvent = ref destroyEnemyEventPool.Get(entityDestroy);
            destroyEvent.Entity = entityEnemy;
            gameObject.SetActive(false);
        }
    }
}