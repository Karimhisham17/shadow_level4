using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eb3enemy : EnemyController
{
    public Transform enemy;
    public Transform spawnPoint;
    // Start is called before the first frame update
    void Start()
    {
        
    }
void RespawnEnemy()
{
    Instantiate(enemy, spawnPoint.transform.position, spawnPoint.transform.rotation);
}



            void OnTriggerEnter2D (Collider2D other)
    {
        if (other.tag == "Player")
        {
            RespawnEnemy();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
