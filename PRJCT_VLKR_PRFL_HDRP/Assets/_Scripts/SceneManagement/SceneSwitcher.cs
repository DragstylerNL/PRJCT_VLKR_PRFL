using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public void LoadScene(string sceneToBeLoaded)
    {
        print("Loading: " + sceneToBeLoaded);
        SceneManager.LoadScene(sceneToBeLoaded);
    }
}
