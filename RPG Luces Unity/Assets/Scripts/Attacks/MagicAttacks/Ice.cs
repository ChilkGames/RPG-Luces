using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ice : BaseAttack
{
    public Ice()
    {
        attackName = "Ice";
        attackDescription = "Freezes a single enemy";
        baseDamage = 3;
        manaCost = 5;
        isAreaAttack = false;
        levelRequirement = 1;
        isBuff = false;
        jobsRequirement = JobsEnum.Jobs.ALL;
        AddColor("color1", ColorsEnum.Colors.LIGHT_BLUE, 30);
    }
}