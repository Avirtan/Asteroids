using Leopotam.EcsLite;
using Component;
using Leopotam.EcsLite.Di;
using Data;
using UnityEngine;
namespace System
{
    sealed class StartGameSystem : IEcsRunSystem
    {
        readonly EcsCustomInject<GameData> _gameData = default;

        public void Run(IEcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();
            var startGameFilter = world.Filter<StarGameEvent>().End();
            var gameStateFilter = world.Filter<GameState>().End();
            var gameStatePool = world.GetPool<GameState>();
            var scoreUIEventFilter = world.GetPool<UpdateScoreUIEvent>();
            var poolFilter = world.Filter<PoolTag>().Inc<IsPause>().End();
            var isPausePool = world.GetPool<IsPause>();
            foreach (int eventEntity in startGameFilter)
            {
                GameObject.Instantiate(_gameData.Value.Player, new Vector3(0, 0, 0), Quaternion.identity);
                foreach (int poolEntity in poolFilter)
                {
                    isPausePool.Del(poolEntity);
                }
                foreach (int gameStateEntity in gameStateFilter)
                {
                    ref GameState gameState = ref gameStatePool.Get(gameStateEntity);
                    gameState.Score = 0;
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