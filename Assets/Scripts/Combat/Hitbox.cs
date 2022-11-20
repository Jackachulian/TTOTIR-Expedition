using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combat
{
    /// <summary> Attach to anything that deals damage and/or takes damage.
    public class Hitbox : MonoBehaviour
    {
        [Tooltip("Damage to deal when colliding with characters, projectiles, etc.")]
        private int damage;

        [Tooltip("Force to apply on collision.")]
        private float force; // mass * gameunit / second^2
    }
}
