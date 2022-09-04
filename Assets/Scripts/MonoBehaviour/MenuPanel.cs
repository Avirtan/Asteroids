using Component;
using UnityEngine;

namespace MonoBeh
{
    public class MenuPanel : Entity
    {
        [SerializeField] GameObject _gamePanel;
        public void StartGame()
        {
            var entity = _world.NewEntity();
            var startGameEventPool = _world.GetPool<StarGameEvent>();
            startGameEventPool.Add(entity);
            _gamePanel.SetActive(true);
            gameObject.SetActive(false);

        }
    }
}