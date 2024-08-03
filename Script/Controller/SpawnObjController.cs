using System.Collections;
using System.Collections.Generic;
using PortKey.Assets.Script;
using PortKey.Assets.Script.SwitchLevel;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnObjInterval
{
    public readonly float[] spawnObstacleTime = { 1, 2 };

    public readonly float[] enemyControlReverseTime = { 5, 8 };

    public readonly float[] scoreUpTime = { 5, 8 };

    public readonly float[] heartTime = { 5, 8 };

    public readonly float[] reduceEnemyHealthSpawnTimeRange = { 5, 8 };

    public readonly float[] slowEnemyTime = { 5, 8 };

    public readonly float[] bulletTime = { 5, 8 };

    public void SetLevelParameters(int level)
    {
        switch (level)
        {
            case 1:
                spawnObstacleTime[0] = 1;
                spawnObstacleTime[1] = 2;
                enemyControlReverseTime[0] = 5;
                enemyControlReverseTime[1] = 8;
                scoreUpTime[0] = 5;
                scoreUpTime[1] = 8;
                reduceEnemyHealthSpawnTimeRange[0] = 5;
                reduceEnemyHealthSpawnTimeRange[1] = 8;
                slowEnemyTime[0] = 5;
                slowEnemyTime[1] = 8;
                heartTime[0] = 4;
                heartTime[1] = 7;
                bulletTime[0] = 5;
                bulletTime[1] = 8;
                break;
            case 2:
                spawnObstacleTime[0] = 0.8f;
                spawnObstacleTime[1] = 1.8f;
                enemyControlReverseTime[0] = 4.8f;
                enemyControlReverseTime[1] = 7.8f;
                scoreUpTime[0] = 4.8f;
                scoreUpTime[1] = 7.8f;
                reduceEnemyHealthSpawnTimeRange[0] = 4.8f;
                reduceEnemyHealthSpawnTimeRange[1] = 7.8f;
                slowEnemyTime[0] = 4.8f;
                slowEnemyTime[1] = 7.8f;
                heartTime[0] = 3.8f;
                heartTime[1] = 6.8f;
                bulletTime[0] = 4.8f;
                bulletTime[1] = 7.8f;
                break;
            case 3: //BUFFER LEVEL
                spawnObstacleTime[0] = 0.6f;
                spawnObstacleTime[1] = 1.6f;
                enemyControlReverseTime[0] = 8f;
                enemyControlReverseTime[1] = 12f;
                scoreUpTime[0] = 8f;
                scoreUpTime[1] = 12f;
                reduceEnemyHealthSpawnTimeRange[0] = 4f;
                reduceEnemyHealthSpawnTimeRange[1] = 10f;
                slowEnemyTime[0] = 4.6f;
                slowEnemyTime[1] = 7.6f;
                heartTime[0] = 4.5f;
                heartTime[1] = 9f;
                bulletTime[0] = 4.6f;
                bulletTime[1] = 7.6f;
                break;
            case 4:
                spawnObstacleTime[0] = 0.6f;
                spawnObstacleTime[1] = 1.6f;
                enemyControlReverseTime[0] = 8f;
                enemyControlReverseTime[1] = 12f;
                scoreUpTime[0] = 8f;
                scoreUpTime[1] = 12f;
                reduceEnemyHealthSpawnTimeRange[0] = 4f;
                reduceEnemyHealthSpawnTimeRange[1] = 10f;
                slowEnemyTime[0] = 4.6f;
                slowEnemyTime[1] = 7.6f;
                heartTime[0] = 4.5f;
                heartTime[1] = 9f;
                bulletTime[0] = 4.6f;
                bulletTime[1] = 7.6f;
                break;
            case 5: //BUFFER LEVEL
                spawnObstacleTime[0] = 0.5f;
                spawnObstacleTime[1] = 1.5f;
                enemyControlReverseTime[0] = 8;
                enemyControlReverseTime[1] = 13;
                scoreUpTime[0] = 8;
                scoreUpTime[1] = 13;
                reduceEnemyHealthSpawnTimeRange[0] = 5;
                reduceEnemyHealthSpawnTimeRange[1] = 11;
                slowEnemyTime[0] = 6;
                slowEnemyTime[1] = 10;
                heartTime[0] = 4;
                heartTime[1] = 12;
                bulletTime[0] = 3;
                bulletTime[1] = 6;
                break;
            case 6:
                spawnObstacleTime[0] = 0.5f;
                spawnObstacleTime[1] = 1.5f;
                enemyControlReverseTime[0] = 8;
                enemyControlReverseTime[1] = 13;
                scoreUpTime[0] = 8;
                scoreUpTime[1] = 13;
                reduceEnemyHealthSpawnTimeRange[0] = 5;
                reduceEnemyHealthSpawnTimeRange[1] = 11;
                slowEnemyTime[0] = 6;
                slowEnemyTime[1] = 10;
                heartTime[0] = 4;
                heartTime[1] = 12;
                bulletTime[0] = 3;
                bulletTime[1] = 6;
                break;
            case 7:
                spawnObstacleTime[0] = 0.5f;
                spawnObstacleTime[1] = 1.5f;
                enemyControlReverseTime[0] = 8;
                enemyControlReverseTime[1] = 14;
                scoreUpTime[0] = 8;
                scoreUpTime[1] = 14;
                reduceEnemyHealthSpawnTimeRange[0] = 5;
                reduceEnemyHealthSpawnTimeRange[1] = 12;
                slowEnemyTime[0] = 7;
                slowEnemyTime[1] = 12;
                heartTime[0] = 5;
                heartTime[1] = 13;
                bulletTime[0] = 6;
                bulletTime[1] = 10;
                break;
            case 8:
                spawnObstacleTime[0] = 0.5f;
                spawnObstacleTime[1] = 1.5f;
                enemyControlReverseTime[0] = 8;
                enemyControlReverseTime[1] = 14;
                scoreUpTime[0] = 8;
                scoreUpTime[1] = 14;
                reduceEnemyHealthSpawnTimeRange[0] = 5;
                reduceEnemyHealthSpawnTimeRange[1] = 12;
                slowEnemyTime[0] = 7;
                slowEnemyTime[1] = 12;
                heartTime[0] = 5;
                heartTime[1] = 13;
                bulletTime[0] = 6;
                bulletTime[1] = 10;
                break;
            default:
                Debug.Log("Invalid level");
                spawnObstacleTime[0] = 1;
                spawnObstacleTime[1] = 2;
                enemyControlReverseTime[0] = 5;
                enemyControlReverseTime[1] = 8;
                scoreUpTime[0] = 5;
                scoreUpTime[1] = 8;
                reduceEnemyHealthSpawnTimeRange[0] = 5;
                reduceEnemyHealthSpawnTimeRange[1] = 8;
                slowEnemyTime[0] = 5;
                slowEnemyTime[1] = 8;
                heartTime[0] = 5;
                heartTime[1] = 8;
                bulletTime[0] = 5;
                bulletTime[1] = 8;
                break;
        }
    }
}

