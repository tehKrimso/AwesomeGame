using System;
using Infrastructure;
using UnityEngine;

namespace Mechanics
{
    public class EndDoor : InteractableBase
    {
        public Game GameController;
        public ElectricCircuitWatcher Watcher;
        public GlareColorChanger GlareColorChanger;
        [SerializeField] GameObject newModeTip;
        [SerializeField] GameObject endGameTip;

        private void Start()
        {
            GameController.HardModeStarted.AddListener(GlareColorChanger.SetYellowColor);
            GameController.IsWinnableEvent.AddListener(GlareColorChanger.SetGreenColor);
        }

        private void Update()
        {
            Watcher.CheckIsWinnable();
        }

        protected override void Interact()
        {
            
            
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
