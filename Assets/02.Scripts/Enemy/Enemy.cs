using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;
using static UnityEditor.Progress;
using static UnityEngine.GraphicsBuffer;
public enum EnemyType // �� Ÿ�� ������
{
    Basic,
    Target,
    Follow,
}
public class Enemy : MonoBehaviour
{
    // ��ǥ : ���� �Ʒ��� �̵���Ű�� �ʹ�.
    // �Ӽ� : 
    //  - �ӷ�
    
    public float Speed = 2.5f;
    public int Health = 2;

    public GameObject Item_HealthPrefab;
    public GameObject Item_SpeedPrefab;

    public AudioSource EnemySource;

    // ��ǥ : 
    // EnemyType.BasicŸ�� : �Ʒ��� �̵� 
    // EnemyType.Target Ÿ�� : ó�� �¾�� �� �÷��̾ �ִ� �������� �̵�
    // �Ӽ�
    //  - EnemyType Ÿ��
    // ���� ���� :
    // 1. ������ �� ������ ���Ѵ�. ( �÷��̾ �ִ� ���� )
    // 2. ������ ���� �̵��Ѵ�.

    public EnemyType EType;

    private Vector2 _dir;

    private GameObject _target;

    public Animator MyAnimator;

    public GameObject ExplosionVFXPrefab;



    // ������ ��
    void Start()
    {
        MyAnimator = GetComponent<Animator>();

        GameObject I = GameObject.Find("SoundController_Enemy");
        EnemySource = I.GetComponent<AudioSource>();

        // ĳ�� : ���� ���� �����͸� �� ����� ��ҿ� �����صΰ� �ʿ��Ҷ� ������ ���� ��
        // ������ �� �÷��̾ ã�Ƽ� ����صд�.
        _target = GameObject.Find("Player");

        if (EType == EnemyType.Target)
        {
            // 1.������ �� ������ ���Ѵ�. (�÷��̾ �ִ� ����)

            // 1-1. �÷��̾ ã�´�.
            // GameObject target = GameObject.Find("Player"); -> _target = GameObject.Find("Player");
            // GameObject.FindGameObjectWithTag("Player");

            // 1-2. ������ ���Ѵ�. ( target - me )
            _dir = _target.transform.position - this.transform.position;
            _dir.Normalize();

            // 1. ������ ���Ѵ�.
            // tan@ = y/x   -> @ = y/x*atan
            float radian = Mathf.Atan2(_dir.y, _dir.x);   // * Mathf.Rad2Deg;
            Debug.Log(radian);  // ȣ���� -> ���� ��

            float degree = radian * Mathf.Rad2Deg;
            Debug.Log(degree);

            // 2. ������ �°� ȸ���Ѵ�.
            // transform.rotation = Quaternion.Euler(new Vector3(0, 0, degree + 90)); // �̹��� ���ҽ��� �°� 90���� ���Ѵ�.
            transform.eulerAngles = new Vector3(0, 0, degree + 90);


        }
        else if (EType == EnemyType.Basic)
        {
            _dir = Vector2.down;
        }
    }

