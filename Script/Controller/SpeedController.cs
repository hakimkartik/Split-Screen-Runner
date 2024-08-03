using System.Collections;
using System.Collections.Generic;
using PortKey.Assets.Script;
using PortKey.Assets.Script.SwitchLevel;
using UnityEngine;

public class SpeedLimits
{
    public float defaultCarSpeed = 3.0f;

    public float defaultSpawnSpeed = 2.0f;

    public float carSpeedMultiplier = 1.15f;

    public float spawnSpeedMultiplier = 1.05f;

    public float carMaxSpeed = 10.0f;

    public float obstacleMaxSpeed = 5.0f;

    public float speedIncreaseDuration = 30.0f;

    private bool canIncreaseSpeed = true;

    private bool canIncreaseSpawnSpeed = true;

    public bool CanIncreaseSpawnSpeed
    {
        get { return canIncreaseSpawnSpeed; }
    }

    public bool CanIncreaseSpeed
    {
        get { return canIncreaseSpeed; }
    }

    public void SetLevelParameters(int level)
    {
        switch (level)
        {
            case -2:
                canIncreaseSpeed = true;
                canIncreaseSpawnSpeed = true;
                defaultCarSpeed = 2.0f;
                defaultSpawnSpeed = 2.0f;
                carSpeedMultiplier = 4.0f;
                spawnSpeedMultiplier = 1.0f;
                carMaxSpeed = 7.0f;
                obstacleMaxSpeed = 5.5f;
                speedIncreaseDuration = 50.0f;
                break;
            case 0:
                canIncreaseSpeed = true;
                canIncreaseSpawnSpeed = true;
                defaultCarSpeed = 5.0f;
                defaultSpawnSpeed = 2.0f;
                carSpeedMultiplier = 1.1f;
                spawnSpeedMultiplier = 1.0f;
                carMaxSpeed = 5.0f;
                obstacleMaxSpeed = 4.0f;
                speedIncreaseDuration = 30.0f;
                break;
            case 1:
                canIncreaseSpeed = true;
                canIncreaseSpawnSpeed = true;
                defaultCarSpeed = 5.0f;
                defaultSpawnSpeed = 2.0f;
                carSpeedMultiplier = 1.005f;
                spawnSpeedMultiplier = 1.01f;
                carMaxSpeed = 8.0f;
                obstacleMaxSpeed = 4.0f;
                speedIncreaseDuration = 30.0f;
                break;
            case 2:
                canIncreaseSpeed = true;
                canIncreaseSpawnSpeed = true;
                defaultCarSpeed = 5.0f;
                defaultSpawnSpeed = 2.0f;
                carSpeedMultiplier = 1.005f;
                spawnSpeedMultiplier = 1.012f;
                carMaxSpeed = 9.0f;
                obstacleMaxSpeed = 4.1f;
                speedIncreaseDuration = 30.0f;
                break;
            case 3: //BUFFER LEVEL
                canIncreaseSpeed = true;
                canIncreaseSpawnSpeed = true;
                defaultCarSpeed = 5.0f;
                defaultSpawnSpeed = 2.0f;
                carSpeedMultiplier = 1.01f;
                spawnSpeedMultiplier = 1.01f;//smaller multiplier rate
                carMaxSpeed = 10.0f;
                obstacleMaxSpeed = 3.8f;//a little slower
                speedIncreaseDuration = 40.0f;
                break;
            case 4:
                canIncreaseSpeed = true;
                canIncreaseSpawnSpeed = true;
                defaultCarSpeed = 5.0f;
                defaultSpawnSpeed = 2.0f;
                carSpeedMultiplier = 1.01f;
                spawnSpeedMultiplier = 1.015f;
                carMaxSpeed = 10.0f;
                obstacleMaxSpeed = 4.2f;
                speedIncreaseDuration = 40.0f;
                break;
            case 5: //BUFFER LEVEL
                canIncreaseSpeed = true;
                canIncreaseSpawnSpeed = true;
                defaultCarSpeed = 5.0f;
                defaultSpawnSpeed = 2.0f;
                carSpeedMultiplier = 1.02f;
                spawnSpeedMultiplier = 1.015f;//smaller multiplier rate
                carMaxSpeed = 10.0f;
                obstacleMaxSpeed = 4.1f;//a little slower
                speedIncreaseDuration = 50.0f;
                break;
            case 6:
                canIncreaseSpeed = true;
                canIncreaseSpawnSpeed = true;
                defaultCarSpeed = 5.0f;
                defaultSpawnSpeed = 2.0f;
                carSpeedMultiplier = 1.02f;
                spawnSpeedMultiplier = 1.02f;
                carMaxSpeed = 10.0f;
                obstacleMaxSpeed = 4.5f;
                speedIncreaseDuration = 50.0f;
                break;
            case 7:
                canIncreaseSpeed = true;
                canIncreaseSpawnSpeed = true;
                defaultCarSpeed = 5.0f;
                defaultSpawnSpeed = 2.0f;
                carSpeedMultiplier = 1.03f;
                spawnSpeedMultiplier = 1.02f;
                carMaxSpeed = 12.0f;
                obstacleMaxSpeed = 4.5f;
                speedIncreaseDuration = 50.0f;
                break;
            case 8:
                canIncreaseSpeed = true;
                canIncreaseSpawnSpeed = true;
                defaultCarSpeed = 5.0f;
                defaultSpawnSpeed = 2.0f;
                carSpeedMultiplier = 1.03f;
                spawnSpeedMultiplier = 1.02f;
                carMaxSpeed = 12.0f;
                obstacleMaxSpeed = 4.5f;
                speedIncreaseDuration = 50.0f;
                break;
            default:
                canIncreaseSpeed = true;
                canIncreaseSpawnSpeed = true;
                defaultCarSpeed = 3.0f;
                defaultSpawnSpeed = 2.0f;
                carSpeedMultiplier = 1.05f;
                spawnSpeedMultiplier = 1.01f;
                carMaxSpeed = 8.0f;
                obstacleMaxSpeed = 5.0f;
                speedIncreaseDuration = 30.0f;
                Debug.LogError("Unknown level: " + level);
                break;
        }
    }
}

