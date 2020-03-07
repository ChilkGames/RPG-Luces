using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class JobsEnum
{

    public enum Jobs
    {
        PALADIN,
        BLOODKNIGHT,
        ALL
    };

    public Jobs actualJob;
}
