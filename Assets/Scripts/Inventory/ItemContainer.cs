using System.Collections.Generic;

namespace Inventory
{
    /// <summary> Can store a certain amount of stacks of items. Implemented with a sorted dictionary. </summary>
    public class ItemContainer
    {
        /// <summary> Sorted dictionary containing all items. </summary>
        private SortedDictionary<ItemData, int> items;

        /// <summary> 
        /// Add a type and amount of item to this container.
        /// </summary>
        public void Add(ItemData type, int amount)
        {
            if (amount <= 0) return;

            // Add the key type and value amount if the key isn't in the dictionary
            if (!items.ContainsKey(type)) 
            {
                items.Add(type, amount);
            } 
            else {
                items[type] += amount; // holy heck c# is cool
            }
        }

        /// <summary> Withdraw a type of item and an amount. 
        /// Returns false if there is not enough of the type of item, without withdrawing any if so. </summary>
        public bool Remove(ItemData type, int amount)
        {
            // Return success if no items are being removed
            if (amount <= 0) return true;

            // Return fail if there is no key for the item, so 0 in this container
            if (!items.ContainsKey(type)) return false;

            // Return fail if there is an amount but less than requested amount
            if (items[type] < amount) return false;

            // Subtract amount
            items[type] -= amount;

            // Remove the key from the dictionary if value results in 0
            if (items[type] == 0) items.Remove(type);

            // Return success
            return true;
        }
    }
}