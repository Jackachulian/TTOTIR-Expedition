using UnityEngine;
using Combat;

namespace Inventory
{
    /// <summary> A type of spell. </summary>
    public class SpellData : ScriptableObject
    {
        [Tooltip("Display name of this spell.")]
        private string displayName;
        [Tooltip("Icon for this spell in the inventory/UI.")]
        private Sprite icon;

        [Tooltip("Magica cost to use this spell.")]
        private float cost;
        [Tooltip("Effect when using this spell in-game.")]
        private SpellBehaviour behaviour;
    }
}