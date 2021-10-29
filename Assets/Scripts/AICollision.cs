using System.Collections.Generic;
using UnityEngine;

public class AICollision : MonoBehaviour
    {
        private AIStateMachine _aiStateMachine;
        private void Awake()
        {
            _aiStateMachine = gameObject.GetComponent<AIStateMachine>();
        }
        private void OnTriggerEnter(Collider other)
        {
            if ((_aiStateMachine.attackMask & (1 << other.gameObject.layer)) == 0) return;
            if (_aiStateMachine.particleSystem.isPlaying.Equals(true)) return;
            
            int rand = Random.Range(0, 1);
            switch (rand)
            {
                case 0:
                    _aiStateMachine.particleSystem.Play();
                    break;
                case 1:
                    other.GetComponent<ParticleSystem>().Play();
                    break;
            }

            Destroy(other.gameObject, 1);
            Destroy(gameObject, 1);
        }
    }