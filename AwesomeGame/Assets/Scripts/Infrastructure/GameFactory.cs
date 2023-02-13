using Mechanics;
using UnityEngine;

namespace Infrastructure
{
    public class GameFactory
    {
        private readonly Game _game;
        private AssetLoader _assets;
        private readonly SceneLoader _sceneLoader;

        public GameFactory(Game game,AssetLoader assets, SceneLoader sceneLoader)
        {
            _game = game;
            _assets = assets;
            _sceneLoader = sceneLoader;
        }

        public void ConfigureDoors()
        {
            GameObject[] doors = GameObject.FindGameObjectsWithTag("Door");

            foreach (GameObject door in doors)
            {
                door.GetComponent<Door>().Configure(_game);
            }
        }
    }
}