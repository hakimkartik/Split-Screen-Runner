using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjMoveMiddle : MonoBehaviour
{
    public float moveSpeed = 2.5f;

    public float speed = 2.5f;

    public float range = 2.8f;

    public float oscillateSpeed = 1f;

    private bool movingRight = true;

    private float startPos;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);

        float newX = transform.position.x + (movingRight ? oscillateSpeed : -oscillateSpeed) * Time.deltaTime;
        if (Mathf.Abs(newX - startPos) >= range)
        {
            movingRight = !movingRight;
            newX = Mathf.Clamp(newX, startPos - range, startPos + range);
        }
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
    }
}