public class SpeedController : MonoBehaviour
{
    private CarMove carLeftMove;

    private CarMove carRightMove;

    private SpeedLimits speedLimits = new SpeedLimits();

    private int level = 1;

    private float carFrequency = 0.2f;

    private float spawnFrequency = 0.1f;

    private Transform zoomLeft;

    private Transform zoomRight;

    private float carSpeed = 2.0f;

    public float spawnSpeed = 2.0f;

    private bool leftCarSlowDown = false;

    private bool rightCarSlowDown = false;

    private float slowDownFactor = 0.5f;

    GameController gameController;


    // Start is called before the first frame update
    void Start()
    {
        level = LevelInfo.Instance.Level;
        Debug.Log("Level: " + level);
        speedLimits.SetLevelParameters(level);

        GameObject carLeft = GameObject.Find(ConstName.LEFT_CAR);
        GameObject carRight = GameObject.Find(ConstName.RIGHT_CAR);


        gameController = FindObjectOfType<GameController>();
        if (gameController == null)
        {
            Debug.LogError("GameController not found");
        }

        if (carLeft != null)
        {
            carLeftMove = carLeft.GetComponent<CarMove>();
            if (carLeftMove == null)
            {
                Debug.LogError("CarLeft does not have a CarMove component!");
            }
        }
        else
        {
            Debug.LogError("CarLeft object not found!");
        }

        if (carRight != null)
        {
            carRightMove = carRight.GetComponent<CarMove>();
            if (carRightMove == null)
            {
                Debug.LogError("CarRight does not have a CarMove component!");
            }
        }
        else
        {
            Debug.LogError("CarRight object not found!");
        }

        // Initialize zoomLeft and zoomRight
        zoomLeft = GameObject.Find(ConstName.ZOOM_LEFT).transform;
        if (zoomLeft == null)
        {
            Debug.LogError("ZoomLeft object not found!");
        }

        zoomRight = GameObject.Find(ConstName.ZOOM_RIGHT).transform;
        if (zoomRight == null)
        {
            Debug.LogError("ZoomRight object not found!");
        }

        if (carLeftMove != null && carRightMove != null && speedLimits.CanIncreaseSpeed)
        {
            carLeftMove.carSpeed = speedLimits.defaultCarSpeed;
            carRightMove.carSpeed = speedLimits.defaultCarSpeed;
            carSpeed = speedLimits.defaultCarSpeed;
            StartCoroutine(UpdateCarSpeed());
        }

        if (zoomLeft != null && zoomRight != null && speedLimits.CanIncreaseSpawnSpeed)
        {
            spawnSpeed = speedLimits.defaultSpawnSpeed;
            StartCoroutine(UpdateSpawnSpeed());
        }
    }

