using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    public UnitStats stats;

    private BattleStateMachine BSM;

    public enum TurnState
    {
        WAITING,
        SELECTING,
        ACTION,
        DEAD
    }

    public TurnState actualState;

    // Start is called before the first frame update
    void Start()
    {
        BSM = FindObjectOfType<BattleStateMachine>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (actualState)
        {
            case TurnState.WAITING:
                break;
            case TurnState.SELECTING:
                break;
            case TurnState.ACTION:
                break;
            case TurnState.DEAD:
                break;
        }
    }
}
