using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TTOTIR.Combat
{
    /// <summary> Attach to anything that deals damage.
    [System.Serializable]
    public class Hurtbox : MonoBehaviour
    {
        [Tooltip("Damage component of this projectile.")]
        [SerializeField] private Damager damager;

        [Tooltip("The mode in which to apply force on collision.")]
        [SerializeField] private HurtboxForceMode forceMode;

        public enum HurtboxForceMode
        {
            [Tooltip("Apply force proportional to velocity and mass, in direction of velocity. Does not use horizontalForce.")]
            Impulse,
            [Tooltip("Apply force away from projectile's center along with collision.")]
            Explosion,
            [Tooltip("Apply force in the direction the projectile is facing.")]
            Directional,
            [Tooltip("Do not apply forces, except upwardsForce.")]
            None
        }

        // Note: only the player gets invincibility time when getting hit, enemies do not
        [Tooltip("Invincibility time given to the player when they are hit. 0 to use default (0.75s).")]
        [SerializeField] public float invincibleTime = 0f; // seconds

        // If this hurtbox's collider is not a trigger, still use the trigger code (damage & knockback)
        void OnCollisionEnter(Collision collision) {
            OnTriggerEnter(collision.collider);
        }

        void OnTriggerEnter(Collider other) {
            // Deal damage and apply knockback when colliding with a character.
            Character character = other.gameObject.GetComponent<Character>();
            if (character != null) {
                // deal damage
                character.TakeDamage(damager.damage, damager.element);

                // apply knockback
                Rigidbody rb = character.GetComponent<Rigidbody>();

                // apply force as if this were a normal collider and not a trigger (also included in explosion)
                if (forceMode == HurtboxForceMode.Impulse || forceMode == HurtboxForceMode.Explosion)
                {
                    Rigidbody hurtRb = GetComponent<Rigidbody>();
                    if (hurtRb != null) rb.AddForce(hurtRb.velocity * hurtRb.mass, ForceMode.Impulse);
                }

                // apply force from outward as an explosion force.
                if (forceMode == HurtboxForceMode.Explosion) 
                {
                    Vector3 horizontal = (character.transform.position - transform.position);
                    horizontal.y = 0;
                    horizontal.Normalize();
                    rb.AddForce(horizontal * damager.horizontalForce, ForceMode.Impulse);
                }

                // apply force in the direction this hitbox is facing
                else if (forceMode == HurtboxForceMode.Directional)
                {
                    rb.AddForce(transform.forward * damager.horizontalForce, ForceMode.Impulse);
                }

                // (upward force behaves the same for all force modes, even none)
                rb.AddForce(Vector3.up * damager.upwardsForce, ForceMode.Impulse);
            }
        }
    }
}
