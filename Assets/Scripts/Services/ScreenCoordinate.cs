using System;
using UnityEngine;
using Util;

namespace Service
{
    public class ScreenCoordinate
    {
        private float _maxY;
        private float _maxX;
        private float _minY;
        private float _minX;
        private float offsetSpawnEnemy = 1f;

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

        public bool CheckOutScreen(Vector2 position, bool useOffset = false)
        {
            var offset = 0f;
            if(useOffset)
            {
                offset = offsetSpawnEnemy;
            }
            if (position.x < _minX - offset || position.x > _maxX + offset ||
                position.y < _minY - offset || position.y > _maxY + offset)
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

        public Vector2 RandomCoordinateOutScreen()
        {
            var rndX = (Side)UnityEngine.Random.Range(0, 4);
            var positionX = 0f;
            var positionY = 0f;
            switch (rndX)
            {
                case Side.LEFT:
                    positionX = _minX - offsetSpawnEnemy;
                    positionY = UnityEngine.Random.Range(_minY, _maxY);
                    break;
                case Side.TOP:
                    positionX = UnityEngine.Random.Range(_minX, _maxX);
                    positionY = _maxY + offsetSpawnEnemy;
                    break;
                case Side.RIGHT:
                    positionX = _maxX + offsetSpawnEnemy;
                    positionY = UnityEngine.Random.Range(_minY, _maxY);
                    break;
                case Side.BOTTOM:
                    positionX = UnityEngine.Random.Range(_minX, _maxX);
                    positionY = _minY - offsetSpawnEnemy;
                    break;
            }
            var position = new Vector2(positionX, positionY);
            return position;
        }
    }
}
