using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Serialization;

public class Loading : MonoBehaviour
{
    public GameObject pantallaDeCarga;
    public Slider slider;
    public TextMeshProUGUI tmp;

    // Start is called before the first frame update
    void Start()
    {
        string levelToLoad = SceneLoader.nextLevel;
        StartCoroutine(this.MakeTheLoad(levelToLoad));
    }

    IEnumerator MakeTheLoad(string level)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(level);

        pantallaDeCarga.SetActive(true);
        while (!operation.isDone)
        {
            float progreso = Mathf.Clamp01(operation.progress / .9f);
            slider.value = progreso; 
            tmp.text = (progreso * 100) + "%";
            yield return null;
        }
        //Parar coroutine??
    }
}
