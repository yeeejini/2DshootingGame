using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private int _health = 3;

    public AudioSource PlayerSource;

    private void OnCollisionEnter2D(Collision2D collision)
    {

        //�÷��̾���� �浹 üũ
        if (collision.collider.tag == "Enemy")
        {
            PlayerSource.Play();
        }
        
    }
    private void Start()
    {
        GameObject P = GameObject.Find("SoundController_Player");
        PlayerSource = P.GetComponent<AudioSource>();

        // GetComponent<������Ʈ Ÿ��>(); -> ���� ������Ʈ�� ������Ʈ�� �������� �޼���

        // SpriteRenderer sr = GetComponent<SpriteRenderer>();
        // sr.color = Color.white;

        // Transform tr = GetComponent<Transform>();
        // tr.position = new Vector2(0f, -2,7f);
        // transform.position = new Vector2(0f, -2, 7f);

        // PlayerMove playerMove = GetComponent<PlayerMove>();
        // Debug.Log(playerMove.Speed);
        // playerMove.Speed = 5f;
        // Debug.Log(playerMove.Speed);
    }

    public int GetHealth() 
    {
        return _health;
    }
    public void SetHealth(int health) 
    {
        _health = health;
    }
    
}
