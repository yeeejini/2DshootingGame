using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum BulletType  // 총알 타입에 대한 열거형(상수를 기억하기 좋게 하나의 이름으로 그룹화 하는 것)
{
    Main,
    Sub,
    Pet
}
public class Bullet : MonoBehaviour
{
    //public int BType = 0;   // 0이면 주총알, 1이면 보조총알, 2면 펫이 쏘는 총알
    public BulletType BType = BulletType.Main;
    

    // 목표 : 총알이 위로 계속 이동하고 싶다.
    // 속성 :
    //   - 속력
    // 구현 순서
    // 1. 이동할 방향을 구한다.
    // 2. 이동한다.

    public float Speed;

    


    void Start()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision) 
    {
        if (collision.collider.tag == "Enemy") 
        {
            Destroy(gameObject);
        }
        

    }
    void Update()
    {
        // 1. 이동할 방향을 구한다.
        Vector2 bullet = Vector2.up;

        // 2. 이동한다.
        //transform.Translate(dir * Speed * Time.deltaTime);
        // 새로운 위치 = 현재위치 * 속도 * 시간
        transform.position += (Vector3)(bullet * Speed) * Time.deltaTime;

       
        
        // 보조 총알
        Vector2 subbullet = Vector2.up;

        transform.position += (Vector3)(subbullet * Speed) * Time.deltaTime;

    }
}
