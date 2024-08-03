using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

using Proyecto26;
using UnityEngine.SceneManagement;
using PortKey.Assets.Script.SwitchLevel;
using PortKey.Assets.Script;


public class GameControllerTutorial : MonoBehaviour
{
    public Transform zoom1;

    public Transform zoom2;

    public TextMeshProUGUI leftScore;

    public TextMeshProUGUI rightScore;

    public TextMeshProUGUI leftMsg;

    public TextMeshProUGUI rightMsg;

    public TextMeshProUGUI broadcastMsg;

    public TextMeshProUGUI broadcastMsgLeft;

    public TextMeshProUGUI broadcastMsgRight;

    public TextMeshProUGUI TimerMsg;

    public Transform carLeft;

    public Transform carRight;

    public Sprite spriteA;

    public Sprite spriteD;

    public Image imageA;

    public Image imageD;

    public Sprite spriteLeft;

    public Sprite spriteRight;

    public Image imageLeft;

    public Image imageRight;

    public Image navArea;

    public Image leftMud;

    public Image rightMud;

    public TextMeshProUGUI player1;
    public TextMeshProUGUI player2;
    public Image controlLeft1;
    public Image controlRight1;
    public Image controlLeft2;
    public Image controlRight2;

    public Image flipLeft;
    public Image flipRight;
    public TextMeshProUGUI fiveLeft;
    public TextMeshProUGUI fiveRight;

    public Image spotlightLeft;
    public Image spotlightRight;
    public Image spotlightIconLeft1;
    public Image spotlightIconLeft2;
    public Image spotlightIconRight1;
    public Image spotlightIconRight2;
    public Image spotlightLivesRight;
    public Image spotlightLivesLeft;

    public Image spotlightForRightTimer;
    public Image spotlightForLeftTimer;

    private RectTransform controlLeft1Rect;
    private RectTransform controlRight1Rect;
    private RectTransform controlLeft2Rect;
    private RectTransform controlRight2Rect;
    private bool move = false;

    private float currentLeftScore = 0f;

    private float currentRightScore = 0f;

    private float scoreMultiplier = 1.0f;

    private readonly float baseScore = 1.0f;

    // game duration, unit is second
    private float gameDuration = 56f;

    //analytics helper variables
    public int totalCtrlSwitchPropCollectedRight = 0;

    public int totalCtrlSwitchPropCollectedLeft = 0;

    public int level;

    public int reasonforFinshingLevel;

    public int collisionDueToCtrlFlipLeft;

    public int collisionDueToCtrlFlipRight;

    public int totalScoreUpLeft;

    public int totalScoreUpRight;

    public TextMeshProUGUI LostHealthMsgRight;

    public TextMeshProUGUI LostHealthMsgLeft;

    public Image CountDownNavArea;
    public TextMeshProUGUI CountDownLeftText;

    public bool canMove;

    public TextMeshProUGUI LeftCtrlFlipTimer;

    public TextMeshProUGUI RightCtrlFlipTimer;

    public int count = 0;

    void Awake()
    {
        if (LostHealthMsgRight != null)
        { LostHealthMsgRight.gameObject.SetActive(false); }

        if (LostHealthMsgLeft != null)
        { LostHealthMsgLeft.gameObject.SetActive(false); }

        if (spotlightForRightTimer != null)
        {
            spotlightForRightTimer.enabled = false;
        }
        if (spotlightForLeftTimer != null)
        {
            spotlightForLeftTimer.enabled = false;
        }
    }


    void Start()
    {
        canMove = true;

        spotlightLeft.enabled = false;
        spotlightRight.enabled = false;
        spotlightIconLeft1.enabled = false;
        spotlightIconLeft2.enabled = false;
        spotlightIconRight1.enabled = false;
        spotlightIconRight2.enabled = false;

        spotlightLivesRight.enabled = false;
        spotlightLivesLeft.enabled = false;

        flipLeft.enabled = false;
        flipRight.enabled = false;
        fiveLeft.gameObject.SetActive(false);
        fiveRight.gameObject.SetActive(false);

        navArea.gameObject.SetActive(false);
        if (leftMud != null && rightMud != null)
        {
            leftMud.enabled = false;
            rightMud.enabled = false;
        }
        level = LevelInfo.Instance.Level;
        Time.timeScale = 1;

        controlLeft1Rect = controlLeft1.GetComponent<RectTransform>();
        controlRight1Rect = controlRight1.GetComponent<RectTransform>();
        controlLeft2Rect = controlLeft2.GetComponent<RectTransform>();
        controlRight2Rect = controlRight2.GetComponent<RectTransform>();

        AddDelayAndStartGame();
    }

