using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
    public class ViewManager : MonoBehaviour
    {
        private void Start()
        {
            AddScene("Menu");
        }

        public void AddScene(string sceneName)
        {
            SceneManager.LoadScene("Menu", LoadSceneMode.Additive);
        }

        public void UnLoadScene(string sceneName)
        {
            SceneManager.UnloadSceneAsync(sceneName);
        }
    }
}
