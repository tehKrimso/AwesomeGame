using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Infrastructure
{
    public class SceneLoader
    {
        private readonly Game _game;

        public SceneLoader(Game game)
        {
            _game = game;
        }
        
        public void Load(string name, Action onLoaded = null) =>
            _game.StartCoroutine(LoadScene(name, onLoaded));
        
        private IEnumerator LoadScene(string nextScene, Action onLoaded = null)
        {
            if (SceneManager.GetActiveScene().name == nextScene)
            {
                onLoaded?.Invoke();
                yield break;
            }
            
            AsyncOperation waitNextScene = SceneManager.LoadSceneAsync(nextScene);

            while (!waitNextScene.isDone)
                yield return null;
            
            onLoaded?.Invoke();
            //SceneManager.LoadScene(nextScene);

            //onLoaded.Invoke();
        }
    }
}