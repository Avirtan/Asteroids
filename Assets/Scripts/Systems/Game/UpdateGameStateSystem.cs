using Leopotam.EcsLite;
using Component;

namespace System
{
    sealed class UpdateGameStateSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();
            var gameStateFilter = world.Filter<GameState>().End();
            var scoreEventFilter = world.Filter<UpdateScoreEvent>().End();
            var scoreUIEventFilter = world.GetPool<UpdateScoreUIEvent>();
            var gameStatePool = world.GetPool<GameState>();
            var updateScoreEventPool = world.GetPool<UpdateScoreEvent>();
            foreach (int eventEntity in scoreEventFilter)
            {
                ref UpdateScoreEvent updateScoreEvent = ref updateScoreEventPool.Get(eventEntity);
                foreach (int gameStateEntity in gameStateFilter)
                {
                    ref GameState gameState = ref gameStatePool.Get(gameStateEntity);
                    gameState.Score += updateScoreEvent.Value;
                    var entity = world.NewEntity();
                    scoreUIEventFilter.Add(entity);
                    ref UpdateScoreUIEvent scoreUIEvent = ref scoreUIEventFilter.Get(entity);
                    scoreUIEvent.Value = gameState.Score;
                }
                world.DelEntity(eventEntity);
            }
        }
    }
}