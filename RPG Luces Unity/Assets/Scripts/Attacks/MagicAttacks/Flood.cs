using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flood : BaseAttack
{
    public Flood()
    {
        attackName = "Flood";
        attackDescription = "Attacks multiple enemies with a massive wave";
        baseDamage = 5;
        isAreaAttack = true;
        manaCost = 20;
        levelRequirement = 3;
        isBuff = false;
        jobsRequirement = JobsEnum.Jobs.CASTERDPS;
        AddColor("color1", ColorsEnum.Colors.BLUE, 20);
    }
}
