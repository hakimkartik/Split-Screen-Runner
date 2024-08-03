using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateBulletShooter : MonoBehaviour
{
    // Helper variables for determining whether it's left player or right player
    public GameObject playerLeft;
    public GameObject playerRight;

    public float rotationSpeed = 27f;
    public GameObject bulletPrefab;
    public float bulletSpeed = 30;
    public Transform bulletSpawnPoint;
    string defaultRightPlayerName = "CarRight";
    string defaultLeftPlayerName = "CarLeft";

    float zRotationRange = 80f;
    float rotation;
    Transform parent;
    int reversed = 1;

    public int currentBulletsLeft = 7;
    public int currentBulletsRight = 7;

    // Dotted line stuff
    public GameObject dotPrefab; 
    private float dotSpacing = 0.7f; 
    private int numberOfDots = 10;
    private float maxLength = 10.0f; 
    private bool isObjectDetected = false;
    private List<GameObject> dots = new List<GameObject>();

    public AudioClip bulletCollisionClip;

    private AudioSource playerAudio;


    void Start()
    {
        rotation = transform.eulerAngles.z;
        parent = transform.parent;

        InitializeDots();
        GenerateDottedLine();
        playerAudio = GetComponent<AudioSource>();
    }

    void Update()
    {
        // Rotate pointer and generate dots in a synchronized manner
        RotatePointer();
        GenerateDottedLine();

        // Shoot bullet when space key or arrow key is pressed
        RotatePointerAndShootBullets();
    }

    void RotatePointerAndShootBullets()
    {
        LeftCarShooting();
        RightCarShooting();
    }

    void PlayBulletCollisionAudio()
    {
        if (playerAudio != null && bulletCollisionClip != null)
        {
            playerAudio.PlayOneShot(bulletCollisionClip);
        }

        if (playerAudio == null)
        {
            Debug.Log("playerAudio is null, obj: " + gameObject.name);
        }

        if (bulletCollisionClip == null)
        {
            Debug.Log("bulletCollisionClip is null, obj: " + gameObject.name);
        }


    }

    void RotatePointer()
    {
        rotation += rotationSpeed * Time.deltaTime * reversed;
        CheckRotationRange();
        transform.rotation = Quaternion.Euler(0, 0, rotation);
    }

    void CheckRotationRange()
    {
        if (rotation > zRotationRange)
        {
            rotation = zRotationRange;
            reversed = -1;
        }

        if (rotation < -zRotationRange)
        {
            rotation = -zRotationRange;
            reversed = 1;
        }
    }

    void LeftCarShooting()
    {
        if (parent.name == defaultLeftPlayerName)
        {
            KeyCode shootKey = KeyCode.W;

            if (Input.GetKeyDown(shootKey))
            {
                Shoot("left");
            }
        }
    }

    void RightCarShooting()
    {
        if (parent.name == defaultRightPlayerName)
        {
            KeyCode shootKey = KeyCode.UpArrow;

            if (Input.GetKeyDown(shootKey))
            {
                Shoot("right");
            }
        }
    }

    void Shoot(string isLeftOrRight)
    {
        int currentBullet = (isLeftOrRight == "left") ? currentBulletsLeft : currentBulletsRight;

        if (currentBullet > 0)
        {
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.velocity = bulletSpawnPoint.up * bulletSpeed;

            if (isLeftOrRight == "left")
            {
                currentBulletsLeft -= 1;
            }
            else
            {
                currentBulletsRight -= 1;
            }
            PlayBulletCollisionAudio();
        }
    }

    public void IncreaseBulletCountLeft()
    {
        currentBulletsLeft += 1;
    }

    public void IncreaseBulletCountRight()
    {
        currentBulletsRight += 1;
    }

    public void InitializeBullets()
    {
        currentBulletsLeft = 7;
        currentBulletsRight = 7;
    }

    // Dotted line stuff
    void InitializeDots()
    {
        for (int i = 0; i < numberOfDots; i++)
        {
            GameObject dot = Instantiate(dotPrefab);
            dot.SetActive(false);
            dots.Add(dot);
        }
    }

    void GenerateDottedLine()
    {
        if (dotPrefab == null)
        {
            Debug.LogError("Dot Prefab is not assigned.");
            return;
        }

        float rotation = transform.eulerAngles.z;

        // Calculate the maximum number of dots based on maxLength and dotSpacing
        int maxDots = Mathf.FloorToInt(maxLength / dotSpacing);

        int dotsToGenerate = Mathf.Min(numberOfDots, maxDots);

        Vector2 position;
        int dotIndex = 0;

        for (int i = 0; i < dotsToGenerate; i++)
        {
            position = (Vector2)transform.position + (Vector2)(Quaternion.Euler(0, 0, rotation) * Vector2.up * dotSpacing * i);

            if (!IsWithinBounds(position))
            {
                break;
            }

            if (IsObjectDetected(position))
            {
                isObjectDetected = true;
                break;
            }

            if (dotIndex < dots.Count)
            {
                GameObject dot = dots[dotIndex];
                dot.transform.position = position;
                dot.transform.rotation = dotPrefab.transform.rotation;
                dot.SetActive(true);
                dotIndex++;
            }
        }

        for (int i = dotIndex; i < dots.Count; i++)
        {
            dots[i].SetActive(false);
        }
    }

    bool IsObjectDetected(Vector2 position)
    {
        Collider2D hitCollider = Physics2D.OverlapCircle(position, dotSpacing * 0.5f);
        if (hitCollider != null)
        {
            return hitCollider.CompareTag("Obstacle") ||
                   hitCollider.gameObject.name.Contains("EnemyControlReverse") ||
                   hitCollider.gameObject.name.Contains("ScoreUp") ||
                   hitCollider.CompareTag("HeartProp") ||
                   hitCollider.gameObject.name.Contains("SlowEnemy") ||
                   hitCollider.gameObject.name.Contains("ReduceEnemyHealth");
        }
        return false;
    }

    bool IsWithinBounds(Vector2 position)
    {
        return position.x > -9 && position.x < 9 && position.y > -4 && position.y < 4;
    }
}
