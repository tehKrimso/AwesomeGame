using Infrastructure;
using UnityEngine;

namespace Mechanics
{
    public class EndDoor : InteractableBase
    {
        public Game GameController;
        public ElectricCircuitWatcher Watcher;

        protected override void Interact()
        {
            Watcher.CheckIsWinnable();
            
            if (GameController.IsWinnable)
                EndGameInteraction();
            else
                NormalModeInteraction();
        }

        private void NormalModeInteraction()
        {
            //запитай цепи лох
            GameController.SetSelfDestructionMode();
            //дверь говорит
            Debug.Log("Go!");
            
            
        }

        private void EndGameInteraction()
        {
            //гг
            Debug.Log("You win GGS!");
        }
    }
}
