using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using PortKey.Assets.Script;
using PortKey.Assets.Script.SwitchLevel;

public class HealthParameter
{

    public float obstacleImpactOnHealth = -15f;

    public float minusPropImpactOnHealth = -5f;

    public void SetParametersByLevel(int level)
    {
        switch (level)
        {
            case 1:

                obstacleImpactOnHealth = -15f;
                minusPropImpactOnHealth = -5f;
                break;
            case 2:
                obstacleImpactOnHealth = -15f;
                minusPropImpactOnHealth = -5f;
                break;
            case 3:
                obstacleImpactOnHealth = -10f;
                minusPropImpactOnHealth = -10f;
                break;
            case 4:
                obstacleImpactOnHealth = -10f;
                minusPropImpactOnHealth = -10f;
                break;
            default:
                obstacleImpactOnHealth = -10f;
                minusPropImpactOnHealth = -10f;
                break;
        }
    }

}

public class CarMove : MonoBehaviour
{
    private HealthParameter hp = new HealthParameter();

    public float carSpeed = 300f;

    public bool actionAD = false;

    public float boundaryLeft = -850f;

    public float boundaryRight = -50f;

    public bool canMove = true;

    public TextMeshProUGUI deathText;

    public TextMeshProUGUI winText;

    public TextMeshProUGUI broadcastMsg;

    public Image navArea;

    public bool reversed = false;

    public GameObject reverseIcon;

    public GameObject background;

    Quaternion originalRotation;

    float originalYPosition;

    public HealthBar healthBar;

    public LivesManager liveManager;

    private int level;

    GameController gameController;

    SpeedController speedController;

    public float playerHealth = 100;

    public float maxHealth = 100;

    // store control switch prop collected
    public Coroutine currentRevertCoroutine = null;

    CarMove carOpp;

    public AudioClip obstacleCollisionClip;
    public AudioClip scoreUpCollisionClip;
    public AudioClip heartCollisionClip;
    public AudioClip controlFlipCollisionClip;
    public AudioClip antiHealthCollisionClip;
    public AudioClip slowEnemyCollisionClip;

    private AudioSource playerAudio;

    void Start()
    {
        level = LevelInfo.Instance.Level;
        if (level == -1)
        {
            Debug.LogError("Level not found");
        }
        hp.SetParametersByLevel(level);

        gameController = FindObjectOfType<GameController>();
        if (gameController == null)
        {
            Debug.LogError("GameController not found");
        }

        speedController = FindObjectOfType<SpeedController>();
        if (speedController == null)
        {
            Debug.LogError("SpeedController not found");
        }

        originalRotation = transform.localRotation;
        originalYPosition = transform.localPosition.y;

        UploadHealthBars();
        liveManager = GameObject.FindWithTag("Lives").GetComponent<LivesManager>();
        if (liveManager == null)
        {
            Debug.LogError("LiveManager not found");
        }
        carOpp = (transform.name == ConstName.LEFT_CAR) ? gameController.carRight.GetComponent<CarMove>() : gameController.carLeft.GetComponent<CarMove>();
        playerAudio = GetComponent<AudioSource>();
    }

    void UploadHealthBars()
    {
        if (transform.name == ConstName.LEFT_CAR)
        {
            healthBar = GameObject.FindWithTag("LeftHealthBar").GetComponent<HealthBar>();
            healthBar.UpdateLeftPlayerHealthBar(playerHealth, maxHealth);

        }
        else if (transform.name == ConstName.RIGHT_CAR)
        {
            healthBar = GameObject.FindWithTag("RightHealthBar").GetComponent<HealthBar>();
            healthBar.UpdateRightPlayerHealthBar(playerHealth, maxHealth);
        }
        else
        {
            Debug.LogError("HealthBar component not found.");
        }
    }

