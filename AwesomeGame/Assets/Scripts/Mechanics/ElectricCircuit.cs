using UnityEngine;
using UnityEngine.Events;

namespace Mechanics
{
    public class ElectricCircuit : MonoBehaviour
    {
        public UnityEvent PowerOn;

        public Material CircuitOn;
        public Material CircuitOff;

        public bool IsPowerOn;

        public void TurnCircuitOn()
        {
            IsPowerOn = true;

            GetComponent<MeshRenderer>().sharedMaterial = CircuitOn;
            
            PowerOn?.Invoke();
        }
    }
}
