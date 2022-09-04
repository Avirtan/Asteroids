using System;

namespace Component
{
    [Serializable]
    public struct MoveSpeed
    {
        public float Value;
    }
    [Serializable]
    public struct RotateSpeed
    {
        public float Value;
    }
    [Serializable]
    public struct LaserComponent
    {
        public UnityEngine.LineRenderer LineRenderer;
        public float Count;
    }
}