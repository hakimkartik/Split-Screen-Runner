using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FollowCar : MonoBehaviour
{
    public RectTransform healthbarLeft;
    public RectTransform healthbarRight;
    public Transform carLeft;
    public Transform carRight;

    private Vector2 initialOffsetLeft;
    private Vector2 initialOffsetRight;

    private void Start()
    {
        // calculate initial offset between the healthbar and the player
        Vector2 screenPointLeft = RectTransformUtility.WorldToScreenPoint(Camera.main, carLeft.position);
        initialOffsetLeft = healthbarLeft.position - (Vector3)screenPointLeft;

        Vector2 screenPointRight = RectTransformUtility.WorldToScreenPoint(Camera.main, carRight.position);
        initialOffsetRight = healthbarRight.position - (Vector3)screenPointRight;
    }

    void LateUpdate()
    {
        // converting player's world position to a screen position to move the healthbars as the player moves
        Vector2 screenPointLeft = RectTransformUtility.WorldToScreenPoint(Camera.main, carLeft.position);
        healthbarLeft.position = screenPointLeft + initialOffsetLeft;

        Vector2 screenPointRight = RectTransformUtility.WorldToScreenPoint(Camera.main, carRight.position);
        healthbarRight.position = screenPointRight + initialOffsetRight;
    }
}
