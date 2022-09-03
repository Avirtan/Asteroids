using Leopotam.EcsLite;
using Component;
using Service;
using Leopotam.EcsLite.Di;

namespace System
{
    sealed class AttackSystem : IEcsRunSystem
    {
        readonly EcsCustomInject<BulletPool> _bulletPool = default;

        public void Run(IEcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();
            var playerFilter = world.Filter<PlayerTag>().Inc<Transform>().End();
            var attackEventFilter = world.Filter<AttackEvent>().End();
            var transformPool = world.GetPool<Transform>();
            var attackEventPool = world.GetPool<AttackEvent>();
            foreach (int entity in attackEventFilter)
            {
                ref AttackEvent atackEvent = ref attackEventPool.Get(entity);
                foreach (int playerEntity in playerFilter)
                {
                    ref Transform transform = ref transformPool.Get(playerEntity);
                    var positionPlayer = transform.Value.position;
                    var bullet = _bulletPool.Value.GetPooledObject() as MonoBeh.Bullet;
                    bullet.transform.position = positionPlayer + transform.Value.up * 0.3f;
                    bullet.gameObject.SetActive(true);
                    bullet.Shot(transform.Value.up);
                }
                world.DelEntity(entity);
            }
        }
    }
}