    void Update()
    {
        //ShootBullet();
        if (!canMove)
        {
            return;
        }

        float posX = transform.position.x;

        if (transform.name == ConstName.LEFT_CAR)
        {
            if (!reversed)
            {
                if (Input.GetKey(KeyCode.A) && posX > boundaryLeft)
                {
                    transform.Translate(Vector3.left * carSpeed * Time.deltaTime);
                }
                if (Input.GetKey(KeyCode.D) && posX < boundaryRight)
                {
                    transform.Translate(Vector3.right * carSpeed * Time.deltaTime);
                }
            }
            else
            {
                if (Input.GetKey(KeyCode.A) && posX < boundaryRight)
                {
                    transform.Translate(Vector3.left * carSpeed * Time.deltaTime);
                }
                if (Input.GetKey(KeyCode.D) && posX > boundaryLeft)
                {
                    transform.Translate(Vector3.right * carSpeed * Time.deltaTime);
                }
            }
        }

        if (transform.name == "CarRight")
        {
            if (!reversed)
            {
                if (Input.GetKey(KeyCode.LeftArrow) && posX > boundaryLeft)
                {
                    transform.Translate(Vector3.left * carSpeed * Time.deltaTime);
                }
                if (Input.GetKey(KeyCode.RightArrow) && posX < boundaryRight)
                {
                    transform.Translate(Vector3.right * carSpeed * Time.deltaTime);
                }
            }
            else
            {
                if (Input.GetKey(KeyCode.LeftArrow) && posX < boundaryRight)
                {
                    transform.Translate(Vector3.left * carSpeed * Time.deltaTime);
                }
                if (Input.GetKey(KeyCode.RightArrow) && posX > boundaryLeft)
                {
                    transform.Translate(Vector3.right * carSpeed * Time.deltaTime);
                }
            }
        }

        UpdateReverseIconVisibility();
        UpdateBackground();
    }

    void UpdateLives(string player, bool didLiveUp, bool isDueToMinusProp)
    {
        if (didLiveUp) //if collected the heart then increment the hearts
        {
            if (player == ConstName.LEFT_CAR)
            {
                liveManager.IncrementLivesLeft();
            }
            else
            {
                liveManager.IncrementLivesRight();
            }
        }
        else
        {
            if (player == ConstName.LEFT_CAR)
            {
                liveManager.DecrementLivesLeft();
                if (liveManager.GetLivesLeft() == 0)
                {
                    PlayerDead(isDueToMinusProp);
                }
            }
            else
            {
                liveManager.DecrementLivesRight();
                if (liveManager.GetLivesRight() == 0)
                {
                    PlayerDead(isDueToMinusProp);
                }
            }
        }

    }

    void ProcessReduceEnemyHealthProp(string player)
    {
        if (player == ConstName.RIGHT_CAR)
        {
            liveManager.DecrementLivesLeft();
            gameController.DisplayLeftLostHealthMsg();
            if (liveManager.GetLivesLeft() == 0)
            {
                PlayerDead(true);
            }
        }
        else
        {
            liveManager.DecrementLivesRight();
            gameController.DisplayRightLostHealthMsg();
            if (liveManager.GetLivesRight() == 0)
            {
                PlayerDead(true);
            }
        }
    }


