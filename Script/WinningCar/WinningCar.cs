using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinningCar : MonoBehaviour
{

    GameController gameController;

    // Start is called before the first frame update
    void Start()
    {
        gameController = FindObjectOfType<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTrophyPlacement(470, 280, Vector3.one);
    }


    //update the position of trophy to the winning side's screen
    void UpdateTrophyPlacement(float x, float y, Vector3 scale)
    {
        Vector3 newPosition = transform.localPosition;
        newPosition.y = y;

        if (gameController.isGameOver)
        {
            transform.localScale = Vector3.zero;
        }
        else if (gameController.GetCurrentScoreLeft() > gameController.GetCurrentScoreRight())
        {
            newPosition.x = -x;
            transform.localPosition = newPosition;
            transform.localScale = scale;
        }
        else if (gameController.GetCurrentScoreLeft() < gameController.GetCurrentScoreRight())
        {
            newPosition.x = x;
            transform.localPosition = newPosition;
            transform.localScale = scale;
        }
        else
        {
            //make it invisible if tie
            transform.localScale = Vector3.zero;
        }
    }
}
