using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChampagneSupernova : BaseAttack
{
    public ChampagneSupernova()
    {
        attackName = "Champagne Supernova";
        attackDescription = "Massive purple damage to all enemies";
        baseDamage = 30;
        isAreaAttack = true;
        levelRequirement = 5;
        manaCost = 20;
        isBuff = false;
        jobsRequirement = JobsEnum.Jobs.CASTERDPS;
        AddColor("color1", ColorsEnum.Colors.PURPLE, 30);
    }
}
