using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubBullet : MonoBehaviour
{
    
    // 목표 : 보조 총알 양쪽에 2개 발사하기
    

    [Header("보조 총알 프리팹")]
    public GameObject SubBulletPrefab; 
    [Header("보조 총알")]
    public GameObject[] SubMuzzle; 


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

            for (int i = 0; i < SubMuzzle.Length; i++)
            {
                GameObject bullet = Instantiate(SubBulletPrefab);

                bullet.transform.position = SubMuzzle[i].transform.position;
            }


        }

    }

}
