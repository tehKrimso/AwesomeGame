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
        private readonly List<GameObject> _deathObjects;

        public GameFactory(AssetLoader assets, SceneLoader sceneLoader, List<Vector3> deathObjectsPosition)
        {
            _assets = assets;
            _sceneLoader = sceneLoader;
            _deathObjectsPosition = deathObjectsPosition;
            _deathObjects = new List<GameObject>();
        }

        public void ConfigureDoors()
        {
            GameObject[] doors = GameObject.FindGameObjectsWithTag("Door");

            foreach (GameObject door in doors)
            {
                //door.GetComponent<Door>().Configure(_game);
            }
        }


        public void ClearDeathObjects()
        {
            _deathObjectsPosition.Clear();
            foreach (GameObject deathObject in _deathObjects)
            {
                GameObject.Destroy(deathObject);
            }
            
            _deathObjects.Clear();
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
                _deathObjects.Add(_assets.InstantiateDeathObject(deathObjectPosition));
            }
        }
    }
}