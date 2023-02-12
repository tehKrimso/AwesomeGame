using Infractructure;
using UnityEngine;

namespace Infrastructure
{
    public class Bootstrapper : MonoBehaviour
    {
        private Game _game;
        private GameFactory _factory;
        private SceneLoader _sceneLoader;

        private void Awake()
        {
            _game = new Game();
        }
    }
}
