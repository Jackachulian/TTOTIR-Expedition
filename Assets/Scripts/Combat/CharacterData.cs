using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TTOTIR.Combat
{
    /// <summary> A type of item. </summary>
    [CreateAssetMenu(fileName = "Character", menuName = "TTOTIR/Character")]
    public class CharacterData : ScriptableObject
    {
        [Tooltip("Display name of this character.")]
        public string displayName;

        [Tooltip("Max health of the character.")]
        public int maxHealth = 20;

        [Tooltip("Defense of the character. Reduces damage taken.")]
        public int defense = 5;

        [Tooltip("Acceleration when accelerating (m/s^2).")]
        public float moveAccel = 20;

        [Tooltip("Acceleration when deccelerating (m/s^2).")]
        public float moveDecel = 25;

        [Tooltip("Top speed the character can accelerate to with its own movement (m/s).")]
        public float topSpeed = 10;
    }
}