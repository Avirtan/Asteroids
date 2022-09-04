using Component;
using UnityEngine;

namespace MonoBeh
{
    public class Player : Entity
    {
        private void OnCollisionEnter2D(Collision2D collision)
        {
            var entity = _world.NewEntity();
            var gameOverEventPool = _world.GetPool<GameOverEvent>();
            gameOverEventPool.Add(entity);
            _world.DelEntity(_idEntity);
            Destroy(gameObject);
        }
    }
}