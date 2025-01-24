using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Video;

public class IntroVideoManager : MonoBehaviour
{
    private VideoPlayer vp;
    public string nextNameScene;
    public GameObject canvas;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        vp = GetComponent<VideoPlayer>();
        float duration = (float) vp.length;
        StartCoroutine(ClipFinished(duration - 10f));
    }

    IEnumerator ClipFinished(float duration)
    {
        yield return new WaitForSeconds(duration);
        canvas.GetComponent<FlowManager>().GoWithLoading(nextNameScene);
    }
}
