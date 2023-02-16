using System.Collections.Generic;
using Mechanics;
using UnityEngine;

namespace Infrastructure
{
    public class GameFactory
    {
        private readonly AssetLoader _assets;
        private readonly SceneLoader _sceneLoader;
        private readonly List<Vector3> _deathObjectsPosition;

        public GameFactory(AssetLoader assets, SceneLoader sceneLoader, List<Vector3> deathObjectsPosition)
        {
            _assets = assets;
            _sceneLoader = sceneLoader;
            _deathObjectsPosition = deathObjectsPosition;
        }

        public void ConfigureDoors()
        {
            GameObject[] doors = GameObject.FindGameObjectsWithTag("Door");

            foreach (GameObject door in doors)
            {
                //door.GetComponent<Door>().Configure(_game);
            }
        }


        public GameObject InstantiatePlayer()
        {
            GameObject player =  _assets.InstantiatePlayer();

            Camera.main.transform.parent.GetComponent<FollowCamera>().Follow(player.transform);
            
            return player;
        }


        public void InstantiateDeathObjects()
        {
            foreach (Vector3 deathObjectPosition in _deathObjectsPosition)
            {
                _assets.InstantiateDeathObject(deathObjectPosition);
            }
        }
    }
}