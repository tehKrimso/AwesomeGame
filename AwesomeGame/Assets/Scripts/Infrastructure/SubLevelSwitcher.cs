using System;
using UnityEngine;

namespace Infrastructure
{
    public class SubLevelSwitcher : MonoBehaviour
    {
        public PlayerControls Player;
        
        private SubLevelController _currentLevel;

        private GameObject _initialPoint;
        private Game _game;

        private void Awake()
        {
            _game = GetComponent<Game>();
            
            _currentLevel = GameObject.Find("SubLevel_0").GetComponent<SubLevelController>();
            _initialPoint = GameObject.FindGameObjectWithTag("InitialPoint");
            
            GetComponent<Game>().LevelStart.AddListener(OnLevelStart);
        }

        private void OnLevelStart()
        {
            Player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControls>();
        }

        public void SwitchTo(SubLevelController sublevelTo)
        {
            _currentLevel.TurnLevelOff();
            _currentLevel = sublevelTo;

            _game.CleanUpDeathObjects();
            Player.MoveTo(_initialPoint.transform.position);

            if (_game.IsHardModeOn())
            {
                _currentLevel.HardModeOn();
            }
            else
            {
                _currentLevel.NormalModeOn();
            }
            
        }
    }
}
