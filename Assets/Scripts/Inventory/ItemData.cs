using UnityEngine;

namespace Inventory
{
    /// <summary> A type of item. </summary>
    public class ItemData : ScriptableObject
    {
        [Tooltip("Display name of this item.")]
        private string displayName;
        [Tooltip("Icon for this item in the inventory/UI.")]
        private Sprite icon;

        [Tooltip("Max stack size in the inventory for this type of item.")]
        private int maxStackSize;
        [Tooltip("Base sell price in gold of this item.")]
        private int sellCost;
    }
}