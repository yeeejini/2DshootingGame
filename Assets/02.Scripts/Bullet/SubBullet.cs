using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubBullet : MonoBehaviour
{
    
    // 목표 : 보조 총알 양쪽에 2개 발사하기
    

    [Header("보조 총알 프리팹")]
    public GameObject SubBulletPrefab; 
    [Header("보조 총알")]
    // public GameObject[] SubMuzzle;
    public List<GameObject> SubMuzzle;



    // 태어날 때 풀에다가 총알을 (풀 사이즈)에 생성한다.
    public int PoolSize = 6;
    
    public List<GameObject> _subbulletPool = null;
    
    private void Awake()
    {
        _subbulletPool = new List<GameObject>();
        
        for (int i = 0; i < PoolSize; i++)
        {
            GameObject bullet = Instantiate(SubBulletPrefab);
            bullet.SetActive(false); 

            _subbulletPool.Add(bullet);
        }
    }




    [Header("타이머")]
    public float Timer = 10f;
    public const float COOL_TIME = 2f;

    [Header("자동 모드")]
    public bool AutoMode = false;



    private void Start()
    {
        Timer = 0f;
        AutoMode = false;
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


        Timer -= Time.deltaTime;


        bool ready = (AutoMode || Input.GetKey(KeyCode.Space));
        if (Timer <= 0 && ready)
        {

            Timer = COOL_TIME;

            // 총구 개수 만큼 총알을 풀에서 꺼내쓴다.
            for (int i = 0; i < SubMuzzle.Count; i++)
            {
                GameObject subbullet = null;
                foreach (GameObject s in _subbulletPool) 
                {
                    if (s.activeInHierarchy == false) 
                    {
                        subbullet = s;
                        break;
                    }
                }
                subbullet.transform.position = SubMuzzle[i].transform.position;

                subbullet.SetActive(true);
            }
        }
    }
}