    void AddDelayAndStartGame()
    {
        WaitAndStartGame();
    }

    void StopGame()
    {
        StopPlayer();
        StopSpawingProps();
    }

    void StartPlayer()
    {
        carLeft.GetComponent<CarMoveTutorial>().canMove = true;
        carRight.GetComponent<CarMoveTutorial>().canMove = true;
    }

    void StartSpawingProps()
    {
        //zoom1.GetComponent<SpawnObjController>().isStopSpawn = false;
        //zoom2.GetComponent<SpawnObjController>().isStopSpawn = false;
    }

    void StopPlayer()
    {
        carLeft.GetComponent<CarMoveTutorial>().canMove = false;
        carRight.GetComponent<CarMoveTutorial>().canMove = false;
    }

    void StopSpawingProps()
    {
        //zoom1.GetComponent<SpawnObjController>().isStopSpawn = true;
        //zoom2.GetComponent<SpawnObjController>().isStopSpawn = true;
    }

    void WaitAndStartGame()
    {
        StopGame();
        StartGame();
    }

    void StartGame()
    {
        StartPlayer();
        StartSpawingProps();

        if (leftScore != null)
        {
            InvokeRepeating("CalculateScoreLeft", 1, 1);
        }

        if (rightScore != null)
        {
            InvokeRepeating("CalculateScoreRight", 1, 1);
        }

        StartCoroutine(CountdownTimer());
    }

    void Update()
    {
        if (move)
        {
            controlLeft1Rect.Translate(new Vector3(0, -200.0f * Time.deltaTime, 0));
            controlRight1Rect.Translate(new Vector3(0, -200.0f * Time.deltaTime, 0));
            controlLeft2Rect.Translate(new Vector3(0, -200.0f * Time.deltaTime, 0));
            controlRight2Rect.Translate(new Vector3(0, -200.0f * Time.deltaTime, 0));
            if (controlLeft1Rect.anchoredPosition.y < -502)
            {
                move = false;
                controlLeft1.enabled = false;
                controlRight1.enabled = false;
                controlLeft2.enabled = false;
                controlRight2.enabled = false;
            }
        }
    }


    IEnumerator CountdownTimer()
    {
        float timer = gameDuration;
        while (gameDuration > 0)
        {
            if (canMove)
            {
                TimerMsg.text = "" + Mathf.Ceil(gameDuration).ToString() + "s";
                if ((timer - gameDuration) == 5f)
                {
                    StartCoroutine(FadeOutText(player1));
                    StartCoroutine(FadeOutText(player2));
                    move = true;
                }
                yield return new WaitForSeconds(1f);
                // Decrease game duration by 1 second
                gameDuration -= 1f;
            }
            else
            {
                yield return null;
            }
        }
        TimerMsg.text = "0s";

        //  Metric #2 
        reasonforFinshingLevel = 2;
        //posting the analytics to the firebase
        Anaytics();

        // Pause the game when the game duration is over
        PauseGame();
        navArea.gameObject.SetActive(true);
        if (LostHealthMsgRight != null && LostHealthMsgLeft != null)
        {
            LostHealthMsgLeft.gameObject.SetActive(false);
            LostHealthMsgRight.gameObject.SetActive(false);
        }
        //making sure everything that might be falshing will be visible!
        StopFlashing();
        broadcastMsg.text = "CONTINUE TO\nLEVEL 1";
        broadcastMsg.color = Color.black;
        broadcastMsg.gameObject.SetActive(true);

    }

    IEnumerator FadeOutText(TextMeshProUGUI text)
    {
        float time = 0f;
        while (time < 2.0f)
        {
            time += Time.deltaTime;
            float alpha = Mathf.Lerp(1, 0, time / 2.0f);
            text.color = new Color(Color.black.r, Color.black.g, Color.black.b, alpha);
            yield return null;
        }
        text.gameObject.SetActive(false);
    }

