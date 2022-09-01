using System;
using UnityEngine;

namespace Service
{
    public class ScreenCoordinate
    {
        private float _maxY;
        private float _maxX;
        private float _minY;
        private float _minX;

        public float MaxPositionX { get => _maxX; }
        public float MaxPositionY { get => _maxY; }
        public float MinPositionY { get => _minY; }
        public float MinPositionX { get => _minX; }

        public ScreenCoordinate(Camera camera)
        {
            var bottomLeft = camera.ScreenToWorldPoint(new Vector2(0, 0));
            var topRight = camera.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
            _maxX = topRight.x;
            _minX = bottomLeft.x;
            _maxY = topRight.y;
            _minY = bottomLeft.y;
        }

        public bool CheckOutScreen(Vector2 position)
        {
            if (position.x < _minX || position.x > _maxX ||
                position.y < _minY || position.y > _maxY)
            {
                return true;
            }
            return false;
        }

        public Vector3 CheckOutScreenPlayer(Vector3 positionPlayer)
        {
            if (positionPlayer.x < _minX)
            {
                positionPlayer.x = _maxX;
                return positionPlayer;
            }
            else if (positionPlayer.x > _maxX)
            {
                positionPlayer.x = _minX;
                return positionPlayer;
            }
            else if (positionPlayer.y < _minY)
            {
                positionPlayer.y = _maxY;
                return positionPlayer;
            }
            else if (positionPlayer.y > _maxY)
            {
                positionPlayer.y = _minY;
                return positionPlayer;
            }
            else
            {
                return Vector3.zero;
            }
        }

        public string GetCurrentCoordinate(Vector3 position)
        {
            return String.Format("X:{0:#.0} Y:{1:#.0}", position.x + _maxX, position.y + _maxY);
        }
    }
}
