using UnityEngine;
using UnityEngine.SceneManagement;

public class FlowManager : MonoBehaviour
{
    public void GoWithLoading(string sceneName)
    {
        SceneLoader.GoToScene(sceneName);
    }
    
    public void GoToDirectly(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void LeftGame()
    {
        Application.Quit();
    }
}