    public void StopFlashing()
    {
        leftScore.color = Color.black;
        leftScore.enabled = true;
        rightScore.color = Color.black;
        rightScore.enabled = true;
        imageLeft.GetComponentInChildren<TextMeshProUGUI>().color = Color.black;
        imageRight.GetComponentInChildren<TextMeshProUGUI>().color = Color.black;
        imageLeft.enabled = true;
        imageRight.enabled = true;
        imageA.GetComponentInChildren<TextMeshProUGUI>().color = Color.black;
        imageD.GetComponentInChildren<TextMeshProUGUI>().color = Color.black;
        imageA.enabled = true;
        imageD.enabled = true;
    }

    void PauseGame()
    {
        // Stop the game
        Time.timeScale = 0;
        Debug.Log("Game Paused! Time's up.");
    }

    public void SwitchScreen(string carName)
    {
        if (carName == ConstName.LEFT_CAR)
        {
            CarLeftStop();
        }
        else
        {
            CarRightStop();
        }
    }

    public void EnemyControlReverse(string carName)
    {
        CarMoveTutorial carMove;
        Image currentImage, swappedImage;
        TextMeshProUGUI timerText;
        Image flipImage;

        if (carName == ConstName.LEFT_CAR)
        {
            carMove = carRight.GetComponent<CarMoveTutorial>();
            currentImage = imageLeft;
            swappedImage = imageRight;
            timerText = RightCtrlFlipTimer;
            flipImage = flipRight;
            StartCoroutine(Spotlight(spotlightIconRight1, 4f));
            StartCoroutine(Spotlight(spotlightIconRight2, 4f));
            if (spotlightForRightTimer != null)
            {
                //Debug.Log("Calling spotlightForRightTimer for spotlight");
                StartCoroutine(Spotlight(spotlightForRightTimer, 8f));
            }
            count++;
        }
        else
        {
            carMove = carLeft.GetComponent<CarMoveTutorial>();
            currentImage = imageA;
            swappedImage = imageD;
            timerText = LeftCtrlFlipTimer;
            flipImage = flipLeft;
            StartCoroutine(Spotlight(spotlightIconLeft1, 4f));
            StartCoroutine(Spotlight(spotlightIconLeft2, 4f));
            if (spotlightForLeftTimer != null)
            {
                //Debug.Log("Calling spotlightForLeftTimer for spotlight");
                StartCoroutine(Spotlight(spotlightForLeftTimer, 8f));
            }
            count++;
        }

        // if the car is already in the process of reversing, stop the current coroutine
        if (carMove.currentRevertCoroutine != null)
        {
            StopCoroutine(carMove.currentRevertCoroutine);
        }

        // if the car is not reversed, reverse it
        if (!carMove.reversed)
        {
            ReverseControl(carMove, currentImage, swappedImage, flipImage);
        }

        // start the timer for the car to revert back to normal control
        carMove.currentRevertCoroutine = StartCoroutine(RevertControl(carName, timerText, flipImage));

    }

    private void ReverseControl(CarMoveTutorial carMove, Image currentImage, Image swappedImage, Image flipImage)
    {
        carMove.carSpeed *= -1;
        carMove.reversed = true;
        //Sprite tempSprite = currentImage.sprite;
        //currentImage.sprite = swappedImage.sprite;
        //swappedImage.sprite = tempSprite;
        StartCoroutine(Flashing(currentImage, swappedImage, flipImage));
    }

    private IEnumerator RevertControl(string carName, TextMeshProUGUI timerText, Image flipImage)
    {
        float timeRemaining = 4.0f;
        while (timeRemaining > 0)
        {
            timerText.text = $"{Mathf.FloorToInt(timeRemaining)}";
            yield return new WaitForSeconds(1);
            timeRemaining -= 1;
        }

        CarMoveTutorial carMove = (carName == ConstName.LEFT_CAR) ? carRight.GetComponent<CarMoveTutorial>() : carLeft.GetComponent<CarMoveTutorial>();
        CarMoveTutorial carSwitch = (carName == ConstName.LEFT_CAR) ? carLeft.GetComponent<CarMoveTutorial>() : carRight.GetComponent<CarMoveTutorial>();
        Image currentImage = (carName == ConstName.LEFT_CAR) ? imageRight : imageD;
        Image swappedImage = (carName == ConstName.LEFT_CAR) ? imageLeft : imageA;

        timerText.text = "";
        if (carMove.reversed)
        {
            count++;
            ReverseControl(carMove, currentImage, swappedImage, flipImage);
            carSwitch.DisplayRevertMessage();
            carMove.reversed = false;
            carMove.currentRevertCoroutine = null;
        }
    }


