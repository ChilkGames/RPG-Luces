using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionBar : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateTokens(Image tokenImage = null, Vector3 position = default)
    {
        CombatToken token = new CombatToken(tokenImage, position);
    }
}
