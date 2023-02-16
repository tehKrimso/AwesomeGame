using UnityEngine;

namespace Infrastructure
{
    public class AssetLoader
    {
        private const string PlayerPath = "Prefabs/Player";
        private const string DeathObjectPath = "Prefabs/Player";

        private GameObject _initialPoint;
        
        public AssetLoader()
        {
            _initialPoint = GameObject.FindGameObjectWithTag("InitialPoint");
        }

        public GameObject InstantiatePlayer()
        {
            GameObject playerPrefab = Resources.Load(PlayerPath) as GameObject;
            GameObject playerObject = GameObject.Instantiate(playerPrefab, _initialPoint.transform.position, Quaternion.identity);

            return playerObject;
        }
    }
}