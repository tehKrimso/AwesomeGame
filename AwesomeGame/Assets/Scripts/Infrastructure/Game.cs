using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Infrastructure
{
    public class Game : MonoBehaviour
    {
        public UnityEvent LevelStart = new UnityEvent();
        
        
        private SceneLoader _sceneLoader;
        private AssetLoader _assets;
        private GameFactory _gameFactory;


        private GameObject _player;
        private List<Vector3> _deathObjectsPosition;

        private void Awake()
        {
            _deathObjectsPosition = new List<Vector3>();
            
            _sceneLoader = new SceneLoader(this);
            _assets = new AssetLoader();
            _gameFactory = new GameFactory(_assets, _sceneLoader, _deathObjectsPosition);
            
            //DontDestroyOnLoad(this);
        }

        private void Start()
        {
            OnGameStart();
        }

        private void Initialize()
        {
            //настройка компонентов
            
            //
        }

        public void StartGame()
        {
            _sceneLoader.Load("Level_1",OnGameStart);

        }

        public void LoadLevel(string levelName)
        {
            _sceneLoader.Load(levelName,_gameFactory.ConfigureDoors);
            
        }

        private void OnGameStart()
        {
            _player = _gameFactory.InstantiatePlayer();

            _player.GetComponent<PlayerControls>().PlayerIsDead.AddListener(OnPlayerDeath);
            
            LevelStart?.Invoke();
        }

        private void OnPlayerDeath(Vector3 deathPosition)
        {
            _deathObjectsPosition.Add(deathPosition);

            _player.GetComponent<PlayerControls>().PlayerIsDead.RemoveListener(OnPlayerDeath);
            
            Destroy(_player);
            
            LevelRestart();
        }

        private void LevelRestart()
        {
           _player = _gameFactory.InstantiatePlayer();

            _player.GetComponent<PlayerControls>().PlayerIsDead.AddListener(OnPlayerDeath);
            
            _gameFactory.InstantiateDeathObjects();
            
            LevelStart?.Invoke();
        }

        //public Coroutine StartCoroutine(IEnumerator coroutine) => StartCoroutine(coroutine);
    }
}