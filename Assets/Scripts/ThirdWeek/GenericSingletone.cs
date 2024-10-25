using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericSingletone<T> : MonoBehaviour where T : Component
{
    private static T _instance;

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<T>();
                if (_instance == null)
                {
                    GameObject obj = new GameObject();  //객체 만들고
                    obj.name = typeof(T).Name;          //오브젝트 이름 설정
                    _instance = obj.AddComponent<T>();  //인스턴스 컨퍼넌트를 붙인다.
                }
            }
            return _instance;
        }
    }
    public virtual void Awake()                                //Awake가 실행될때
    {
        Debug.Log("디스트로이 방지 전");
        if (_instance == null)                           //해당 인스턴스가 존재하지 않으면
        {
            _instance = this as T;                            //인스턴스는 이 클래스 -> class Singleton
            DontDestroyOnLoad(gameObject);              //유니티에 파괴되지 않는 객체로 등록
            Debug.Log("디스트로이 방지");
        }
        else if (_instance != this)
        {
            Debug.Log("디스트로이 방지 안됨");
            Destroy(gameObject);                                //해당 인스턴스가 존재하면 생성하자마자 파괴된다.
        }
    }
}
