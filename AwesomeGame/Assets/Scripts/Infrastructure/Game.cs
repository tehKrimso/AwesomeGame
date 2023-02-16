using System;
using System.Collections;
using UnityEngine;

namespace Infrastructure
{
    public class Game : MonoBehaviour
    {
        private SceneLoader _sceneLoader;
        private AssetLoader _assets;
        private GameFactory _gameFactory;


        private void Awake()
        {
            _sceneLoader = new SceneLoader(this);
            _assets = new AssetLoader();
            _gameFactory = new GameFactory(this,_assets, _sceneLoader);
            
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

        private void OnGameStart() => _gameFactory.InstantiatePlayer();

        private void OnLevelLoad()
        {
            _gameFactory.InstantiatePlayer();
            _gameFactory.InstantiateDeathObjects();
        }

        //public Coroutine StartCoroutine(IEnumerator coroutine) => StartCoroutine(coroutine);
    }
}