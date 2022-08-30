using System;

namespace Component {
    [Serializable]
    public struct Transform
    {
       public UnityEngine.Transform Value;
    }

    [Serializable]
    public struct Rigidbody
    {
        public UnityEngine.Rigidbody2D Value;
    }

    [Serializable]
    public struct Camera
    {
        public UnityEngine.Camera Value;
        public UnityEngine.Vector2 BottomLeft;
        public UnityEngine.Vector2 TopRight;
    }
}