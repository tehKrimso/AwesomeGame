using System;
using Unity.Mathematics;
using UnityEngine;

namespace Mechanics
{
    public class CheckForCircuitOnDeath : MonoBehaviour
    {
        public int BufferSize = 5;
        public LayerMask CollisionMask;
        public Vector3 PowerUpHalfSize;

        private Collider[] _deathColliderBuffer;

        private void Awake()
        {
            _deathColliderBuffer = new Collider[BufferSize];
        }

        public void CheckCircuits()
        {
            PowerUpHalfSize = new Vector3(3,3,3);
            Physics.OverlapBoxNonAlloc(gameObject.transform.position, PowerUpHalfSize, _deathColliderBuffer,
                quaternion.identity, CollisionMask);
            foreach (Collider circuitCollider in _deathColliderBuffer)
            {
                circuitCollider?.GetComponent<ElectricCircuit>()?.TurnCircuitOn();
            }
        }
    }
}
