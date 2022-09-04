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
            var playerFilter = world.Filter<PlayerTag>().Inc<Transform>().Inc<Entity>().Inc<LaserComponent>().End();
            var attackEventFilter = world.Filter<AttackEvent>().End();
            var transformPool = world.GetPool<Transform>();
            var attackEventPool = world.GetPool<AttackEvent>();
            var laserComponentPool = world.GetPool<LaserComponent>();
            var entityPool = world.GetPool<Entity>();
            foreach (int attackEventEntity in attackEventFilter)
            {
                ref AttackEvent atackEvent = ref attackEventPool.Get(attackEventEntity);
                foreach (int playerEntity in playerFilter)
                {
                    ref Transform transform = ref transformPool.Get(playerEntity);
                    ref Entity entity = ref entityPool.Get(playerEntity);
                    ref LaserComponent laserComponent = ref laserComponentPool.Get(playerEntity);
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
                            if (laserComponent.Count == 0) break;
                            var enemys = UnityEngine.Physics2D.RaycastAll(transform.Value.position, transform.Value.up, 
                                float.MaxValue, UnityEngine.LayerMask.GetMask("Enemy"));
                            var player = entity.Value as MonoBeh.Player;
                            laserComponent.Count -= 1;
                            //var updateLaserCountEntity = world.NewEntity();
                            //updateLaserCountUIPool.Add(updateLaserCountEntity);
                            //ref UpdateLaserCountUI updateLaserCount = ref updateLaserCountUIPool.Get(updateLaserCountEntity);
                            //updateLaserCount.Value = laserComponent.Count;
                            player.StartCoroutine(player.ShowLaser(laserComponent.LineRenderer, transform.Value.up));
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
                world.DelEntity(attackEventEntity);
            }
        }
    }
}