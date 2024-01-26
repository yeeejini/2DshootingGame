using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum BulletType  // �Ѿ� Ÿ�Կ� ���� ������(����� ����ϱ� ���� �ϳ��� �̸����� �׷�ȭ �ϴ� ��)
{
    Main,
    Sub,
    Pet
}
public class Bullet : MonoBehaviour
{
    //public int BType = 0;   // 0�̸� ���Ѿ�, 1�̸� �����Ѿ�, 2�� ���� ��� �Ѿ�
    public BulletType BType = BulletType.Main;
    

    // ��ǥ : �Ѿ��� ���� ��� �̵��ϰ� �ʹ�.
    // �Ӽ� :
    //   - �ӷ�
    // ���� ����
    // 1. �̵��� ������ ���Ѵ�.
    // 2. �̵��Ѵ�.

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
        // 1. �̵��� ������ ���Ѵ�.
        Vector2 bullet = Vector2.up;

        // 2. �̵��Ѵ�.
        //transform.Translate(dir * Speed * Time.deltaTime);
        // ���ο� ��ġ = ������ġ * �ӵ� * �ð�
        transform.position += (Vector3)(bullet * Speed) * Time.deltaTime;

       
        
        // ���� �Ѿ�
        Vector2 subbullet = Vector2.up;

        transform.position += (Vector3)(subbullet * Speed) * Time.deltaTime;

    }
}
