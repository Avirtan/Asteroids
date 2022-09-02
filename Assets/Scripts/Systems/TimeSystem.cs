using Leopotam.EcsLite;
using Component;

namespace System
{
    sealed class TimeSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();
            var timeFilter = world.Filter<TimePlay>().End();
            var timePool = world.GetPool<TimePlay>();
            foreach (int entity in timeFilter)
            {
                ref TimePlay time = ref timePool.Get(entity);
                time.Seconds += UnityEngine.Time.deltaTime;
                if (time.Seconds >= 60)
                {
                    time.Seconds = 0;
                    time.Minutes += 1;
                }
                if (time.Minutes >= 60)
                {
                    time.Minutes = 0;
                    time.Hours += 1;
                }
            }
        }
    }
}