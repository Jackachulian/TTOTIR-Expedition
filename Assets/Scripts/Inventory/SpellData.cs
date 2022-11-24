using UnityEngine;
using TTOTIR.Combat;

namespace TTOTIR.Inventory
{
    /// <summary> A type of spell. </summary>
    [CreateAssetMenu(fileName = "Spell", menuName = "TTOTIR/Inventory/Spell")]
    public class SpellData : ScriptableObject
    {
        [Tooltip("Display name of this spell.")]
        public string displayName;
        [Tooltip("Icon for this spell in the inventory/UI.")]
        public Sprite icon;

        [Tooltip("Magica cost to use this spell.")]
        public float cost;
        [Tooltip("Effect when using this spell in-game.")]
        public SpellBehaviour behaviour;
    }
}