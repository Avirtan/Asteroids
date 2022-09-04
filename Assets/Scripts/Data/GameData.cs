using MonoBeh;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "GameData", menuName = "Config/GameData", order = 0)]
    public class GameData : ScriptableObject
    {
        public Entity Player;

    }
}
