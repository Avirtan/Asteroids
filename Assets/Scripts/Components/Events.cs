using System;

namespace Component
{
    [Serializable]
    public struct InitEntityEvent { }
    [Serializable]
    public struct InitInputActionEvent { }
    [Serializable]
    public struct AttackEvent
    {
        public Util.TypeAttack Value;
    }
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
    [Serializable]
    public struct UpdateScoreUIEvent
    {
        public float Value;
    }
    [Serializable]
    public struct StarGameEvent { }
    [Serializable]
    public struct GameOverEvent { }
}