using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MonoBeh
{
    public class Test : MonoBehaviour
    {
        [SerializeField] GameObject player;
        Camera cam;

        void Start()
        {
            cam = Camera.main;
        }

        void Update()
        {
            Vector3 screenPos = cam.WorldToScreenPoint(player.transform.position);
            float width = cam.pixelWidth;
            float height = cam.pixelHeight;
            var pos = player.transform.position;
            Vector2 bottomLeft = cam.ScreenToWorldPoint(new Vector2(0, 0));
            Vector2 bottomRight = cam.ScreenToWorldPoint(new Vector2(width, 0));
            Vector2 topLeft = cam.ScreenToWorldPoint(new Vector2(0, height));
            Vector2 topRight = cam.ScreenToWorldPoint(new Vector2(width, height));
            if(pos.x < bottomLeft.x)
            {
                player.transform.position = new Vector3(topRight.x, pos.y, pos.z);
            }else if (pos.x > topRight.x)
            {
                player.transform.position = new Vector3(bottomLeft.x, pos.y, pos.z);
            }
            else if (pos.y < bottomLeft.y)
            {
                player.transform.position = new Vector3(pos.x, topRight.y, pos.z);
            }
            else if (pos.y > topRight.y)
            {
                player.transform.position = new Vector3(pos.x, bottomLeft.y, pos.z);
            }
        }
    }
}