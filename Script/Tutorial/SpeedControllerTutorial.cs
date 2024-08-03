using System.Collections;
using System.Collections.Generic;
using PortKey.Assets.Script;
using PortKey.Assets.Script.SwitchLevel;
using UnityEngine;

public class SpeedControllerTutorial : MonoBehaviour
{
    private CarMoveTutorial carLeftMove;

    private CarMoveTutorial carRightMove;

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


    // Start is called before the first frame update
    void Start()
    {
        level = LevelInfo.Instance.Level;
        Debug.Log("Level: " + level);
        speedLimits.SetLevelParameters(level);

        GameObject carLeft = GameObject.Find(ConstName.LEFT_CAR);
        GameObject carRight = GameObject.Find(ConstName.RIGHT_CAR);

        if (carLeft != null)
        {
            carLeftMove = carLeft.GetComponent<CarMoveTutorial>();
            if (carLeftMove == null)
            {
                Debug.LogError("CarLeft does not have a CarMoveTutorial component!");
            }
        }
        else
        {
            Debug.LogError("CarLeft object not found!");
        }

        if (carRight != null)
        {
            carRightMove = carRight.GetComponent<CarMoveTutorial>();
            if (carRightMove == null)
            {
                Debug.LogError("CarRight does not have a CarMoveTutorial component!");
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

    private IEnumerator SlowDownCoroutine(CarMoveTutorial carMove, float duration)
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
