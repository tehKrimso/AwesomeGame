using System;
using UnityEngine;

namespace Infrastructure
{
    public class SubLevelSwitcher : MonoBehaviour
    {
        public PlayerControls Player;
        
        private GameObject _currentLevel;

        private GameObject _initialPoint;
        private Game _game;

        private void Awake()
        {
            _game = GetComponent<Game>();
            
            _currentLevel = GameObject.Find("SubLevel_0");
            _initialPoint = GameObject.FindGameObjectWithTag("InitialPoint");
            
            GetComponent<Game>().LevelStart.AddListener(OnLevelStart);
        }

        private void OnLevelStart()
        {
            Player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControls>();
        }

        public void SwitchTo(GameObject sublevelTo)
        {
            _currentLevel.SetActive(false);
            _currentLevel = sublevelTo;

            _game.CleanUpDeathObjects();
            Player.MoveTo(_initialPoint.transform.position);
            
            _currentLevel.SetActive(true);
        }
    }
}