    void PlayAudioOnCollision(Collider2D other)
    {

        if (playerAudio != null)
        {
            if (obstacleCollisionClip != null && other.CompareTag("Obstacle"))
            {
                playerAudio.PlayOneShot(obstacleCollisionClip);
            }
            else if (scoreUpCollisionClip != null && other.gameObject.name.Contains("ScoreUp"))
            {
                playerAudio.PlayOneShot(scoreUpCollisionClip);
            }
            else if (heartCollisionClip != null && other.CompareTag("HeartProp"))
            {
                playerAudio.PlayOneShot(heartCollisionClip);
            }

            else if (controlFlipCollisionClip != null && other.gameObject.name.Contains("EnemyControlReverse"))
            {
                playerAudio.PlayOneShot(controlFlipCollisionClip);
            }
            else if (antiHealthCollisionClip != null && other.gameObject.name.Contains("ReduceEnemyHealth"))
            {
                playerAudio.PlayOneShot(antiHealthCollisionClip);
            }
            else if (slowEnemyCollisionClip != null && other.gameObject.name.Contains("SlowEnemy"))
            {
                playerAudio.PlayOneShot(slowEnemyCollisionClip);
            }

        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        /************************* For Obstacle Collision *************************/
        if (other.gameObject.tag == "Obstacle")
        {
            ShakePlayerOnHealthLoss();

            //decrement healthBar accordingly
            gameController.UpdateHealthBarOnCollision(transform.name, hp.obstacleImpactOnHealth);

            //destroy the obstacle on collision
            Destroy(other.gameObject);

            // Collisions after Control Flip Metric #4
            UpdateDataForAnalytics();

            // Check if player is healthy or not
            //CheckIfPlayerIsHealthyOrNot();

            //update lives of the player
            UpdateLives(transform.name, false, false);

            //destroy the obstacle on collision
            Destroy(other.gameObject);
        }
        /************************* For Obstacle Collision *************************/

        /************************* For EnemyControlReverse Collision *************************/
        if (other.gameObject.name.Contains("EnemyControlReverse"))
        {
            if (!carOpp.reversed)
            {
                DisplaySwitchMessage();
            }
            Destroy(other.gameObject);
            gameController.EnemyControlReverse(transform.name);
            UpdateAnalyticsOnControlInversion();
        }
        /************************* For EnemyControlReverse Collision *************************/


        /************************* For ScoreUp Collision *************************/
        if (other.gameObject.name.Contains("ScoreUp"))
        {
            Destroy(other.gameObject);
            gameController.OneTimeBonus(transform.name);
            UpdateAnalyticsOnScoreUp();
        }
        /************************* For ScoreUp Collision *************************/


        /************************* For ReduceEnemyHealth Collision *************************/
        if (other.gameObject.name.Contains("ReduceEnemyHealth"))
        {
            Destroy(other.gameObject);
            ProcessReduceEnemyHealthProp(transform.name);
            gameController.ReduceHealthEffect(transform.name);
        }
        /************************* For ReduceEnemyHealth Collision *************************/


        /************************* For SlowEnemy Collision *************************/
        if (other.gameObject.name.Contains("SlowEnemy"))
        {
            Destroy(other.gameObject);
            if (speedController == null)
            {
                Debug.LogError("SpeedController not found");
            }
            else
            {
                speedController.SlowDownCarTemporarily(transform.name, 0.5f, 4f);
                gameController.ShowSpeedSlowMsg(transform.name);
            }
        }
        /************************* For SlowEnemy Collision *************************/

        /************************* For Heart Collision *************************/
        if (other.gameObject.tag == "HeartProp")
        {
            //update lives of the player
            Destroy(other.gameObject);
            UpdateLives(transform.name, true, false);
        }
        /************************* For Heart Collision *************************/

        /************************* For Bullet Collision *************************/
        if (other.gameObject.tag == "Bullet")
        {
            if (transform.name == ConstName.LEFT_CAR)
            {

                RotateBulletShooter shooter = GameObject.Find("PivotLeft").GetComponent<RotateBulletShooter>();
                if (shooter != null)
                {
                    shooter.IncreaseBulletCountLeft();
                }

            }
            else
            {
                RotateBulletShooter shooter = GameObject.Find("PivotRight").GetComponent<RotateBulletShooter>();
                if (shooter != null)
                {
                    shooter.IncreaseBulletCountRight();
                }
            }
            Destroy(other.gameObject); // Destroy the prop after collecting
        }
        /************************* For Bullet Collision *************************/

        PlayAudioOnCollision(other);
    }


    public void ShakePlayerOnHealthLoss()
    {
        StartCoroutine(ShakePlayer());
    }


    IEnumerator ShakePlayer()
    {
        //Debug.Log("Shaking "+gameObject.name);
        float time = 0.0f;
        //Quaternion originalRotation = transform.localRotation;
        while (time < 0.5f)
        {
            float shake = Random.Range(-1f, 1f) * 10.0f;
            transform.localRotation = Quaternion.Euler(0, 0, originalRotation.eulerAngles.z + shake);
            time += Time.deltaTime;
            if (Time.timeScale == 0)
            {
                Vector3 currentPosition = transform.localPosition;
                transform.localPosition = new Vector3(currentPosition.x, originalYPosition, currentPosition.z);
                transform.localRotation = originalRotation;
            }
            yield return null;
        }
        Vector3 finalPosition = transform.localPosition;
        transform.localPosition = new Vector3(finalPosition.x, originalYPosition, finalPosition.z);
        transform.localRotation = originalRotation;
    }


    void UpdateAnalyticsOnControlInversion()
    {
        // Metric #3
        if (transform.name == ConstName.LEFT_CAR)
        {
            gameController.totalCtrlSwitchPropCollectedLeft += 1;
        }
        else
        {
            gameController.totalCtrlSwitchPropCollectedRight += 1;
        }
    }

    void UpdateAnalyticsOnScoreUp()
    {

        // Metric #1
        if (transform.name == ConstName.LEFT_CAR)
        {
            gameController.totalScoreUpLeft += 1;
        }
        else
        {
            gameController.totalScoreUpRight += 1;
        }
    }

    void UpdateDataForAnalytics()
    {
        // Collisions after Control Flip Metric #4
        if (transform.name == ConstName.LEFT_CAR)
        {
            gameController.collisionDueToCtrlFlipLeft += 1;
        }
        else
        {
            gameController.collisionDueToCtrlFlipRight += 1;
        }
    }


    void PlayerDead(bool isDueToMinusProp)
    {
        if (liveManager.GetLivesLeft() <= 0 || liveManager.GetLivesRight() <= 0)
        {

            //updates the ui if one of the players lost all of their health
            Time.timeScale = 0;
            gameController.StopFlashing();
            if (isDueToMinusProp)
            {
                deathText.gameObject.SetActive(true);
                deathText.text = "YOU WIN";
                deathText.color = Color.green;
                winText.gameObject.SetActive(true);
                winText.text = "YOU DIE";
                winText.color = Color.red;
            }
            else
            {
                deathText.gameObject.SetActive(true);
                deathText.text = "YOU DIE";
                deathText.color = Color.red;
                winText.gameObject.SetActive(true);
                winText.text = "YOU WIN";
                winText.color = Color.green;
            }


            navArea.gameObject.SetActive(true);
            broadcastMsg.text = "GAME OVER";
            broadcastMsg.color = Color.black;

            // Level Completion Reason Metric #2 
            gameController.reasonforFinshingLevel = 1;

            gameController.StopScoreCalculation(transform.name);
        }
    }

    public void DisplaySwitchMessage()
    {
        winText.text = "CONTROLS SWITCHED!";
        winText.color = Color.blue;
        winText.gameObject.SetActive(true);
        StartCoroutine(HideSwitchMessage(1f));
    }

    public void DisplayRevertMessage()
    {
        winText.text = "CONTROLS\nRESET!";
        winText.color = Color.blue;
        winText.gameObject.SetActive(true);
        StartCoroutine(HideSwitchMessage(1f));
    }

    IEnumerator HideSwitchMessage(float delay)
    {
        yield return new WaitForSeconds(delay);
        winText.gameObject.SetActive(false);
    }


    private void UpdateReverseIconVisibility()
    {
        if (reverseIcon != null)
        {
            reverseIcon.SetActive(reversed);
        }
        else
        {
            Debug.LogError("ReverseIcon GameObject is not assigned!");
        }
    }

    private void UpdateBackground()
    {
        if (background == null)
        {
            Debug.LogError("Background GameObject is not assigned!");
            return;
        }

        if (reversed)
        {
            //  set background color to "#FFFE9C" and alpha to 160
            background.GetComponent<SpriteRenderer>().color = new Color(1f, 254f / 255f, 156f / 255f, 160f / 255f);
        }
        else
        {
            // set background color to "#FFFFFF" and alpha to 160
            background.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 160f / 255f);
        }
    }
}