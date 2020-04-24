using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public static class StatusEnum
{
    public enum Status
    {
        POISONED,
        HASTED,
        SLOWED,
        HEALING,
        FROZEN,
        BURNED,
        DEFUP,
        DEFDOWN,
        ATUP,
        ATDOWN,
        MAGUP,
        MAGDOWN,
        MPRESTORE
    };
}
