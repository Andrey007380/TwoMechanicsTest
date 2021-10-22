using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    protected readonly BaseAI _baseAI;

    public State(BaseAI baseAI)
    {
        _baseAI = baseAI;
    }

    public virtual IEnumerator Attack() { yield break; }
    public virtual IEnumerator Chase() { yield break; }
    public virtual IEnumerator Idle() { yield break; }


}
