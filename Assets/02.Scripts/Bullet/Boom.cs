using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boom : MonoBehaviour
{
    private const float DEATH_TIME = 3f;
    private float BoomTimer = 0f;

    void Start()
    {
        // FindGameObjectsWithTag -> 특정 태그를 가진 모든 게임 오브젝트 찾고 배열 형태로 반환
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        Debug.Log(enemies.Length);
        for (int i = 0; i < enemies.Length; i++) 
        {
            Enemy enemy = enemies[i].GetComponent<Enemy>();
            enemy.Death();
            enemy.MakeItem();
        }
    }
    private void Update()
    {
        BoomTimer += Time.deltaTime;
        if (BoomTimer >= DEATH_TIME)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Enemy enemy = collider.gameObject.GetComponent<Enemy>();

        if (collider.gameObject.tag == "Enemy") 
        {
            enemy.Death();
            enemy.MakeItem();
        }
    }

}
