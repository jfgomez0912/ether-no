using UnityEngine;

public class Habitacion1 : MonoBehaviour
{
    private GameObject[] enemies;
    public GameObject nextScene;

    // Update is called once per frame
    void Update()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemigo");
        int cont = 0;
        foreach (GameObject enemy in enemies)
        {
            bool isAlive = enemy.GetComponent<EnemyController>().isAlive;
            if (isAlive)
            {
                continue;
            }
            else
            {
                cont++;
            }
        }

        if (enemies.Length == cont)
        {
            print("Todos muertos");
            nextScene.SetActive(true);
        }
    }
}
