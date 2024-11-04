using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GingerCat : PlayableCharacter
{
  
    public override void SpecialAbillity()
    {
        Trap trap = FindNearbyTrap();
        if (trap != null) 
        {
            if (trap is PottedPlant pottedPlant)
            {
                pottedPlant.TakeDamage(SpecialAttack);
            }
            else
            {
                ApplyDamage(trap);
                trap.ApplyDamageBack(this);
            }
        }
    }
}
