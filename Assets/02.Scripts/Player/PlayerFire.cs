using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    // [ 총알 발사 제작 ]
    // 목표 : 총알을 만들어서 발사하고 싶다.
    // 속성 :
    //  - 총알
    //  - 총구 위치
    // 구현 순서
    // 1. 발사 버튼을 누르면
    // 2. 프리팹으로부터 총알을 동적으로 만들고,
    // 3. 만든 총알의 위치를 총구의 위치로 바꾼다.


    [Header("총알 프리팹")]
    public GameObject BulletPrefab;  // 총알 프리팹


    // 목표 : 태어날 때 풀에다가 총알을 (풀 사이즈)개 생성한다.
    // 속성 : 
    // - 풀 사이즈(몇 개 생성할거냐)
    public int PoolSize = 10;
    // - 오브젝트(총알) 풀
    public List<GameObject> _bulletPool = null;
    // 순서 : 
    // 1. 태어날 때 : Awake
    private void Awake()
    {
        // 2. 오브젝트 풀 선언(할당)해주고,
        _bulletPool = new List<GameObject>();

        // 3. 총알 프리팹으로부터 총알을 풀 사이즈만큼 생성해준다.
        for (int i = 0; i < PoolSize; i++) 
        {
            GameObject bullet = Instantiate(BulletPrefab);
            bullet.SetActive(false);  // 끈다.


            // 4. 생성한 총알을 풀에다가 넣는다.
            _bulletPool.Add(bullet);
        }
    }





    [Header("총구들")]
    // public GameObject[] Muzzles;  // 총구들
    public List<GameObject> Muzzles; // List로 변환


    [Header("타이머")]
    public float Timer = 10f;
    public const float COOL_TIME = 0.3f;

    [Header("자동 모드")]
    public bool AutoMode = false;

    public AudioSource FireSource;

    [Header("필살기")]
    public GameObject BoomPrefab;
    public float boomTimer = 0f;
    public const float BOOM_COOLTIME = 5f;
    

    Vector3 a = new Vector3(0, 0, 0);

    private void Start() 
    {
       
        Timer = 0f;
        AutoMode = false;
        boomTimer = 0f;
    }
    

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log("자동 공격 모드");
            AutoMode = true;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2)) 
        {
            Debug.Log("수동 공격 모드");
            AutoMode = false;
        }
        // 타이머 계산
        Timer -= Time.deltaTime;


        // 1. 타이머가 0보다 작은 상태에서 오토모드이거나 스페이스를 누르면 (발사 버튼을 누르면)
        bool ready = (AutoMode || Input.GetKey(KeyCode.Space));
        if (Timer <= 0 && ready)
        {
            // 총알 사운드
            FireSource.Play();

            // 타이머 초기화
            Timer = COOL_TIME;

            



            // 2. 프리팹으로부터 총알을 동적으로 만들고,
            // GameObject bullet1 = Instantiate(BulletPrefab);
            // GameObject bullet2 = Instantiate(BulletPrefab);

            // 3. 만든 총알의 위치를 총구의 위치로 바꾼다.
            // bullet1.transform.position = Muzzle.transform.position;
            // bullet2.transform.position = Muzzle2.transform.position;



            // 목표 : 총구 개수 만큼 총알을 만들고, 만든 총알의 위치를 각 총구의 위치로 바꾼다.
            /**for (int i = 0; i < Muzzles.Length; i++) 
            {
                // 1. 총알을 만들고
                GameObject bullet = Instantiate(BulletPrefab);

                // 2. 위치를 선정한다.
                bullet.transform.position = Muzzles[i].transform.position;
            }**/



            //  PlayerFire.cs에 있는 배열 ->  배열에서 List로 바꾸기
            /**for (int i = 0; i < Muzzles.Count; i++) 
            {
                GameObject bullet = Instantiate(BulletPrefab);

                bullet.transform.position = Muzzles[i].transform.position;
            }**/



            // 목표 : 총구 개수 만큼 총알을 풀에서 꺼내쓴다.
            // 순서 :
            // 1. 꺼져있는 총알을 꺼낸다.
            // 2. 꺼낸 총알의 위치를 각 총구의 위치로 바꾼다.
            // 3. 총알을 킨다. (발사한다.)
            for (int i = 0; i < Muzzles.Count; i++)
            {
                // 1. 꺼져있는 총알을 찾아 꺼낸다.
                GameObject bullet = null;
                foreach (GameObject b in _bulletPool) // 풀 찾기
                {
                    // 만약에 꺼져(비활성화) 있다면.
                    if (b.activeInHierarchy == false) 
                    {
                        bullet = b;
                        break;  // 찾았기 때문에 그 뒤까지 찾을 필요가 없다.
                    }
                }
                // 2. 꺼낸 총알의 위치를 각 총구의 위치로 바꾼다.
                bullet.transform.position = Muzzles[i].transform.position;


                // 3. 총알을 킨다. (발사한다.)
                bullet.SetActive(true);


            }
        }

        boomTimer -= Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Alpha3) && boomTimer <= 0) 
        {
            boomTimer = BOOM_COOLTIME;

            GameObject boom = Instantiate(BoomPrefab);
            boom.transform.position = a;
        }
    }
}
