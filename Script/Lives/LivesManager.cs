using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LivesManager : MonoBehaviour
{
    public Image[] filledHeartsLeft;
    public Image[] filledHeartsRight;


    int maxLives = 4;
    int currentLivesLeft = 4;
    int currentLivesRight = 4;


    public Vector2 initialPosition = new Vector2(0, 0);

    // Start is called before the first frame update
    void Start()
    {

    }


    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateHeartsVisualLeft()
    {
        for (int i = 0; i < currentLivesLeft; i++)
        {
            filledHeartsLeft[i].transform.localScale = Vector3.one;
        }
        for (int i = currentLivesLeft; i < maxLives; i++)
        {
            filledHeartsLeft[i].transform.localScale = Vector3.zero;
        }
    }

    void UpdateHeartsVisualRight()
    {
        for (int i = 0; i < currentLivesRight; i++)
        {
            filledHeartsRight[i].transform.localScale = Vector3.one;
        }
        for (int i = currentLivesRight; i < maxLives; i++)
        {
            filledHeartsRight[i].transform.localScale = Vector3.zero;
        }
    }

    public void IncrementLivesLeft()
    {
        if (currentLivesLeft != maxLives)
        {
            currentLivesLeft += 1;
            UpdateHeartsVisualLeft();
        }
       
    }

    public void DecrementLivesLeft()
    {
        currentLivesLeft -= 1;
        UpdateHeartsVisualLeft();
    }

    public void IncrementLivesRight()
    {
        if (currentLivesRight != maxLives)
        {
            currentLivesRight += 1;
            UpdateHeartsVisualRight();
        }
    }

    public void DecrementLivesRight()
    {
        currentLivesRight -= 1;
        UpdateHeartsVisualRight();
    }

    public int GetLivesRight()
    {
        return currentLivesRight;
    }

    public int GetLivesLeft()
    {
        return currentLivesLeft;
    }
}
