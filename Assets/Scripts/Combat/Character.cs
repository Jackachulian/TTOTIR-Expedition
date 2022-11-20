using UnityEngine;
using UnityEditor;

namespace Combat
{
    /// <summary> 
    /// The GameObject this is attached to is a character that can move around, take damage and be defeated.
    /// The physics part of this component is kind of a recoding of Rigidbody but with way less features.
    /// <summary>
    public class Character : MonoBehaviour
    {
        [Tooltip("Current health of this character.")]
        public int health;

        [Tooltip("Max health of this character.")]
        public int maxHealth;

        [Tooltip("Defense of this character. Reduces damage taken.")]
        public int defense;

        [Tooltip("Acceleration when accelerating (m/s^2).")]
        public float moveAccel;

        [Tooltip("Acceleration when deccelerating (m/s^2).")]
        public float moveDecel;

        [Tooltip("Top speed this character can accelerate to via movement (m/s).")]
        public float topSpeed;

        [Tooltip("How fast this character can attack (attacks/s).")]
        public float attackSpeed;
    }
}