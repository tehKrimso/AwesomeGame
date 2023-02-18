using System;
using Unity.Mathematics;
using UnityEngine;

namespace Mechanics
{
    public class CheckForCircuitOnDeath : MonoBehaviour
    {
        public int BufferSize = 5;
        public LayerMask CollisionMask;
        
        private Collider[] _deathColliderBuffer;

        private void Awake()
        {
            _deathColliderBuffer = new Collider[BufferSize];
        }

        public void CheckCircuits()
        {
            Physics.OverlapBoxNonAlloc(gameObject.transform.position, new Vector3(5,5,5), _deathColliderBuffer,
                quaternion.identity, CollisionMask);
            foreach (Collider circuitCollider in _deathColliderBuffer)
            {
                circuitCollider?.GetComponent<ElectricCircuit>()?.TurnCircuitOn();
            }
        }
    }
}
