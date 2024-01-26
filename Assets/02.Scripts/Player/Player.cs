using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int Health = 3;

    public AudioSource PlayerSource;

    private void OnCollisionEnter2D(Collision2D collision)
    {

        //플레이어와의 충돌 체크
        if (collision.collider.tag == "Enemy")
        {
            PlayerSource.Play();
        }
        
    }
    private void Start()
    {
        GameObject P = GameObject.Find("SoundController_Player");
        PlayerSource = P.GetComponent<AudioSource>();

        // GetComponent<컴포넌트 타입>(); -> 게임 오브젝트의 컴포넌트를 가져오는 메서드

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
}
