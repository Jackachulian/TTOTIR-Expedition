using UnityEngine;
using System.Collections.Generic;
using TTOTIR.Inventory;

namespace TTOTIR.Combat
{
    /// <summary> Attached to the in-game player. Stoes magica, spells and inventory.
    /// Also attached to player is Character which stores health, defense and speed stats. </summary>
    public class Player : MonoBehaviour
    {
        [Tooltip("Current amount of stored magica.")]
        [SerializeField] private float magica;

        [Tooltip("Max amount of magica the player can store.")]
        [SerializeField] private float maxMagica;

        [Tooltip("Spells that the player has currently equipped.")]
        [SerializeField] private List<Spell> spells;

        [Tooltip("Index of the spell the player currently has equipped.")]
        [SerializeField] private int currentSpell;

        [Tooltip("List of all owned spells.")]
        [SerializeField] private List<Spell> ownedSpells;

        [Tooltip("All items in the player's inventory.")]
        [SerializeField] private ItemContainer inventory;
    }
}