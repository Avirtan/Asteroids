using System;

namespace Component
{
    [Serializable]
    public struct InitEntityEvent { }
    [Serializable]
    public struct InitInputActionEvent { }
    [Serializable]
    public struct AttackEvent { }
    [Serializable]
    public struct DestroyEnemyEvent
    {
        public MonoBeh.Entity Entity;
    }
    [Serializable]
    public struct UpdateScoreEvent
    {
        public float Value;
    }
}