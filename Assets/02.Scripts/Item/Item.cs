using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public enum ItemType 
{
    Speed,
    Health,
}
public class Item : MonoBehaviour
{
    public float _timer = 0f; // 시간을 체크할 변수
    public float _timer2 = 0f;

    public ItemType MyType;    // 0: 체력을 올려주는 타입, 1: 스피드를 올려주는 타입

    public Animator MyAnimator;

    public AudioSource ItemSource;

    public GameObject ExplosionVFX_SpeedPrefab;
    public GameObject ExplosionVFX_HealthPrefab;

    private void Start()
    {
        MyAnimator = GetComponent<Animator>();

        MyAnimator.SetInteger("ItemType", (int)MyType);

        GameObject I = GameObject.Find("SoundController_Item");
        ItemSource = I.GetComponent<AudioSource>();

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Clooison Enter");
    }

    // (다른 콜라이더에 의해) 트리거가 발동할 때
    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        Debug.Log("트리거 시작");

        // Destroy(this.gameObject);

        // 1. 플레이어 스크립트 받아오기
        // GameObject.playerGameObject = GameObject.Find("Player");
        // Player player = playerGameObject.GetComponent<Player>();
        Player player = otherCollider.gameObject.GetComponent<Player>();

        // 2. 플레이어 체력 올리기
        player.SetHealth(player.GetHealth() + 1);
        Debug.Log($"플레이어 체력 : {player.GetHealth()}");
        
    }

    // (다른 콜라이더에 의해) 트리거가 발동 중일 때
    private void OnTriggerStay2D(Collider2D otherCollider)
    {
        _timer += Time.deltaTime;
        //if( _timer >= 1f ) 
        //{
            if (MyType == ItemType.Health)
            {
                Player player = otherCollider.GetComponent<Player>();
                player.SetHealth(player.GetHealth() + 1);

                Destroy(this.gameObject);

                GameObject vfx = Instantiate(ExplosionVFX_HealthPrefab);
                vfx.transform.position = otherCollider.transform.position;
            }
            else if (MyType == ItemType.Speed)
            {
                // 타입이 1이면 플레이어의 스피드 올려주기
                PlayerMove playermove = otherCollider.GetComponent<PlayerMove>();
                playermove.SetSpeed(playermove.GetSpeed() + 1);
                playermove.AddSpeed(1);

                Destroy(this.gameObject);

                GameObject vfx = Instantiate(ExplosionVFX_SpeedPrefab);
                vfx.transform.position = otherCollider.transform.position;
            }
            
            ItemSource.Play();
            
        //}
        
        // Debug.Log("트리거 중");
    }

    // (다른 콜라이더에 의해) 트리거가 끝났을 때
    private void OnTriggerExit2D(Collider2D otherCollider)
    {
        _timer = 0f;
        Debug.Log("트리거 종료");
    }

    // 아이템이 정지해 있다가, 3초 동안 안 먹으면 플레이어에게 다가가기
    private Vector2 _dir;
    // private GameObject _target;
    public float Speed;

    void ItemFollow() 
    {
        GameObject target = GameObject.Find("Player");

        _dir = target.transform.position - this.transform.position;
        _dir.Normalize();  

        transform.position += (Vector3)(_dir * Speed) * Time.deltaTime;
    }
    private void Update()
    {
        _timer2 += Time.deltaTime;
        if (_timer2 >= 3f) 
        {
            ItemFollow();
        }
    }
}
