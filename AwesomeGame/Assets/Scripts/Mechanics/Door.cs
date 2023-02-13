using System;
using Infrastructure;
using UnityEngine;

namespace Mechanics
{
    public class Door : InteractableBase
    {
        public string NextLevelName;

        private Game _game;

        public void Configure(Game game)
        {
            _game = game;
        }
        
        protected override void Interact()
        {
            _game.LoadLevel(NextLevelName);
        }

        private void OnTriggerStay(Collider other)
        {
            if(Input.GetKey(KeyCode.E))
                Interact();
        }
    }
}