using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poison : BaseAttack
{
    public Poison ()
    {
        attackName = "Poison Breath";
        attackDescription = "Single poison attack";
        baseDamage = 1;
        manaCost = 3;
        isAreaAttack = false;
        levelRequirement = 3;
        isBuff = false;
        jobsRequirement = JobsEnum.Jobs.BLOODKNIGHT;
    }
}
