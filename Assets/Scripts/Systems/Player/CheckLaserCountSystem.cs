using Leopotam.EcsLite;
using Component;
using Leopotam.EcsLite.Di;
using Data;

namespace System
{
    sealed class CheckLaserCountSystem : IEcsRunSystem
    {
        readonly EcsCustomInject<GameData> _gameData = default;

        public void Run(IEcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();
            var playerFilter = world.Filter<PlayerTag>().Inc<Transform>().Inc<Entity>().Inc<LaserComponent>().End();
            var updateLaserFilter = world.Filter<UpdateLaser>().End();
            var laserComponentPool = world.GetPool<LaserComponent>();
            var updateLaserPool = world.GetPool<UpdateLaser>();
            foreach (int entityUpdate in updateLaserFilter)
            {
                ref UpdateLaser updateLaser = ref updateLaserPool.Get(entityUpdate);
                foreach (int playerEntity in playerFilter)
                {
                    ref LaserComponent laser = ref laserComponentPool.Get(playerEntity);
                    if (laser.Count < _gameData.Value.MaxLaserCount)
                    {
                        updateLaser.Text.gameObject.SetActive(true);
                        updateLaser.Slider.gameObject.SetActive(true);
                        updateLaser.Value += UnityEngine.Time.deltaTime;
                        updateLaser.Slider.value = updateLaser.Value / _gameData.Value.CooldownLaser;
                        if (updateLaser.Value > _gameData.Value.CooldownLaser)
                        {
                            laser.Count += 1;
                            updateLaser.Value = 0;
                        }
                    }
                    else
                    {
                        updateLaser.Slider.value = 1;
                        updateLaser.Text.gameObject.SetActive(false);
                        updateLaser.Slider.gameObject.SetActive(false);
                    }
                }
            }
        }
    }
}