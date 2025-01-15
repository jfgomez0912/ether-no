using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class CloudController : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(DestroyCorrutune());
    }

    private IEnumerator DestroyCorrutune()
    {
        yield return new WaitForSeconds(1);
        Destroy(this.gameObject);
    }
    
}
