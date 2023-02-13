using UnityEngine.SceneManagement;

namespace Infrastructure
{
    public class SceneLoader
    {
        public void LoadScene(string nextScene)
        {
            SceneManager.LoadScene(nextScene);
        }
    }
}