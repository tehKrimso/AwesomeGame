using System;
using System.Collections;
using System.Collections.Generic;
using Mechanics;
using UnityEngine;
using UnityEngine.Events;

namespace Infrastructure
{
    public class Game : MonoBehaviour
    {
        public GameObject Player;
        
        public UnityEvent LevelStart = new UnityEvent();
        public float SpawnDelay;


        private SceneLoader _sceneLoader;
        private AssetLoader _assets;
        private GameFactory _gameFactory;
        
        private List<Vector3> _deathObjectsPosition;

        public bool IsWinnable;
        private bool _isHardMode;
        private bool _isSelfDestructionAllowed;

        private void Awake()
        {
            _deathObjectsPosition = new List<Vector3>();
            
            _sceneLoader = new SceneLoader(this);
            _assets = new AssetLoader();
            _gameFactory = new GameFactory(_assets, _sceneLoader, _deathObjectsPosition);
        }

        private void Start()
        {
            OnGameStart();
        }

        public void CleanUpDeathObjects() => _gameFactory.ClearDeathObjects();

        public bool IsHardModeOn() => _isHardMode;
        public void SetHardMode() => _isHardMode = true;
        public void SetSelfDestructionMode()
        {
            _isSelfDestructionAllowed = true;
            Player.GetComponent<SelfDestruction>().SetSelfDestruction(_isSelfDestructionAllowed);
        }

        public void StartHardMode()
        {
            SetHardMode();
            GetComponent<SubLevelSwitcher>().SwitchTo(GameObject.Find("SubLevel_0").GetComponent<SubLevelController>());
        }

        private void OnGameStart()
        {
            //Player = _gameFactory.InstantiatePlayer();

            Player.GetComponent<PlayerControls>().PlayerIsDead.AddListener(OnPlayerDeath);
            
            Player.GetComponent<SelfDestruction>().SetSelfDestruction(_isSelfDestructionAllowed);
            
            LevelStart?.Invoke();
        }

        private void OnPlayerDeath(Vector3 deathPosition)
        {
            _deathObjectsPosition.Add(deathPosition);

            Player.GetComponent<PlayerControls>().PlayerIsDead.RemoveListener(OnPlayerDeath);
            
            Destroy(Player);
            
            StartCoroutine( LevelRestart());
        }

        private IEnumerator LevelRestart()
        {
            _gameFactory.InstantiateDeathObjects();


            yield return new WaitForSeconds(SpawnDelay);
            
            Player = _gameFactory.InstantiatePlayer();

            Player.GetComponent<PlayerControls>().PlayerIsDead.AddListener(OnPlayerDeath);
            
            Player.GetComponent<SelfDestruction>().SetSelfDestruction(_isSelfDestructionAllowed);


           LevelStart?.Invoke();
        }

        //public Coroutine StartCoroutine(IEnumerator coroutine) => StartCoroutine(coroutine);
    }
}