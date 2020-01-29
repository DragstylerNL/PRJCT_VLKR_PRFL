using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public void LoadScene(string sceneToBeLoaded)
    {
        print("ok");
        SceneManager.LoadScene(sceneToBeLoaded);
    }


}
