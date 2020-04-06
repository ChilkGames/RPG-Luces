using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupermassiveBlackHole : BaseAttack
{
    public SupermassiveBlackHole ()
    {
        attackName = "Supermassive Black Hole";
        attackDescription = "Massive black attack";
        baseDamage = 30;
        manaCost = 30;
        isAreaAttack = true;
        levelRequirement = 10;
        isBuff = false;
        jobsRequirement = JobsEnum.Jobs.CASTERDPS;
        AddColor("color1", ColorsEnum.Colors.BLACK, 50);
        AddColor("color2", ColorsEnum.Colors.PURPLE, 20);
    }
}