    IEnumerator Flashing(Image left, Image right, Image flip)
    {
        if (count == 1 || count == 3)
        {
            Time.timeScale = 0;
            for (int i = 0; i < 5; i++)
            {
                if (i == 2)
                {
                    left.GetComponentInChildren<TextMeshProUGUI>().color = Color.green;
                    right.GetComponentInChildren<TextMeshProUGUI>().color = Color.green;
                    Sprite oldleft = left.sprite;
                    Sprite oldright = right.sprite;
                    left.sprite = oldright;
                    right.sprite = oldleft;
                    flip.enabled = true;
                }
                left.enabled = false;
                right.enabled = false;
                yield return new WaitForSecondsRealtime(0.4f);
                left.enabled = true;
                right.enabled = true;
                yield return new WaitForSecondsRealtime(0.4f);
            }
            left.GetComponentInChildren<TextMeshProUGUI>().color = Color.black;
            right.GetComponentInChildren<TextMeshProUGUI>().color = Color.black;
            flip.enabled = false;
            Time.timeScale = 1;
        }
        else
        {
            Sprite oldleft = left.sprite;
            Sprite oldright = right.sprite;
            left.sprite = oldright;
            right.sprite = oldleft;
            left.GetComponentInChildren<TextMeshProUGUI>().color = Color.green;
            right.GetComponentInChildren<TextMeshProUGUI>().color = Color.green;
            for (int i = 0; i < 3; i++)
            {
                left.enabled = false;
                right.enabled = false;
                yield return new WaitForSeconds(0.2f);
                left.enabled = true;
                right.enabled = true;
                yield return new WaitForSeconds(0.2f);
            }
            left.GetComponentInChildren<TextMeshProUGUI>().color = Color.black;
            right.GetComponentInChildren<TextMeshProUGUI>().color = Color.black;
        }
    }

    public void ShowSpeedSlowMsg(string carName)
    {
        if (carName == ConstName.LEFT_CAR)
        {
            //broadcastMsgRight.text = "Your speed has been reduced!";
            //broadcastMsgRight.gameObject.SetActive(true);
            //StartCoroutine(HideRightMessage());
            rightMud.enabled = true;
            StartCoroutine(HideMud(rightMud));
        }
        else
        {
            //broadcastMsgLeft.text = "Your speed has been reduced!";
            //broadcastMsgLeft.gameObject.SetActive(true);
            //StartCoroutine(HideLeftMessage());
            leftMud.enabled = true;
            StartCoroutine(HideMud(leftMud));
        }
    }

    IEnumerator HideRightMessage()
    {
        yield return new WaitForSeconds(1f);
        broadcastMsgRight.gameObject.SetActive(false);
    }

    IEnumerator HideLeftMessage()
    {
        yield return new WaitForSeconds(1f);
        broadcastMsgLeft.gameObject.SetActive(false);
    }

    IEnumerator HideMud(Image mud)
    {
        yield return new WaitForSeconds(4f);
        mud.enabled = false;
    }

    public void CarLeftStop()
    {
        // stop zoom1 object movement
        zoom1.GetComponent<SpawnObjController>().isStopSpawn = true;
        foreach (Transform t in zoom1)
        {
            t.GetComponent<ObjMove>().speed = 0;
        }

        // start zoom2 object movement
        zoom2.GetComponent<SpawnObjController>().isStopSpawn = false;
        foreach (Transform t in zoom2)
        {
            t.GetComponent<ObjMove>().speed = t.GetComponent<ObjMove>().moveSpeed;
        }

        // stop CarLeft movement
        carLeft.GetComponent<CarMoveTutorial>().canMove = false;

        // start CarRight movement
        carRight.GetComponent<CarMoveTutorial>().canMove = true;
    }

