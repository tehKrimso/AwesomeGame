using System;
using Infrastructure;
using UnityEngine;

namespace Mechanics
{
    public class ElectricCircuitWatcher : MonoBehaviour
    {
        public ElectricCircuit Sublevel0Circuit;
        public ElectricCircuit Sublevel1Circuit;
        public ElectricCircuit Sublevel2Circuit;

        private bool _isZeroActive;
        private bool _isFirstActive;
        private bool _isSecondActive;

        private void Start()
        {
            Sublevel0Circuit.PowerOn.AddListener(() => _isZeroActive = true);
            Sublevel1Circuit.PowerOn.AddListener(() => _isFirstActive = true);
            Sublevel2Circuit.PowerOn.AddListener(() => _isSecondActive = true);
        }

        public void CheckIsWinnable()
        {
            if (_isZeroActive && _isFirstActive && _isSecondActive)
                GetComponent<Game>().IsWinnable = true;
        }
    }
}
