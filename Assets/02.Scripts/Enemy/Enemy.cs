using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;
using static UnityEditor.Progress;
using static UnityEngine.GraphicsBuffer;
public enum EnemyType // 적 타입 열거형
{
    Basic,
    Target,
    Follow,
}
public class Enemy : MonoBehaviour
{
    // 목표 : 적을 아래로 이동시키고 싶다.
    // 속성 : 
    //  - 속력
    
    public float Speed = 2.5f;
    public int Health = 2;

    public GameObject Item_HealthPrefab;
    public GameObject Item_SpeedPrefab;

    public AudioSource EnemySource;

    // 목표 : 
    // EnemyType.Basic타입 : 아래로 이동 
    // EnemyType.Target 타입 : 처음 태어났을 때 플레이어가 있는 방향으로 이동
    // 속성
    //  - EnemyType 타입
    // 구현 순서 :
    // 1. 시작할 때 방향을 구한다. ( 플레이어가 있는 방향 )
    // 2. 방향을 향해 이동한다.

    public EnemyType EType;

    private Vector2 _dir;

    private GameObject _target;

    public Animator MyAnimator;

    public GameObject ExplosionVFXPrefab;



    // 시작할 때
    void Start()
    {
        MyAnimator = GetComponent<Animator>();

        GameObject I = GameObject.Find("SoundController_Enemy");
        EnemySource = I.GetComponent<AudioSource>();

        // 캐싱 : 자주 쓰는 데이터를 더 가까운 장소에 저장해두고 필요할때 가져다 쓰는 거
        // 시작할 때 플레이어를 찾아서 기억해둔다.
        _target = GameObject.Find("Player");

        if (EType == EnemyType.Target)
        {
            // 1.시작할 때 방향을 구한다. (플레이어가 있는 방향)

            // 1-1. 플레이어를 찾는다.
            // GameObject target = GameObject.Find("Player"); -> _target = GameObject.Find("Player");
            // GameObject.FindGameObjectWithTag("Player");

            // 1-2. 방향을 구한다. ( target - me )
            _dir = _target.transform.position - this.transform.position;
            _dir.Normalize();

            // 1. 각도를 구한다.
            // tan@ = y/x   -> @ = y/x*atan
            float radian = Mathf.Atan2(_dir.y, _dir.x);   // * Mathf.Rad2Deg;
            Debug.Log(radian);  // 호도법 -> 라디안 값

            float degree = radian * Mathf.Rad2Deg;
            Debug.Log(degree);

            // 2. 각도에 맞게 회전한다.
            // transform.rotation = Quaternion.Euler(new Vector3(0, 0, degree + 90)); // 이미지 리소스에 맞게 90도를 더한다.
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

            // 1. 각도를 구한다.
            // tan@ = y/x   -> @ = y/x*atan
            float radian = Mathf.Atan2(_dir.y, _dir.x);   // * Mathf.Rad2Deg;
            Debug.Log(radian);  // 호도법 -> 라디안 값

            float degree = radian * Mathf.Rad2Deg;
            Debug.Log(degree);

            // 2. 각도에 맞게 회전한다.
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, degree + 90)); // 이미지 리소스에 맞게 90도를 더한다.
        }
        // 구현 순서
        // 1. 방향 구하기
        // Vector2 dir = Vector2.down;

        // 2. 이동 시킨다.
        transform.position += (Vector3)(_dir * Speed) * Time.deltaTime;
    }



    // 목표 : 충돌하면 적과 플레이어를 삭제하고 싶다.
    // 구현 순서 : 
    // 1. 만약에 충돌이 일어나면
    // 2. 적과 플레이어를 삭제한다.


    
    // 충돌이 일어나면 호출되는 이벤트 함수
    private void OnCollisionEnter2D(Collision2D collision) 
    {


        // 충돌을 시작했을 때
        Debug.Log("Enter");

        // 충돌한 게임 오브젝트의 태그를 확인
        Debug.Log(collision.collider.tag);     // Player or Bullet

        

        // 적과 충돌, Health가 0이 되면 죽음
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
        // 주 총알은 적을 1번, 보조 총알은 적을 2번 때려야 죽음
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
                // 나죽자

                EnemySource.Play();
                Death();

                MakeItem();

            }
            else 
            {
                MyAnimator.Play("Hit");
                
                
               
            }
           
            // 위에 if랑 같은 거
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


        // 2. 충돌한 너와 나를 삭제한다.
        // 너 죽고
        // Destroy(collision.collider.gameObject);
        // 나 죽자
        // Destroy(gameObject);





        
    }



    private void OnCollisionStay2D(Collision2D collision)
    {
        // 충돌 중일 때 매번
        Debug.Log("Stay");

        
    }



    private void OnCollisionExit2D(Collision2D collision)
    {
        // 충돌이 끝났을 때
        Debug.Log("Exit");
    }

    public void Death()
    {
        Destroy(gameObject);
        GameObject vfx = Instantiate(ExplosionVFXPrefab);
        vfx.transform.position = this.transform.position;

        // 목표 : 스코어를 증가시키고 싶다.
        // 1. 씬에서 ScoreManager 게임 오브젝트를 찾아온다.
        GameObject smGameObject = GameObject.Find("ScoreManager");
        // 2. ScoreManager 게임 오브젝트에서 ScoreManager 스크립트 컴포넌트를 얻어온다.
        ScoreManager scoreManager = smGameObject.GetComponent<ScoreManager>();
        // 3. 컴포넌트의 Score 속성을 증가시킨다.
        scoreManager.Score += 1;
        Debug.Log($"스코어:{scoreManager.Score}");

        // 4. 스코어를 화면에 표시한다.
        scoreManager.ScoreTextUI.text = $"점수 : {scoreManager.Score}";

        // 목표 : 최고 점수를 갱신하고 UI에 표시하고 싶다.
        // 1. 만약에 현재 점수가 최고 점수보다 크다면
        if (scoreManager.Score > scoreManager.BestScore) 
        {
            // 2. 최고 점수를 갱신하고,
            scoreManager.BestScore = scoreManager.Score;

            // 목표 : 최고 점수를 저장
            // 'PlayerPrefs' 클래스를 사용
            // -> 데이터를 '키(key)'와 '값(Value)' 형태로 저장하는 클래스입니다.
            // 저장할 수 있는 데이터 타입 : int, float, string
            // 타입별로 저장/로드가 가능한 Set/Get 메서드가 있다.
            PlayerPrefs.SetInt("BestScore", scoreManager.BestScore);


            // 3. UI에 표시한다.
            scoreManager.BestScoreTextUI.text = $"최고 점수 : {scoreManager.BestScore}";
        }
    }
    public void MakeItem() 
    {
        // 목표 : 50% 확률로 체력 올려주는 아이템, 50% 확률로 이동속도 올려주는 아이템
        if (Random.Range(0, 2) == 0)
        {
            // - 체력 올려주는 아이템 만들고
            GameObject item = Instantiate(Item_HealthPrefab);
            // - 위치를 나의 위치로 수정
            item.transform.position = this.transform.position;
        }
        else
        {
            // -이동속도 올려주는 아이템 만들고
            GameObject item = Instantiate(Item_SpeedPrefab);
            // - 위치를 나의 위치로 수정
            item.transform.position = this.transform.position;
        }
    }
}
