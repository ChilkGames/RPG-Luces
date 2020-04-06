using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : BaseAttack
{
    public Fire ()
    {
        attackName = "Fire";
        attackDescription = "Attacks to a single enemy";
        baseDamage = 3;
        isAreaAttack = false;
        manaCost = 5;
        levelRequirement = 1;
        isBuff = false;
        jobsRequirement = JobsEnum.Jobs.ALL;
        AddColor("color1", ColorsEnum.Colors.RED, 10);
    }
}
