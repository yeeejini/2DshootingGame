using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubBullet : MonoBehaviour
{
    
    // ��ǥ : ���� �Ѿ� ���ʿ� 2�� �߻��ϱ�
    

    [Header("���� �Ѿ� ������")]
    public GameObject SubBulletPrefab; 
    [Header("���� �Ѿ�")]
    public GameObject[] SubMuzzle; 


    [Header("Ÿ�̸�")]
    public float Timer = 10f;
    public const float COOL_TIME = 2f;

    [Header("�ڵ� ���")]
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
            Debug.Log("�ڵ� ���� ���");
            AutoMode = true;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Debug.Log("���� ���� ���");
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
