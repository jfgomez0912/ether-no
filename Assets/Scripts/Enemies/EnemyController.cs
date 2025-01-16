using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public bool atk;
    private bool coldDown = false;
    private Animator anim;
    private Transform sprite;
    public float coldDownTime;

    private void Awake()
    {
        sprite = gameObject.transform.GetChild(0);
        anim = sprite.GetComponent<Animator>();
    }

    private void Update()
    {
        if (atk && !coldDown)
        {
            coldDown = true;
            anim.SetBool("atk", true);
            StartCoroutine(ColdDownStart());
        }
    }

    private IEnumerator ColdDownStart()
    {
        yield return new WaitForSeconds(0.8f);
        anim.SetBool("atk", false);
        yield return new WaitForSeconds(coldDownTime);
        coldDown = false;
    }

    private IEnumerator ColdDownReset()
    {
        yield return new WaitForSeconds(coldDownTime);
        coldDown = false;
    }
}
