using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public int sceneIndexToLoad;

    public void PlayGame()
    {
        SceneManager.LoadScene(sceneIndexToLoad);
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }
}
