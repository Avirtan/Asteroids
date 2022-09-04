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
                    switch (atackEvent.Value)
                    {
                        case Util.TypeAttack.BULLET:
                            var positionPlayer = transform.Value.position;
                            var bullet = _bulletPool.Value.GetPooledObject() as MonoBeh.Bullet;
                            bullet.transform.position = positionPlayer + transform.Value.up * 0.3f;
                            bullet.gameObject.SetActive(true);
                            bullet.Shot(transform.Value.up);
                            break;
                        case Util.TypeAttack.LASER:
                            var enemys = UnityEngine.Physics2D.RaycastAll(transform.Value.position, transform.Value.up, 
                                float.MaxValue, UnityEngine.LayerMask.GetMask("Enemy"));
                            for (int i = 0; i < enemys.Length; i++)
                            {
                                var entityEnemy = enemys[i].collider.gameObject.GetComponent<MonoBeh.Entity>();
                                var entityDestroy = world.NewEntity();
                                var destroyEnemyEventPool = world.GetPool<DestroyEnemyEvent>();
                                destroyEnemyEventPool.Add(entityDestroy);
                                ref DestroyEnemyEvent destroyEvent = ref destroyEnemyEventPool.Get(entityDestroy);
                                destroyEvent.Entity = entityEnemy;
                                entityEnemy.gameObject.SetActive(false);
                            }
                            UnityEngine.Debug.DrawRay(transform.Value.position, transform.Value.up * 100);
                            break;
                    }
                }
                world.DelEntity(entity);
            }
        }
    }
}