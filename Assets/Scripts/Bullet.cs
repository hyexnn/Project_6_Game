using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float speed = 8f;                    //탄알 이동 속력
    private Rigidbody bulletRigidbody;          //이동에 사용할 이지드바디 컴포넌트


    // Start is called before the first frame update
    void Start()
    {
        bulletRigidbody = GetComponent<Rigidbody>();
        bulletRigidbody.velocity = transform.forward * speed;
        Destroy(gameObject, 3f);
    }

    void OnTriggerEnter(Collider other)         //트리거 충돌 시 자동으로 실행되는 매서드
    {
        if (other.tag == "Target")
        {
            Target_Move Target_Move = other.GetComponent<Target_Move>();
            //상대방 게임 오브젝트에서 playerController 컴포넌트 가져오기

            if (Target_Move != null)       //상대방으로 부터 PlayerController 컴포넌트를 가져오는 데 성공했다면
            {
                Target_Move.Die();         //상대방 PlayerController 컴포넌트 Die() 매서드실행
            }
        }
    }
}