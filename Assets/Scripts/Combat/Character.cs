using UnityEngine;
using UnityEditor;

namespace TTOTIR.Combat
{
    /// <summary> 
    /// The GameObject this is attached to is a character that can move around, take damage and be defeated.
    /// The physics part of this component is kind of a recoding of Rigidbody but with way less features.
    /// <summary>
    public class Character : MonoBehaviour
    {
        [Tooltip("The type of character this is.")]
        [SerializeField] private CharacterData data;

        [Tooltip("Current health of this character.")]
        [SerializeField] private float health;

        [Tooltip("Type of character this is (Player/Enemy).")]
        [SerializeField] private CharacterType type; 

        [Tooltip("Max health of this character.")]
        public int maxHealth { get { return data.maxHealth; } }

        [Tooltip("Defense of this character.")]
        public int defense { get { return data.defense; } }

        [Tooltip("Acceleration when accelerating.")] // meters/second^2
        public float moveAccel { get { return data.moveAccel; } }

        [Tooltip("Acceleration when deccelerating.")]
        public float moveDecel { get { return data.moveDecel; } } // meters/second^2

        [Tooltip("Top speed this character can accelerate to via movement.")]
        public float topSpeed { get { return data.topSpeed; } } // meters/second


        // The type of character that characters can be - player or enemies.
        public enum CharacterType
        {
            Player,
            Enemy
        }

        void Start()
        {
            health = maxHealth;
        }

        public virtual void Update()
        {
            
        }

        // Take damage from a hurtbox or other source.
        public void TakeDamage(float damage, Element element)
        {
            this.health -= damage;
        }

        // Spawn a projectile from this character.
        public static void SpawnProjectile(GameObject projPrefab, Transform spawnPoint, float speed)
        {
            GameObject projectileObject = Instantiate(projPrefab, spawnPoint.position, spawnPoint.rotation);
            projectileObject.GetComponent<Rigidbody>().velocity = spawnPoint.forward * speed;
        }
    }
}