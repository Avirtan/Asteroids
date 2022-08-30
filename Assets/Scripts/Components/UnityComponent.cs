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
}