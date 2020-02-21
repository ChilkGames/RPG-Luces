using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slash : BaseAttack
{
    public Slash()
    {
        attackName = "Slash";
        baseDamage = 5;
        manaCost = 0;
        isAreaAttack = false;

        attackDescription = "Slashes one enemy.";
    }
}
