using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // 역할 : 일정시간마다 적을 프리팹으로부터 생성해서 내 위치에 갖다 놓고 싶다.
    // 필요 속성
    //    - 적 프리팹
    //    - 일정시간
    //    - 현재시간
    // 구현순서 : 
    // 1. 시간이 흐르다가
    // 2. 만약에 시간이 일정시간이 되면
    // 3. 프리팹으로부터 적을 생성한다.
    // 4. 생성한 적의 위치를 내 위치로 바꾼다.
    // 1초마다 적이 생성


    public GameObject EnemyPrefabsBasic;
    public GameObject EnemyPrefabsTarget;
    public GameObject EnemyPrefabsFollow;

    public GameObject EnemySpawn;

    [Header("타이머")]
    public float Timer = 1f;
    float Enemyrate;
    


    // 목표 : 적 생성 시간을 랜덤하게 하고 싶다.
    // 필요 속성 : 
    //     - 최소 시간
    //     - 최대 시간
    public float MinTime = 0.5f;
    public float MaxTime = 1.5f;


   
    private void Start()
    {
        // 시작할 때 적 생성 시간을 랜덤하게 설정한다.
        SetRandomSpawnTime();

    }


    void Update()
    {
        Timer -= Time.deltaTime;

        if (Timer <= 0)
        {
            SetRandomRate();
            SetRandomSpawnTime();
        }
    }

    private void SetRandomSpawnTime()
    {
        Timer = Random.Range(MinTime, MaxTime);
    }
    private void SetRandomRate()
    {
        GameObject EnemyTarget;
        GameObject EnemyBasic;
        GameObject EnemyFollow;

        Enemyrate = Random.Range(0, 100);


        
        if (Enemyrate <= 30)
        {
            EnemyTarget = Instantiate(EnemyPrefabsTarget);

            EnemyTarget.transform.position = EnemySpawn.transform.position;
        }
        if (Enemyrate <= 10)
        {
            EnemyFollow = Instantiate(EnemyPrefabsFollow);

            EnemyFollow.transform.position = EnemySpawn.transform.position;
        }
        else 
        {
            EnemyBasic = Instantiate(EnemyPrefabsBasic);

            EnemyBasic.transform.position = EnemySpawn.transform.position;
        }
        

    }
}
