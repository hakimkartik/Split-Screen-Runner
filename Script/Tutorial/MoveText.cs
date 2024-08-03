using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MoveText : MonoBehaviour
{
    private TextMeshPro txt;
    private float time;
    private Color initCol = Color.red;

    void Start()
    {
        txt = GetComponent<TextMeshPro>();
        if (txt != null)
        {
            txt.color = initCol;
        }
    }

    void Update()
    {
        time += Time.deltaTime * 2.0f;
        if (txt != null)
        {
            float alpha = Mathf.Abs(Mathf.Sin(time));
            txt.color = new Color(initCol.r, initCol.g, initCol.b, alpha);
        }
    }
}
