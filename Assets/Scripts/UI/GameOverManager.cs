using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameOverManager : MonoBehaviour
{
    public static GameOverManager Instance { get; private set; }
    public Image gameOverImage;
    public FlowManager fM;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ShowGameOver()
    {
        gameOverImage.gameObject.SetActive(true);
        StartCoroutine(GoToStartScene());
    }

    private IEnumerator GoToStartScene()
    {
        yield return new WaitForSeconds(1f);
        fM.GoWithLoading("Menu");
        gameOverImage.gameObject.SetActive(false);
    }
}
