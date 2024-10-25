using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour
{
    public float speed;
    public int startIndex;
    public int endIndex;
    public Transform[] sprites;

    float viewHeight;

    private void Awake()
    {
        viewHeight = Camera.main.orthographicSize * 2;
    }
    // Update is called once per frame
    void Update()
    {
        Move();
        Scrolling();
    }

    void Move()
    {
        //#.Sprite ReUse
        Vector3 curPos = transform.position;
        Vector3 nextPos = Vector3.down * speed * Time.deltaTime;
        transform.position = curPos + nextPos;
    }

    void Scrolling()
    {
        if (sprites[endIndex].position.y < viewHeight * (-1))
        {
            //#.Sprite ReUse
            Vector3 backSpritPos = sprites[startIndex].localPosition;
            Vector3 frontSpritPos = sprites[endIndex].localPosition;
            sprites[endIndex].transform.localPosition = backSpritPos + Vector3.up * 10;

            //#.Cursor Indexs Change
            int startIndexSave = startIndex;
            startIndex = endIndex;
            endIndex = (startIndexSave - 1 == -1) ? sprites.Length - 1 : startIndexSave - 1;
        }
    }
}
