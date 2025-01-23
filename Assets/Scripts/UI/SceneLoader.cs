using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneLoader
{
    public static string nextLevel;

    public static void GoToScene(string levelName)
    {
        Time.timeScale = 1f;
        nextLevel = levelName;
        SceneManager.LoadScene("Pantalla de carga");// Escena de pantalla de carga
    }
}