    public void CarRightStop()
    {
        // stop zoom2 object movement
        zoom2.GetComponent<SpawnObjController>().isStopSpawn = true;
        foreach (Transform t in zoom2)
        {
            t.GetComponent<ObjMove>().speed = 0;
        }

        // start zoom1 object movement
        zoom1.GetComponent<SpawnObjController>().isStopSpawn = false;
        foreach (Transform t in zoom1)
        {
            t.GetComponent<ObjMove>().speed = t.GetComponent<ObjMove>().moveSpeed;
        }

        // stop CarRight movement
        carRight.GetComponent<CarMoveTutorial>().canMove = false;

        // start CarLeft movement
        carLeft.GetComponent<CarMoveTutorial>().canMove = true;
    }

    public void StopScoreCalculation(string carName)
    {
        if (carName == ConstName.LEFT_CAR)
        {
            CancelInvoke("CalculateScoreLeft");
        }
        else if (carName == ConstName.RIGHT_CAR)
        {
            CancelInvoke("CalculateScoreRight");
        }

        //posting the analytics to the firebase
        Anaytics();

    }

    void Anaytics()
    {
        //parsing the current level number
        string levelName = SceneManager.GetActiveScene().name;
        char levelLastChar = levelName[levelName.Length - 1];
        int levelNumber;
        int.TryParse(levelLastChar.ToString(), out levelNumber);

        //posting the analytics to the firebase
        PlayerData playerData = new PlayerData();
        playerData.name = "player";
        playerData.level = levelNumber;
        playerData.scoreLeft = totalScoreUpLeft;
        playerData.scoreRight = totalScoreUpRight;
        playerData.reasonforFinshingLevel = reasonforFinshingLevel;
        playerData.totalCtrlSwitchPropCollectedRight = totalCtrlSwitchPropCollectedRight;
        playerData.totalCtrlSwitchPropCollectedLeft = totalCtrlSwitchPropCollectedLeft;
        playerData.collisionDueToCtrlFlipLeft = collisionDueToCtrlFlipLeft;
        playerData.collisionDueToCtrlFlipRight = collisionDueToCtrlFlipRight;

        //string json = JsonUtility.ToJson(playerData);

        if (ConstName.SEND_ANALYTICS == true)
        {
            RestClient.Post("https://portkey-2a1ae-default-rtdb.firebaseio.com/gold_podtesting_analytics.json", playerData);
     //       RestClient.Post("https://portkey-2a1ae-default-rtdb.firebaseio.com/beta_playtesting_analytics.json", playerData);
      //      RestClient.Post("https://portkey-2a1ae-default-rtdb.firebaseio.com/playtesting1_analytics.json", playerData);
        }
    }

    void CalculateScoreLeft()
    {
        if (canMove)
        {
            currentLeftScore += baseScore * scoreMultiplier;
            leftScore.text = "" + currentLeftScore.ToString("F0");
        }
    }

    void CalculateScoreRight()
    {
        if (canMove)
        {
            currentRightScore += baseScore * scoreMultiplier;
            rightScore.text = "" + currentRightScore.ToString("F0");
        }
    }

    public void OneTimeBonus(string carName)
    {
        if (carName == "CarLeft")
        {
            Time.timeScale = 0;
            StartCoroutine(FlashScore(leftScore, new Color(1.0f, 0.7882353f, 0.28235295f), true, fiveLeft));
            StartCoroutine(Spotlight(spotlightLeft, 4f));
        }
        else
        {
            Time.timeScale = 0;
            StartCoroutine(FlashScore(rightScore, new Color(1.0f, 0.7882353f, 0.28235295f), false, fiveRight));
            StartCoroutine(Spotlight(spotlightRight, 4f));
        }
    }

    private IEnumerator Spotlight(Image spotlight, float delay)
    {
        spotlight.enabled = true;
        yield return new WaitForSecondsRealtime(delay);
        spotlight.enabled = false;
    }

    public void SpotlightLives(string carName)
    {
        if (carName == ConstName.LEFT_CAR)
        {
            StartCoroutine(Spotlight(spotlightLivesLeft, 2f));
        }
        else
        {
            StartCoroutine(Spotlight(spotlightLivesRight, 2f));
        }
    }

