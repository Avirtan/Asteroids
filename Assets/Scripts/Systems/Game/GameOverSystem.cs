using Leopotam.EcsLite;
using Component;
namespace System
{
    sealed class GameOverSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();
            var startGameFilter = world.Filter<GameOverEvent>().End();
            var gamePanelFilter = world.Filter<PanelState>().End();
            var gameStateFilter = world.Filter<GameState>().End();
            var saucerFilter = world.Filter<EnemyTag>().Inc<Transform>().End();
            var poolFilter = world.Filter<PoolTag>().End();
            var isPausePool = world.GetPool<IsPause>();
            var panelStatePool = world.GetPool<PanelState>();
            var gameStatePool = world.GetPool<GameState>();
            var transformPool = world.GetPool<Transform>();
            foreach (int eventEntity in startGameFilter)
            {
                foreach (int poolEntity in poolFilter)
                {
                    if (!isPausePool.Has(poolEntity))
                    {
                        isPausePool.Add(poolEntity);
                    }
                }
                foreach (int gameStateEntity in gameStateFilter)
                {
                    ref GameState gameState = ref gameStatePool.Get(gameStateEntity);
                    foreach (int panelEntity in gamePanelFilter)
                    {
                        ref PanelState panel = ref panelStatePool.Get(panelEntity);
                        panel.GameOverPanel.SetScore(gameState.Score);
                        panel.GameOverPanel.gameObject.SetActive(true);
                    }
                    gameState.Score = 0;
                }
                foreach (int enemtEntity in saucerFilter)
                {
                    ref Transform transform = ref transformPool.Get(enemtEntity);
                    transform.Value.gameObject.SetActive(false);
                }
                world.DelEntity(eventEntity);
            }
        }
    }
}