using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TTOTIR.Combat
{
    /** <summary> 
    Stores all behavior of a Spell in combat, 
    like it use effect, use condition, alt use, passive effects, etc. 
    </summary> **/
    public abstract class SpellBehaviour : MonoBehaviour
    {
        /// <summary> Effect when a spell with the SpellData this is attached to is used. </summary>
        public abstract void use();
    }
}
