using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolyDiver : BaseAttack
{
    public HolyDiver ()
    {
        attackName = "Holy Diver";
        attackDescription = "Massive white attack";
        baseDamage = 10;
        manaCost = 15;
        isAreaAttack = true;
        levelRequirement = 2;
        isBuff = false;
        jobsRequirement = JobsEnum.Jobs.PALADIN;
        AddColor("color1", ColorsEnum.Colors.WHITE, 50);
    }
}
