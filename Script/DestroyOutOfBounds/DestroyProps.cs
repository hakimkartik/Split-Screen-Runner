using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyProps : MonoBehaviour
{
    int yPos = 15;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DestroyProp();
    }

    void DestroyProp()
    {
        if (transform.position.y < -yPos)
        {
            //Debug.Log("Destroying " + gameObject.name);
            Destroy(gameObject);
        }
    }
}
