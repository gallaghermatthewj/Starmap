using UnityEngine;
using UnityEngine.SceneManagement;


public class SimpleSceneLoader : MonoBehaviour
{
    public string sceneToLoad;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return)) { loadScene(); }
    }
    void loadScene()
    {
        string activeScene = SceneManager.GetActiveScene().name;
        //SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Single);
        SceneManager.UnloadSceneAsync(activeScene, UnloadSceneOptions.UnloadAllEmbeddedSceneObjects);
        SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Single);
    }
}