    IEnumerator FlashScore(TextMeshProUGUI score, Color col, bool left, TextMeshProUGUI five)
    {
        for (int i = 0; i < 5; i++)
        {
            if (i == 2)
            {
                score.color = col;
                if (left)
                {
                    currentLeftScore += 5;
                    score.text = currentLeftScore.ToString("F0");
                }
                else
                {
                    currentRightScore += 5;
                    score.text = currentRightScore.ToString("F0");
                }
                five.gameObject.SetActive(true);
            }
            score.enabled = false;
            yield return new WaitForSecondsRealtime(0.4f);
            score.enabled = true;
            yield return new WaitForSecondsRealtime(0.4f);
        }
        score.color = Color.black;
        five.gameObject.SetActive(false);
        Time.timeScale = 1;
    }


    public void DisplayRightLostHealthMsg()
    {
        //BulletImpactForRightPlayer();
        LostHealthMsgRight.text = "Health Stolen";
        LostHealthMsgRight.color = Color.blue;
        LostHealthMsgRight.gameObject.SetActive(true);
        StartCoroutine(HideStolenHealthMessage(1f));
    }

    public void DisplayLeftLostHealthMsg()
    {
        //BulletImpactForLeftPlayer();
        LostHealthMsgLeft.text = "Health Stolen";
        LostHealthMsgLeft.color = Color.blue;
        LostHealthMsgLeft.gameObject.SetActive(true);
        StartCoroutine(HideStolenHealthMessage(1f));
    }


    IEnumerator HideStolenHealthMessage(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (LostHealthMsgRight != null && LostHealthMsgLeft != null)
        {
            LostHealthMsgLeft.gameObject.SetActive(false);
            LostHealthMsgRight.gameObject.SetActive(false);
        }
    }

    public void UpdateHealthBarOnCollision(string carName, float impact, bool impactOppositePlayer = false)
    {
        //decrement healthBar accordingly
        if (impactOppositePlayer == false)
        {

            if (carName == ConstName.LEFT_CAR)
            {
                carLeft.GetComponent<CarMoveTutorial>().playerHealth += impact;
                float currentHealth = carLeft.GetComponent<CarMoveTutorial>().playerHealth;
                float maxHealth = carLeft.GetComponent<CarMoveTutorial>().maxHealth;
                carLeft.GetComponent<CarMoveTutorial>().healthBar.UpdateLeftPlayerHealthBar(currentHealth, maxHealth);
                carLeft.GetComponent<CarMoveTutorial>().ShakePlayerOnHealthLoss();
            }
            else
            {
                carRight.GetComponent<CarMoveTutorial>().playerHealth += impact;
                float currentHealth = carRight.GetComponent<CarMoveTutorial>().playerHealth;
                float maxHealth = carRight.GetComponent<CarMoveTutorial>().maxHealth;
                carRight.GetComponent<CarMoveTutorial>().healthBar.UpdateRightPlayerHealthBar(currentHealth, maxHealth);
                carRight.GetComponent<CarMoveTutorial>().ShakePlayerOnHealthLoss();
            }
        }
        else
        {
            if (carName != ConstName.LEFT_CAR)
            {
                carLeft.GetComponent<CarMoveTutorial>().playerHealth += impact;
                float currentHealth = carLeft.GetComponent<CarMoveTutorial>().playerHealth;
                float maxHealth = carLeft.GetComponent<CarMoveTutorial>().maxHealth;
                carLeft.GetComponent<CarMoveTutorial>().healthBar.UpdateLeftPlayerHealthBar(currentHealth, maxHealth);
                carLeft.GetComponent<CarMoveTutorial>().ShakePlayerOnHealthLoss();
            }
            else
            {
                carRight.GetComponent<CarMoveTutorial>().playerHealth += impact;
                float currentHealth = carRight.GetComponent<CarMoveTutorial>().playerHealth;
                float maxHealth = carRight.GetComponent<CarMoveTutorial>().maxHealth;
                carRight.GetComponent<CarMoveTutorial>().healthBar.UpdateRightPlayerHealthBar(currentHealth, maxHealth);
                carRight.GetComponent<CarMoveTutorial>().ShakePlayerOnHealthLoss();
            }
        }

    }
}
