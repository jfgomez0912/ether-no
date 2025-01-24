using System;
using System.Linq;
using UnityEngine;

public class BubbleText : MonoBehaviour
{
    public GameObject dialogueBox;
    private bool isActive = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isActive && collision.gameObject.CompareTag("Player"))
        {
            isActive = true;
            dialogueBox.SetActive(true);
            dialogueBox.GetComponent<Dialogue>().StartDialogue();
        }
    }
}
