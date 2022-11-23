using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TTOTIR.Combat.Projectile
{
    /// Summary: Makes an object move and is able to collide with characters and other projectiles 
    /// via the hitbox that is also on this gameobject that projectile is on. 
    /// Should be attached to a GameObject with a rigidbody.
    public class Projectile : MonoBehaviour
    {
        [Tooltip("Amount of bounces when colliding with terrain.")]
        [SerializeField] private int bounces;

        [Tooltip("Lifespan of this projectile in seconds.")]
        [SerializeField] private float lifespan;

        [Tooltip("Amount of enemy pierces. Rigidbody must be a Trigger, so no bounces.")]
        [SerializeField] private int pierces;

        [Tooltip("Behaviour of this projectiles' explosion on destroy.")]
        [SerializeField] private Explosion explosion;

        [System.Serializable]
        public class Explosion
        {
            [Tooltip("Whether or not this projectile has an explosion.")]
            public bool enabled;

            [Tooltip("Damage component of this explosion. horizontalForce is used for explosion force.")]
            public Damager damager;

            [Tooltip("Radius of the explosion.")]
            public float radius; // meters

            [Tooltip("Whether damage should decrease linearly from the explosion center.")]
            public bool linearDamageDecrease;
        }


        // Time this projectile was created, used for destroying itself when lifespan is exceeded.
        private float spawnTime;

        // Bounce height determined by the physics material on the rigidbody, specifically bounciness and bounce combine.
        // For reference the floor's friction is 0.1 as of writing this.

        // cached rigidbody
        Rigidbody rb;
        
        void Start()
        {
            rb = GetComponent<Rigidbody>();
            spawnTime = Time.time;
        }

        void Update()
        {
            if (Time.time > spawnTime + lifespan) {
                DestroyProjectile();
            }
        }

        // NOTE!!! Projectile's script has been set to run after Hurtbox and other similar scripts!
        // So this projectile can safely destroy itself on collision within this script,
        // without breaking other scripts.
        void OnCollisionEnter(Collision collision)
        {
            bounces -= 1;
            if (bounces < 0) 
            {
                DestroyProjectile();
            }
        }

        // OnTriggerEnter is only really used if this is a piercing projectile.
        // Which means OnCollisionEnter wont be used at all because this has a Trigger collider.
        void OnTriggerEnter(Collider other)
        {
            // Lower pierce counter when colliding with an enemy
            if (other.gameObject.layer == LayerMask.NameToLayer("Character"))
            {
                pierces -= 1;
                Debug.Log(pierces);
                if (pierces < 0)
                {
                    Debug.Log("AHHHHHHHHHHHHHHHH A\"OHIJD:A:SJDIH\"O:BAKS:DUIJ");
                    DestroyProjectile();
                }
            }

            // Destroy this projectile when colliding with terrain
            else if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
            {
                DestroyProjectile();
            }
        }

        // Effect when this projectile is destroyed, such as spawning other projectiles, etc.
        // If overridden, should at least contain Destroy(gameObject) somewhere in the code so it doesn't last forever.
        void DestroyProjectile()
        {
            Debug.Log("destroying "+this);
            if (explosion.enabled) {
                // Find all collisions inside explosion sphere
                foreach (Collider collider in Physics.OverlapSphere(transform.position, explosion.radius)) 
                {
                    // For each player, deal damage and apply knockback
                    if (collider.gameObject.layer == LayerMask.NameToLayer("Character")) 
                    {
                        Character character = collider.gameObject.GetComponent<Character>();
                        if (character != null) 
                        {
                            // Deal damage
                            if (explosion.linearDamageDecrease) {
                                //scale damage linearly from explosion center
                                // centerProximity is a float from 0.0 (sphere edge) to 1.0 (sphere center)
                                float centerProximity = (explosion.radius - (character.transform.position - transform.position).magnitude) / explosion.radius;
                                character.TakeDamage(explosion.damager.damage * centerProximity, explosion.damager.element);
                            }

                            else {
                                character.TakeDamage(explosion.damager.damage, explosion.damager.element);
                            }

                            // apply knockback - force decreases linearly from explosion center    
                            Rigidbody rb = character.GetComponent<Rigidbody>();    
                            rb.AddExplosionForce(explosion.damager.horizontalForce, transform.position, explosion.radius, 0, ForceMode.Impulse);

                            // add vertical force from damager component
                            rb.AddForce(explosion.damager.upwardsForce * Vector3.up, ForceMode.Impulse);
                        } 
                        
                        else {
                            Debug.LogError(collider.gameObject + " is in the Character layer but is not a character!");
                        }
                    }
                }
            }

            Destroy(gameObject);
        }
    }
}
