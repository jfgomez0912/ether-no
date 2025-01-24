using UnityEngine;

public class EnemiesEmpty : MonoBehaviour
{
    private GameObject[] enemies;
    public GameObject light;
    public GameObject portal;
    void Update()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemigo");
        int cont = 0;
        foreach (GameObject enemy in enemies)
        {
            bool isAlive = enemy.GetComponent<EnemyController>().isAlive;
            if (!isAlive)
            {
                cont++;
            }
        }

        if (enemies.Length == cont)
        {
            light.SetActive(true);
            portal.SetActive(true);

        }
    }
}
