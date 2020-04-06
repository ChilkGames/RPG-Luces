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
        levelRequirement = 5;
        jobsRequirement = JobsEnum.Jobs.PALADIN;

        isBuff = false;

        attackDescription = "Slashes multiples enemies.";
    }
}
