using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircularSlash : BaseAttack
{
    public CircularSlash()
    {
        attackName = "Circular Slash";
        baseDamage = 2;
        manaCost = 0;
        isAreaAttack = true;

        attackDescription = "Slashes multiples enemies.";
    }
}
