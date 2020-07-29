using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatTokenFactory : MonoBehaviour
{
    public static CombatTokenFactory Instance { get { return _instance; } }
    private static CombatTokenFactory _instance;

    public int ammount;
    public CombatToken combatToken;
    private Pool<CombatToken> _combatTokenPool;


    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            _combatTokenPool = new Pool<CombatToken>(FactoryMethod, CombatToken.InitCallback, CombatToken.DisposeCallback, ammount);
        }
        else
        {
            Destroy(this);
        }
    }

    // El factory method. La funcíon que se encarga de crear las tokens.
    private CombatToken FactoryMethod()
    {
        return Instantiate(combatToken);
    }

    // La funcion que se encarga de traer tokens de la pool
    public void GetToken()
    {
        _combatTokenPool.GetObjectFromPool();
    }

    // La función que se encarga de devolver la token a la pool.
    public void ReturnToPool(CombatToken bullet)
    {
        _combatTokenPool.DisablePoolObject(bullet);
    }
}
