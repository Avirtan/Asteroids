using Leopotam.EcsLite;
using Component;
using Leopotam.EcsLite.Di;
using Service;

namespace System
{
    sealed class DestroyEnemySystem : IEcsRunSystem
    {
        readonly EcsCustomInject<PartMeteorPool> _partMeteorPool = default;

        public void Run(IEcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();
            var attackEventFilter = world.Filter<DestroyEnemyEvent>().End();
            var destroyEnemyEventPool = world.GetPool<DestroyEnemyEvent>();
            var transformPool = world.GetPool<Transform>();
            var rigidbodyPool = world.GetPool<Rigidbody>();
            var updateScorePool = world.GetPool<UpdateScoreEvent>();
            foreach (int entity in attackEventFilter)
            {
                ref DestroyEnemyEvent destroyEvent = ref destroyEnemyEventPool.Get(entity);
                if (transformPool.Has(destroyEvent.Entity.IdEntity))
                {
                    var enemyEntity = destroyEvent.Entity;
                    ref Transform transform = ref transformPool.Get(enemyEntity.IdEntity);
                    ref Rigidbody rigidbody = ref rigidbodyPool.Get(enemyEntity.IdEntity);
                    var updateScoreEntity = world.NewEntity();
                    updateScorePool.Add(updateScoreEntity);
                    ref UpdateScoreEvent updateScoreEvent = ref updateScorePool.Get(updateScoreEntity);
                    switch (enemyEntity)
                    {
                        case MonoBeh.Meteor:
                            updateScoreEvent.Value = 10;
                            for (int i = 0; i < 2; i++)
                            {
                                var partMeteor = _partMeteorPool.Value.GetPooledObject() as MonoBeh.PartMeteor;
                                partMeteor.transform.position = transform.Value.position;
                                partMeteor.gameObject.SetActive(true);
                                partMeteor.MoveMeteor(rigidbody.Value.velocity);
                            }
                            break;
                        case MonoBeh.Saucer:
                            updateScoreEvent.Value = 15;
                            break;
                        case MonoBeh.PartMeteor:
                            updateScoreEvent.Value = 5;
                            break;
                        default:
                            break;
                    }

                    transform.Value.gameObject.SetActive(false);
                }
                world.DelEntity(entity);
            }
        }
    }
}