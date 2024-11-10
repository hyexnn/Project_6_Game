using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target_Move : MonoBehaviour
{
    public float MoveSpeed = 1.0f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position += new Vector3(-1.0f, 0.0f, 0.0f) * Time.deltaTime * MoveSpeed;

        if (gameObject.transform.position.x < -5)
        {
            gameObject.transform.position += new Vector3(5.0f, 0.0f, 0.0f);
        }
    }

    public void Die()
    {
        gameObject.SetActive(false);                //자신의 게임 오브젝트를 비활성화

    }
}
