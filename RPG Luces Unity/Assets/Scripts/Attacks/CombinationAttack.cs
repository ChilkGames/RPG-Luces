using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CombinationAttack : BaseAttack
{
    /// <summary>
    /// List of attacks needed to trigger it.
    /// </summary>
    public List<BaseAttack> combinationOfAttacks = new List<BaseAttack>();
}
