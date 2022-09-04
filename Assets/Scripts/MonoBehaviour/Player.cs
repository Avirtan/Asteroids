using Component;
using System.Collections;
using UnityEngine;

namespace MonoBeh
{
    public class Player : Entity
    {
        private void OnCollisionEnter2D(Collision2D collision)
        {
            var entity = _world.NewEntity();
            var gameOverEventPool = _world.GetPool<GameOverEvent>();
            gameOverEventPool.Add(entity);
            _world.DelEntity(_idEntity);
            Destroy(gameObject);
        }

        public IEnumerator ShowLaser(UnityEngine.LineRenderer lineRenderer, Vector2 dir)
        {
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, dir * 50);
            lineRenderer.gameObject.SetActive(true);
            var time = 0f;
            while(time < 0.1f)
            {
                time += Time.deltaTime;
                lineRenderer.SetPosition(0, transform.position);
                yield return null;
            }
            lineRenderer.gameObject.SetActive(false);
        }
    }
}