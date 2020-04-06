using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedOfLight : BaseAttack
{
    public SpeedOfLight ()
    {
        attackName = "Speed of Ligjt";
        attackDescription = "Raises and ally speed";
        baseDamage = 0;
        manaCost = 5;
        isAreaAttack = false;
        levelRequirement = 3;
        isBuff = true;
        jobsRequirement = JobsEnum.Jobs.ALL;
    }
}
