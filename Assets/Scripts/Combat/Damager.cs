using UnityEngine;

namespace TTOTIR.Combat
{
    [System.Serializable]
    public class Damager
    {
        [Tooltip("Damage to deal when colliding with characters, projectiles, etc.")]
        public int damage;

        [Tooltip("The element with which to deal damage.")]
        public Element element;

        [Tooltip("Force applied on collision, pointed away from center on X-Z plane.")]
        public float horizontalForce; // mass*meters/second^2

        [Tooltip("Force applied on collision, pointed upwards.")]
        [SerializeField] public float upwardsForce; // mass*meters/second^2
    }
}