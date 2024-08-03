using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjMoveTutorial : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float speed = 2f;

    public Image spotlight;

    GameControllerTutorial gameController;
    Vector3 originalScale;
    bool isBlinking = false;

    // Start is called before the first frame update
    void Start()
    {
        gameController = FindObjectOfType<GameControllerTutorial>();
        originalScale = transform.localScale;
        if (spotlight != null)
        {
            spotlight.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (gameController.canMove)
        {
            transform.Translate(Vector3.down * speed * Time.deltaTime);
            isBlinking = false;
        }
        if (transform.position.y <= -3f && transform.gameObject.tag != "Obstacle")
        {
            gameController.canMove = false;

            if (!isBlinking)
            {
                isBlinking = true;
                StartCoroutine(BlinkObject());
            }

            spotlight.enabled = true;
        }
    }

    void OnDestroy()
    {
        gameController.canMove = true;

        // Stop blinking stuff
        StopCoroutine(BlinkObject());
        transform.localScale = originalScale;
        isBlinking = false;

        if (spotlight != null)
        {
            spotlight.enabled = false;
        }
    }

    IEnumerator BlinkObject()
    {
        while (isBlinking)
        {  
            transform.localScale = Vector3.zero;
            yield return new WaitForSeconds(0.2f); 

            transform.localScale = originalScale;
            yield return new WaitForSeconds(0.2f); 
        }
    }
}
