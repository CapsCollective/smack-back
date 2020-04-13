using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("PlayingField");
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }
}
