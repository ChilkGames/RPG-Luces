using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
    public UnitStats stats;
    public int experienceToDrop;
    public int goldToDrop;
    public List<BaseItem> itemsToDrop;
    public List<int> percentageOfDrop;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
