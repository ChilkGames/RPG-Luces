using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseItem : ScriptableObject
{
    public string itemName;
    public string itemDescription;
    public int price;
    public bool useOnEnemies;
    public bool areaEffect;
    public List<StatusEnum.Status> statuses = new List<StatusEnum.Status>();
    public List<int> percentages = new List<int>();
    public int damage;
}
