using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjMoveTutorial2 : MonoBehaviour
{
    public float moveSpeed = 2f;

    public float speed = 2f;

    public Image spotlightLeft;
    public Image spotlightRight;

    GameControllerTutorial2 gameController;
    Vector3 originalScale;
    bool isBlinking = false;

    // Start is called before the first frame update
    void Start()
    {
        gameController = FindObjectOfType<GameControllerTutorial2>();
        originalScale = transform.localScale;
        if (spotlightRight != null)
        {
            spotlightLeft.enabled = false;
            spotlightRight.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (gameController.canMove)
        {
            //Debug.Log("Moving down for " + gameObject.name);
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
            if (transform.position.x < 0.0f)
            {
                spotlightLeft.enabled = true;
            } else
            {
                spotlightRight.enabled = true;
            }
        }
    }

    void OnDestroy()
    {
        gameController.canMove = true;

        // Stop blinking stuff
        StopCoroutine(BlinkObject());
        transform.localScale = originalScale;
        isBlinking = false;

        if (spotlightRight != null)
        {
            if (transform.position.x < 0.0f)
            {
                spotlightLeft.enabled = false;
            }
            else
            {
                spotlightRight.enabled = false;
            }
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