public class SpawnObjController : MonoBehaviour
{
    private SpawnObjInterval interval = new SpawnObjInterval();

    public GameObject obstacleLeft;

    public GameObject obstacleRight;

    public GameObject obstacleMiddle;

    public GameObject obstacleGapLeft;

    public GameObject obstacleGapRight;

    public GameObject enemyControlReverse;

    public GameObject scoreUp;

    public GameObject slowEnemy;

    public GameObject reduceEnemyHealth;

    public GameObject heart;

    public GameObject bullet;

    public bool isStopSpawn = false;

    public float leftOffset = -200f;

    public float rightOffset = 200f;

    public SpeedLimits speedLimits;

    private bool firstSpawn = true;

    GameObject lastPos;

    private int level;

    private float showUpYPos = 7.0f;

    // Start is called before the first frame update
    void Start()
    {
        // Set level parameters
        level = LevelInfo.Instance.Level;
        interval.SetLevelParameters(level);
        SetPropInfo(level);

        // for level 1, only spawn immovable obstacles
        if (obstacleMiddle == null)
        {
            StartCoroutine(SpawnImmovableObstacle());
        }
        else // for further levels, spawn movable obstacles
        {
            StartCoroutine(SpawnMovableObstacle());
        }


        if (enemyControlReverse != null)
        {
            StartCoroutine(SpawnEnemyControlReverse());
        }

        if (scoreUp != null)
        {
            StartCoroutine(SpawnScoreUp());
        }

        if (reduceEnemyHealth != null)
        {
            StartCoroutine(SpawnReduceEnemyHealth());
        }


        if (slowEnemy != null)
        {
            StartCoroutine(SpawnSlowEnemy());
        }

        if (heart != null)
        {
            StartCoroutine(SpawnHeart());
        }

        if (bullet != null)
        {
            StartCoroutine(SpawnBullet());
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void SetPropInfo(int level)
    {
        if (level != 8)
        {
            return;
        }

        Debug.Log("Level 8");

        if (!Level8Info.GetScoreUp())
        {
            scoreUp = null;
            Debug.Log("Score Up is false");
        }

        if (!Level8Info.GetCtrlFlip())
        {
            enemyControlReverse = null;
            Debug.Log("Ctrl Flip is false");
        }

        if (!Level8Info.GetLives())
        {
            heart = null;
            Debug.Log("Lives is false");
        }

        if (!Level8Info.GetAntiHealth())
        {
            reduceEnemyHealth = null;
            Debug.Log("Anti Health is false");
        }

        if (!Level8Info.GetTurtle())
        {
            slowEnemy = null;
            Debug.Log("Turtle is false");
        }

        if (!Level8Info.GetShooting())
        {
            bullet = null;
            Debug.Log("Shooting is false");
        }
    }

    IEnumerator SpawnImmovableObstacle()
    {
        while (true)
        {
            // Wait for a while before spawning the next obstacle
            yield return new WaitUntil(() => !isStopSpawn);

            // Randomly spawn objects
            yield return new WaitForSeconds(Random.Range(interval.spawnObstacleTime[0], interval.spawnObstacleTime[1]));

            if (!isStopSpawn)
            {
                int leftOrRight = Random.Range(0, 3);
                GameObject obstacle = null;
                GameObject obstacle2 = null;

                switch (leftOrRight)
                {
                    case 0:
                        obstacle = obstacleLeft;
                        break;
                    case 1:
                        obstacle = obstacleRight;
                        break;
                    case 2:
                        obstacle = obstacleGapLeft;
                        obstacle2 = obstacleGapRight;
                        break;
                }

                GameObject cloneObstacle = Instantiate(obstacle, transform);
                GameObject cloneObstacle2 = (obstacle2 != null) ? Instantiate(obstacle2, transform) : null;

                if (firstSpawn)
                {
                    lastPos = (cloneObstacle2 != null) ? cloneObstacle2 : cloneObstacle; // Use the second obstacle if available
                    firstSpawn = false;
                }
                else
                {
                    if (lastPos != null && (cloneObstacle2 != null || cloneObstacle != null))
                    {
                        GameObject relevantObstacle = (cloneObstacle2 != null) ? cloneObstacle2 : cloneObstacle;
                        if (Mathf.Abs(relevantObstacle.transform.position.y - lastPos.transform.position.y) <= 4.0f)
                        {
                            Debug.Log("DO NOT PLACE");
                            Destroy(relevantObstacle);
                            if (cloneObstacle2 != null) Destroy(cloneObstacle2);  // Make sure to clean up both if there are two
                        }
                        else
                        {
                            Debug.Log("PLACE");
                            lastPos = relevantObstacle; // Update lastPos to the last successfully placed obstacle
                        }
                    }
                }
            }
        }
    }

    IEnumerator SpawnMovableObstacle()
    {
        while (true)
        {
            yield return new WaitUntil(() => !isStopSpawn);

            // Randomly spawn objects
            yield return new WaitForSeconds(Random.Range(interval.spawnObstacleTime[0], interval.spawnObstacleTime[1]));

            if (!isStopSpawn)
            {
                int leftOrRight = Random.Range(0, 4);
                GameObject obstacle = null;
                GameObject obstacle2 = null;

                switch (leftOrRight)
                {
                    case 0:
                        obstacle = obstacleLeft;
                        break;
                    case 1:
                        obstacle = obstacleRight;
                        break;
                    case 2:
                        obstacle = obstacleMiddle;
                        break;
                    case 3:
                        obstacle = obstacleGapLeft;
                        obstacle2 = obstacleGapRight;
                        break;
                }

                GameObject cloneObstacle = Instantiate(obstacle, transform);
                GameObject cloneObstacle2 = (obstacle2 != null) ? Instantiate(obstacle2, transform) : null;

                if (firstSpawn)
                {
                    lastPos = cloneObstacle2 ?? cloneObstacle;
                    firstSpawn = false;
                }
                else
                {
                    if (lastPos != null && (cloneObstacle2 != null || cloneObstacle != null))
                    {
                        GameObject relevantObstacle = cloneObstacle2 ?? cloneObstacle;
                        if (Mathf.Abs(relevantObstacle.transform.position.y - lastPos.transform.position.y) <= 4.0f)
                        {
                            //Debug.Log("DO NOT PLACE");
                            Destroy(relevantObstacle);
                            if (cloneObstacle2 != null) Destroy(cloneObstacle2);
                        }
                        else
                        {
                            //Debug.Log("PLACE");
                            lastPos = relevantObstacle;
                        }
                    }
                }
            }
        }
    }

    IEnumerator SpawnEnemyControlReverse()
    {
        while (true)
        {
            yield return new WaitUntil(() => !isStopSpawn);
            // Randomly spawn objects

            yield return new WaitForSeconds(Random.Range(interval.enemyControlReverseTime[0], interval.enemyControlReverseTime[1]));

            if (!isStopSpawn)
            {
                GameObject cloneEnemyControlReverse = Instantiate(enemyControlReverse, transform);
                cloneEnemyControlReverse.transform.position = new Vector2(Random.Range(leftOffset, rightOffset), showUpYPos);
            }
        }

    }

    IEnumerator SpawnScoreUp()
    {

        while (true)
        {
            yield return new WaitUntil(() => !isStopSpawn);
            // Randomly spawn objects

            yield return new WaitForSeconds(Random.Range(interval.scoreUpTime[0], interval.scoreUpTime[1]));

            if (!isStopSpawn)
            {
                GameObject cloneScoreUp = Instantiate(scoreUp, transform);
                cloneScoreUp.transform.position = new Vector2(Random.Range(leftOffset, rightOffset), showUpYPos);
            }
        }

    }

    IEnumerator SpawnHeart()
    {

        while (true)
        {
            yield return new WaitUntil(() => !isStopSpawn);
            // Randomly spawn objects

            yield return new WaitForSeconds(Random.Range(interval.heartTime[0], interval.heartTime[1]));

            if (!isStopSpawn)
            {
                GameObject cloneHeart = Instantiate(heart, transform);
                cloneHeart.transform.position = new Vector2(Random.Range(leftOffset, rightOffset), showUpYPos);
            }
        }

    }

    IEnumerator SpawnBullet()
    {

        while (true)
        {
            yield return new WaitUntil(() => !isStopSpawn);
            // Randomly spawn objects

            yield return new WaitForSeconds(Random.Range(interval.bulletTime[0], interval.bulletTime[1]));

            if (!isStopSpawn)
            {
                GameObject cloneBullet = Instantiate(bullet, transform);
                cloneBullet.transform.position = new Vector2(Random.Range(leftOffset, rightOffset), showUpYPos);
            }
        }

    }



    IEnumerator SpawnSlowEnemy()
    {
        while (true)
        {
            Debug.Log("Spawn Slow Enemy");
            yield return new WaitUntil(() => !isStopSpawn);
            // Randomly spawn objects

            yield return new WaitForSeconds(Random.Range(interval.slowEnemyTime[0], interval.slowEnemyTime[1]));

            if (!isStopSpawn)
            {
                GameObject cloneSlowEnemy = Instantiate(slowEnemy, transform);
                cloneSlowEnemy.transform.position = new Vector2(Random.Range(leftOffset, rightOffset), showUpYPos);
            }
        }
    }

    IEnumerator SpawnReduceEnemyHealth()
    {
        while (true)
        {
            yield return new WaitUntil(() => !isStopSpawn);
            // Randomly spawn objects

            yield return new WaitForSeconds(Random.Range(interval.reduceEnemyHealthSpawnTimeRange[0], interval.reduceEnemyHealthSpawnTimeRange[1]));
            if (!isStopSpawn)
            {
                GameObject cloneReduceEnemyScore = Instantiate(reduceEnemyHealth, transform);
                cloneReduceEnemyScore.transform.position = new Vector2(Random.Range(leftOffset, rightOffset), showUpYPos);
            }
        }

    }

}
