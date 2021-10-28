using System.Collections;
using System.Collections.Generic;
using FSM;
using UnityEngine;

public abstract class State
{
    protected readonly AIStateMachine _baseAI;

    public State(AIStateMachine baseAI)
    {
        _baseAI = baseAI;
    }

    public virtual IEnumerator Execute() { yield break; }


}
