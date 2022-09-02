using Leopotam.EcsLite;
using Component;
using Service;
using Leopotam.EcsLite.Di;

namespace System
{
    sealed class FollowSystem : IEcsRunSystem
    {

        public void Run(IEcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();
            var playerFilter = world.Filter<PlayerTag>().Inc<Transform>().End();
            var saucerFilter = world.Filter<SaucerTag>().Inc<Rigidbody>().End();
            var transformPool = world.GetPool<Transform>();
            var moveSpeedPool = world.GetPool<MoveSpeed>();
            var rigidbodyPool = world.GetPool<Rigidbody>();
            foreach (int playerEntity in playerFilter)
            {
                ref Transform playerTransform = ref transformPool.Get(playerEntity);
                foreach (int saucerEntity in saucerFilter)
                {
                    ref Transform transform = ref transformPool.Get(saucerEntity);
                    ref Rigidbody rigidbody = ref rigidbodyPool.Get(saucerEntity);
                    ref MoveSpeed moveSpeed = ref moveSpeedPool.Get(saucerEntity);
                    var dir = (playerTransform.Value.position - transform.Value.position).normalized;
                    rigidbody.Value.velocity = dir * moveSpeed.Value;
                }
            }
        }
    }
}