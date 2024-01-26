using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // ���� : �����ð����� ���� ���������κ��� �����ؼ� �� ��ġ�� ���� ���� �ʹ�.
    // �ʿ� �Ӽ�
    //    - �� ������
    //    - �����ð�
    //    - ����ð�
    // �������� : 
    // 1. �ð��� �帣�ٰ�
    // 2. ���࿡ �ð��� �����ð��� �Ǹ�
    // 3. ���������κ��� ���� �����Ѵ�.
    // 4. ������ ���� ��ġ�� �� ��ġ�� �ٲ۴�.
    // 1�ʸ��� ���� ����


    public GameObject EnemyPrefabsBasic;
    public GameObject EnemyPrefabsTarget;
    public GameObject EnemyPrefabsFollow;

    public GameObject EnemySpawn;

    [Header("Ÿ�̸�")]
    public float Timer = 1f;
    float Enemyrate;
    


    // ��ǥ : �� ���� �ð��� �����ϰ� �ϰ� �ʹ�.
    // �ʿ� �Ӽ� : 
    //     - �ּ� �ð�
    //     - �ִ� �ð�
    public float MinTime = 0.5f;
    public float MaxTime = 1.5f;


   
    private void Start()
    {
        // ������ �� �� ���� �ð��� �����ϰ� �����Ѵ�.
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
