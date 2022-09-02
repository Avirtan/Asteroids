
using System;

namespace Component
{
    [Serializable]
    public struct TimePlay
    {
        public float Seconds;
        public float Minutes;
        public float Hours;
    }

    [Serializable]
    public struct TimeDelay
    {
        public float Value;
    }
}