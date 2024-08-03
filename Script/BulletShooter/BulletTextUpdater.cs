using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BulletTextUpdater : MonoBehaviour
{
    public RotateBulletShooter rotateBulletShooterLeft;
    public RotateBulletShooter rotateBulletShooterRight;

    public TextMeshProUGUI currentBulletsTextLeft;
    public TextMeshProUGUI currentBulletsTextRight;


    public Image ShootingTextNavAreaLeft;
    public Image ShootingTextNavAreaRight;
    public TextMeshProUGUI ShootingTextLeft;
    public TextMeshProUGUI ShootingTextRight;

    private void Start()
    {
        rotateBulletShooterLeft = GameObject.Find("PivotLeft").GetComponent<RotateBulletShooter>();
        rotateBulletShooterRight = GameObject.Find("PivotRight").GetComponent<RotateBulletShooter>();
        StartCoroutine(HideShootingText());
    }

    void Update()
    {
        UpdateBulletText();
    }

    void UpdateBulletText()
    {
        if (currentBulletsTextLeft != null)
        {
          //  Debug.Log("hello?: " + rotateBulletShooterLeft.currentBulletsLeft.ToString());
            currentBulletsTextLeft.text = ":" + rotateBulletShooterLeft.currentBulletsLeft.ToString();
        }
        if (currentBulletsTextRight != null)
        {
            currentBulletsTextRight.text = ":" + rotateBulletShooterRight.currentBulletsRight.ToString();
        }
    }

    IEnumerator HideShootingText()
    {

        yield return new WaitForSeconds(3); // Wait for 3 seconds
        if (ShootingTextNavAreaLeft != null)
        {
            ShootingTextNavAreaLeft.gameObject.SetActive(false);
        }
        if (ShootingTextNavAreaRight != null)
        {
            ShootingTextNavAreaRight.gameObject.SetActive(false);
        }
        if (ShootingTextLeft != null)
        {
            ShootingTextLeft.gameObject.SetActive(false);
        }
        if (ShootingTextRight != null)
        {
            ShootingTextRight.gameObject.SetActive(false);
        }

        rotateBulletShooterLeft.InitializeBullets();
        rotateBulletShooterRight.InitializeBullets();
    }
}

