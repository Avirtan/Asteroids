using Util;
using UnityEngine;
using Service;

namespace MonoBeh
{
    public class Meteor : Entity
    {
        [SerializeField] Rigidbody2D _rb;
        [SerializeField] float moveSpeed;

        public void MoveMeteor(Side side, ScreenCoordinate screenCoordinate)
        {
            var dir = new Vector2();
            var rndY = Random.Range(screenCoordinate.MinPositionY, screenCoordinate.MaxPositionY) / screenCoordinate.MaxPositionY;
            var rndX = Random.Range(screenCoordinate.MinPositionX, screenCoordinate.MaxPositionX) / screenCoordinate.MaxPositionX;
            switch (side)
            {
                case Side.LEFT:
                    dir.x = 1;
                    dir.y = rndY;
                    _rb.AddForce(dir * moveSpeed);
                    break;
                case Side.TOP:
                    dir.x = rndX;
                    dir.y = -1;
                    _rb.AddForce(dir * moveSpeed);
                    break;
                case Side.RIGHT:
                    dir.x = -1;
                    dir.y = -rndY;
                    _rb.AddForce(dir * moveSpeed);
                    break;
                case Side.BOTTOM:
                    dir.x = -rndX;
                    dir.y = 1;
                    _rb.AddForce(dir * moveSpeed);
                    break;
            }
        }
    }
}