    public void SlowDownCarTemporarily(string carName, float factor, float duration)
    {
        slowDownFactor = factor;
        if (carName == ConstName.LEFT_CAR && carRightMove != null)
        {
            rightCarSlowDown = true;
            StartCoroutine(SlowDownCoroutine(carRightMove, duration));
        }
        else if (carName == ConstName.RIGHT_CAR && carLeftMove != null)
        {
            leftCarSlowDown = true;
            StartCoroutine(SlowDownCoroutine(carLeftMove, duration));
        }
    }

    private IEnumerator SlowDownCoroutine(CarMove carMove, float duration)
    {
        yield return new WaitForSeconds(duration);

        if (carMove == carLeftMove)
        {
            leftCarSlowDown = false;
        }
        else
        {
            rightCarSlowDown = false;
        }

        Debug.Log("rightCarSlowDown: " + rightCarSlowDown + ", leftCarSlowDown: " + leftCarSlowDown);
    }

    private IEnumerator UpdateCarSpeed()
    {
        // // Wait for the countdown before starting the game
        // int waitingTime = gameController.countDownBeforeStartDuration * 2;
        // yield return new WaitForSeconds(waitingTime);

        // Debug.Log($"Initial speeds: Left: {carLeftMove.carSpeed}, Right: {carRightMove.carSpeed}");

        while (speedLimits.CanIncreaseSpeed)
        {
            float[] leftCarSpeeds = SetCarSpeed(carSpeed, carLeftMove.carSpeed, speedLimits.carMaxSpeed, speedLimits.carSpeedMultiplier, leftCarSlowDown, slowDownFactor);
            carLeftMove.carSpeed = leftCarSpeeds[1];

            float[] rightCarSpeeds = SetCarSpeed(carSpeed, carRightMove.carSpeed, speedLimits.carMaxSpeed, speedLimits.carSpeedMultiplier, rightCarSlowDown, slowDownFactor);
            carRightMove.carSpeed = rightCarSpeeds[1];

            carSpeed = leftCarSpeeds[0];

            // Debug.Log($"Updated speeds: Left: {carLeftMove.carSpeed}, Right: {carRightMove.carSpeed}");

            yield return new WaitForSeconds(carFrequency);
        }
    }

    private float[] SetCarSpeed(float carSpeed, float carSpeedInGame, float maxCarSpeed, float carSpeedMultiplier, bool slowDown, float slowDownFactor)
    {
        {
            int direction = carSpeedInGame > 0 ? 1 : -1;
            carSpeed *= carSpeedMultiplier;
            if (Mathf.Abs(carSpeed) > maxCarSpeed)
            {
                carSpeed = maxCarSpeed;
            }

            if (!slowDown)
            {
                carSpeedInGame = carSpeed * direction;
            }
            else if (slowDownFactor > 0)
            {
                carSpeedInGame = carSpeed * direction * slowDownFactor;
            }

            return new float[] { carSpeed, carSpeedInGame };
        }
    }


    private IEnumerator UpdateSpawnSpeed()
    {

        // // Wait for the countdown before starting the game
        // int waitingTime = gameController.countDownBeforeStartDuration * 2;
        // yield return new WaitForSeconds(waitingTime);

        while (true)
        {
            if (speedLimits.CanIncreaseSpawnSpeed)
            {
                spawnSpeed *= speedLimits.spawnSpeedMultiplier;
            }

            if (Mathf.Abs(spawnSpeed) > speedLimits.obstacleMaxSpeed)
            {
                spawnSpeed = speedLimits.obstacleMaxSpeed;
            }

            if (zoomLeft != null)
            {
                foreach (Transform t in zoomLeft)
                {
                    ObjMove objMove = t.GetComponent<ObjMove>();
                    if (objMove != null)
                    {
                        objMove.speed = spawnSpeed;
                    }

                    ObjMoveMiddle objMoveMiddle = t.GetComponent<ObjMoveMiddle>();
                    if (objMoveMiddle != null)
                    {
                        objMoveMiddle.speed = spawnSpeed;
                    }
                }
            }
            else
            {
                Debug.LogError("ZoomLeft is null");
            }

            if (zoomRight != null)
            {
                foreach (Transform t in zoomRight)
                {
                    ObjMove objMove = t.GetComponent<ObjMove>();
                    if (objMove != null)
                    {
                        objMove.speed = spawnSpeed;
                    }


                    ObjMoveMiddle objMoveMiddle = t.GetComponent<ObjMoveMiddle>();
                    if (objMoveMiddle != null)
                    {
                        objMoveMiddle.speed = spawnSpeed;
                    }
                }
            }
            else
            {
                Debug.LogError("ZoomRight is null");
            }

            yield return new WaitForSeconds(spawnFrequency);
        }
    }
}
