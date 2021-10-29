    using System.Collections.Generic;
    using UnityEngine;

    public abstract class State
    {
        protected readonly AIStateMachine _baseAI;

        public State(AIStateMachine baseAI)
        {
            _baseAI = baseAI;
        }

        public virtual void Enter(){}
        public virtual void Execute() { }
        public virtual void Exit(){}


    }

