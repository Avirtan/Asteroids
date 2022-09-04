using Component;
using UnityEngine;
using TMPro;
namespace MonoBeh
{
    public class GameOverPanel : Entity
    {
        [SerializeField] GameObject _gamePanel;
        [SerializeField] TextMeshProUGUI _textMesh;
        public void StartGame()
        {
            var entity = _world.NewEntity();
            var startGameEventPool = _world.GetPool<StarGameEvent>();
            startGameEventPool.Add(entity);
            _gamePanel.SetActive(true);
            gameObject.SetActive(false);
        }

        public void SetScore(float value)
        {
            _gamePanel.SetActive(false);
            _textMesh.text = value.ToString();
        }

    }
}