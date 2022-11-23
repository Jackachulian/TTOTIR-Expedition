using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TTOTIR.Inventory;

/// <summary> Class that stores all data of a game file. (Fully serializable) </summary>
public class PlayerFile
{
    /// <summary> Current health. </summary>
    private int health;

    /// <summary> Current maximum health. </summary>
    private int maxHealth;

    /// <summary> List of all spells the player owns. </summary>
    private List<Spell> spells;

    /// <summary> List of all item stacks the player has in their inventory. </summary>
    private ItemContainer inventory;
}
