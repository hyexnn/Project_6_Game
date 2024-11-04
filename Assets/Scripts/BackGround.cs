using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour
{
    public float speed;
    public int startIndex;
    public int endIndex;
    public Transform[] sprites;

    float viewWidth;

    private void Awake()
    {
        viewWidth = Camera.main.orthographicSize * 2 * Camera.main.aspect;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Scrolling();
    }

    void Move()
    {
        //#. Sprite ReUse
        Vector3 curPos = transform.position;
        Vector3 nextPos = Vector3.left * speed * Time.deltaTime;
        transform.position = curPos + nextPos;
    }

    void Scrolling()
    {
        if (sprites[endIndex].position.x < viewWidth * (-1))
        {
            //#. Sprite ReUse
            Vector3 backSpritePos = sprites[startIndex].localPosition;
            Vector3 frontSpritePos = sprites[endIndex].localPosition;
            sprites[endIndex].transform.localPosition = backSpritePos + Vector3.right * 10;

            //#. Cursor Indexes Change
            int startIndexSave = startIndex;
            startIndex = endIndex;
            endIndex = (startIndexSave - 1 == -1) ? sprites.Length - 1 : startIndexSave - 1;
        }
    }
}