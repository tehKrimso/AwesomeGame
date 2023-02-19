using Infrastructure;
using UnityEngine;

namespace Mechanics
{
    public class EndDoor : InteractableBase
    {
        public Game GameController;
        public ElectricCircuitWatcher Watcher;
        [SerializeField] GameObject newModeTip;
        [SerializeField] GameObject endGameTip;

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
            newModeTip.SetActive(true);
            //дверь говорит
            Debug.Log("Go!");
            
            
        }

        private void EndGameInteraction()
        {
            //гг
            Debug.Log("You win GGS!");
            endGameTip.SetActive(true);
        }
    }
}
