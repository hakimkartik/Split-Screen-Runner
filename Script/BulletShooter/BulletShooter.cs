using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShooter : MonoBehaviour
{
    void Update()
    {
        CheckBounds();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // If collides with Obstacle, EnemyControlReverse, or ScoreUp prefabs
        if (collision.CompareTag("Obstacle") || collision.gameObject.name.Contains("EnemyControlReverse") ||
            collision.gameObject.name.Contains("ScoreUp") || collision.CompareTag("HeartProp") || 
            collision.gameObject.name.Contains("SlowEnemy") || collision.gameObject.name.Contains("ReduceEnemyHealth"))
        {
            // Destroy the objects
            Destroy(collision.gameObject);
            // Destroy this bullet
            Destroy(gameObject);
        }
    }

    //destroys bullets once out of bounds
    void CheckBounds()
    {
        if (transform.position.x < -9 || transform.position.x > 9 || transform.position.y < -4 || transform.position.y > 4)
        {
            Destroy(gameObject);
        }
    }
}