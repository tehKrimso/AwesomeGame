using UnityEngine;

namespace Infrastructure
{
    public class Game : MonoBehaviour
    {
        private SceneLoader _sceneLoader;
        private GameFactory _gameFactory;


        private void Awake()
        {
            _sceneLoader = new SceneLoader();
            _gameFactory = new GameFactory();
            
            DontDestroyOnLoad(this);
        }

        private void Initialize()
        {
            //настройка компонентов
            
            //
        }

        public void StartGame() => _sceneLoader.LoadScene("Level_1");

        public void LoadLevel(string levelName) => _sceneLoader.LoadScene(levelName);
    }
}