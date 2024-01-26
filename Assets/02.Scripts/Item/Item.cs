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
    public float _timer = 0f; // �ð��� üũ�� ����
    public float _timer2 = 0f;

    public ItemType MyType;    // 0: ü���� �÷��ִ� Ÿ��, 1: ���ǵ带 �÷��ִ� Ÿ��

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

    // (�ٸ� �ݶ��̴��� ����) Ʈ���Ű� �ߵ��� ��
    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        Debug.Log("Ʈ���� ����");

        // Destroy(this.gameObject);

        // 1. �÷��̾� ��ũ��Ʈ �޾ƿ���
        // GameObject.playerGameObject = GameObject.Find("Player");
        // Player player = playerGameObject.GetComponent<Player>();
        Player player = otherCollider.gameObject.GetComponent<Player>();

        // 2. �÷��̾� ü�� �ø���
        player.Health++;
        Debug.Log($"�÷��̾� ü�� : {player.Health}");
        
    }

    // (�ٸ� �ݶ��̴��� ����) Ʈ���Ű� �ߵ� ���� ��
    private void OnTriggerStay2D(Collider2D otherCollider)
    {
        _timer += Time.deltaTime;
        //if( _timer >= 1f ) 
        //{
            if (MyType == ItemType.Health)
            {
                Player player = otherCollider.GetComponent<Player>();
                player.Health += 1;

                Destroy(this.gameObject);

                GameObject vfx = Instantiate(ExplosionVFX_HealthPrefab);
                vfx.transform.position = otherCollider.transform.position;
            }
            else if (MyType == ItemType.Speed)
            {
                // Ÿ���� 1�̸� �÷��̾��� ���ǵ� �÷��ֱ�
                PlayerMove playermove = otherCollider.GetComponent<PlayerMove>();
                playermove.Speed += 1;

                Destroy(this.gameObject);

                GameObject vfx = Instantiate(ExplosionVFX_SpeedPrefab);
                vfx.transform.position = otherCollider.transform.position;
            }
            
            ItemSource.Play();
            
        //}
        
        // Debug.Log("Ʈ���� ��");
    }

    // (�ٸ� �ݶ��̴��� ����) Ʈ���Ű� ������ ��
    private void OnTriggerExit2D(Collider2D otherCollider)
    {
        _timer = 0f;
        Debug.Log("Ʈ���� ����");
    }

    // �������� ������ �ִٰ�, 3�� ���� �� ������ �÷��̾�� �ٰ�����
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
