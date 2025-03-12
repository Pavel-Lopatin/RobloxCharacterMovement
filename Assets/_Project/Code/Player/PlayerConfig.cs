using UnityEngine;

namespace Roblox
{
    [CreateAssetMenu(fileName = "NewPlayerConfig", menuName = "Scriptable Objects/PlayerConfig")]
    public class PlayerConfig : ScriptableObject
    {
        [field: SerializeField, Range(1f, 20f)] public float MoveSpeed { get; private set; }
        [field: SerializeField, Range(1f, 20f)] public float JumpForce { get; private set; }
        [field: SerializeField, Range(0.1f, 2f)] public float JumpMoveCoefficient { get; private set; }
    }
}

