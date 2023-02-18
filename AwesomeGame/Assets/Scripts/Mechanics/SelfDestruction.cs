using System;
using UnityEngine;

namespace Mechanics
{
    public class SelfDestruction : MonoBehaviour
    {
        private bool _isSelfDestructionAllowed;

        private PlayerControls _playerController;

        private void Awake()
        {
            _playerController = GetComponent<PlayerControls>();
        }

        public void SetSelfDestruction(bool state) => _isSelfDestructionAllowed = state;

        private void Update()
        {
            if (!_isSelfDestructionAllowed)
                return;

            if (Input.GetKey(KeyCode.F))
            {
                _playerController.OnPlayerDeath();
            }
        }
    }
}