    void Update()
    {
        if (EType == EnemyType.Follow)
        {
            _dir = _target.transform.position - this.transform.position;
            _dir.Normalize();

            // 1. ������ ���Ѵ�.
            // tan@ = y/x   -> @ = y/x*atan
            float radian = Mathf.Atan2(_dir.y, _dir.x);   // * Mathf.Rad2Deg;
            Debug.Log(radian);  // ȣ���� -> ���� ��

            float degree = radian * Mathf.Rad2Deg;
            Debug.Log(degree);

            // 2. ������ �°� ȸ���Ѵ�.
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, degree + 90)); // �̹��� ���ҽ��� �°� 90���� ���Ѵ�.
        }
        // ���� ����
        // 1. ���� ���ϱ�
        // Vector2 dir = Vector2.down;

        // 2. �̵� ��Ų��.
        transform.position += (Vector3)(_dir * Speed) * Time.deltaTime;
    }



    // ��ǥ : �浹�ϸ� ���� �÷��̾ �����ϰ� �ʹ�.
    // ���� ���� : 
    // 1. ���࿡ �浹�� �Ͼ��
    // 2. ���� �÷��̾ �����Ѵ�.


    
    // �浹�� �Ͼ�� ȣ��Ǵ� �̺�Ʈ �Լ�
    private void OnCollisionEnter2D(Collision2D collision) 
    {


        // �浹�� �������� ��
        Debug.Log("Enter");

        // �浹�� ���� ������Ʈ�� �±׸� Ȯ��
        Debug.Log(collision.collider.tag);     // Player or Bullet

        

        // ���� �浹, Health�� 0�� �Ǹ� ����
        if (collision.collider.tag == "Player")
        {
            
            Player player = collision.collider.GetComponent<Player>();
            

            player.Health--;

            


            if (0 >= player.Health)
            {
                
                Destroy(collision.collider.gameObject);
            }

            
            Destroy(gameObject);


        }
        // �� �Ѿ��� ���� 1��, ���� �Ѿ��� ���� 2�� ������ ����
        else if (collision.collider.tag == "Bullet") 
        {
            Bullet bullet = collision.collider.GetComponent<Bullet>();

            

            if (bullet.BType == BulletType.Main)
            {
                Health -= 2;
            }                      
            else if (bullet.BType == BulletType.Sub) 
            {
                Health -= 1;
            }
            

            if (Health <= 0)
            {
                // ������

                EnemySource.Play();
                Death();

                MakeItem();

            }
            else 
            {
                MyAnimator.Play("Hit");
                
                
               
            }
           
            // ���� if�� ���� ��
            /**switch (bullet.BType)
            {
                case BulletType.Main:
                {
                    Health -= 2;
                    break;
                }
                case BulletType.Sub:
                {
                    Health -= 1;
                    break;
                }
            }**/


        }


        // 2. �浹�� �ʿ� ���� �����Ѵ�.
        // �� �װ�
        // Destroy(collision.collider.gameObject);
        // �� ����
        // Destroy(gameObject);





        
    }



    private void OnCollisionStay2D(Collision2D collision)
    {
        // �浹 ���� �� �Ź�
        Debug.Log("Stay");

        
    }



    private void OnCollisionExit2D(Collision2D collision)
    {
        // �浹�� ������ ��
        Debug.Log("Exit");
    }

    public void Death()
    {
        Destroy(gameObject);
        GameObject vfx = Instantiate(ExplosionVFXPrefab);
        vfx.transform.position = this.transform.position;

        // ��ǥ : ���ھ ������Ű�� �ʹ�.
        // 1. ������ ScoreManager ���� ������Ʈ�� ã�ƿ´�.
        GameObject smGameObject = GameObject.Find("ScoreManager");
        // 2. ScoreManager ���� ������Ʈ���� ScoreManager ��ũ��Ʈ ������Ʈ�� ���´�.
        ScoreManager scoreManager = smGameObject.GetComponent<ScoreManager>();
        // 3. ������Ʈ�� Score �Ӽ��� ������Ų��.
        scoreManager.Score += 1;
        Debug.Log($"���ھ�:{scoreManager.Score}");

        // 4. ���ھ ȭ�鿡 ǥ���Ѵ�.
        scoreManager.ScoreTextUI.text = $"���� : {scoreManager.Score}";

        // ��ǥ : �ְ� ������ �����ϰ� UI�� ǥ���ϰ� �ʹ�.
        // 1. ���࿡ ���� ������ �ְ� �������� ũ�ٸ�
        if (scoreManager.Score > scoreManager.BestScore) 
        {
            // 2. �ְ� ������ �����ϰ�,
            scoreManager.BestScore = scoreManager.Score;

            // ��ǥ : �ְ� ������ ����
            // 'PlayerPrefs' Ŭ������ ���
            // -> �����͸� 'Ű(key)'�� '��(Value)' ���·� �����ϴ� Ŭ�����Դϴ�.
            // ������ �� �ִ� ������ Ÿ�� : int, float, string
            // Ÿ�Ժ��� ����/�ε尡 ������ Set/Get �޼��尡 �ִ�.
            PlayerPrefs.SetInt("BestScore", scoreManager.BestScore);


            // 3. UI�� ǥ���Ѵ�.
            scoreManager.BestScoreTextUI.text = $"�ְ� ���� : {scoreManager.BestScore}";
        }
    }
    public void MakeItem() 
    {
        // ��ǥ : 50% Ȯ���� ü�� �÷��ִ� ������, 50% Ȯ���� �̵��ӵ� �÷��ִ� ������
        if (Random.Range(0, 2) == 0)
        {
            // - ü�� �÷��ִ� ������ �����
            GameObject item = Instantiate(Item_HealthPrefab);
            // - ��ġ�� ���� ��ġ�� ����
            item.transform.position = this.transform.position;
        }
        else
        {
            // -�̵��ӵ� �÷��ִ� ������ �����
            GameObject item = Instantiate(Item_SpeedPrefab);
            // - ��ġ�� ���� ��ġ�� ����
            item.transform.position = this.transform.position;
        }
    }
}
