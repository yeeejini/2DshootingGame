using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    // [ �Ѿ� �߻� ���� ]
    // ��ǥ : �Ѿ��� ���� �߻��ϰ� �ʹ�.
    // �Ӽ� :
    //  - �Ѿ�
    //  - �ѱ� ��ġ
    // ���� ����
    // 1. �߻� ��ư�� ������
    // 2. ���������κ��� �Ѿ��� �������� �����,
    // 3. ���� �Ѿ��� ��ġ�� �ѱ��� ��ġ�� �ٲ۴�.


    [Header("�Ѿ� ������")]
    public GameObject BulletPrefab;  // �Ѿ� ������
    [Header("�ѱ���")]
    public GameObject[] Muzzles;  // �ѱ���

    
    [Header("Ÿ�̸�")]
    public float Timer = 10f;
    public const float COOL_TIME = 0.3f;

    [Header("�ڵ� ���")]
    public bool AutoMode = false;

    public AudioSource FireSource;

    [Header("�ʻ��")]
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
            Debug.Log("�ڵ� ���� ���");
            AutoMode = true;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2)) 
        {
            Debug.Log("���� ���� ���");
            AutoMode = false;
        }
        // Ÿ�̸� ���
        Timer -= Time.deltaTime;


        // 1. Ÿ�̸Ӱ� 0���� ���� ���¿��� �������̰ų� �����̽��� ������ (�߻� ��ư�� ������)
        bool ready = (AutoMode || Input.GetKey(KeyCode.Space));
        if (Timer <= 0 && ready)
        {
            // �Ѿ� ����
            FireSource.Play();

            // Ÿ�̸� �ʱ�ȭ
            Timer = COOL_TIME;
            
            // 2. ���������κ��� �Ѿ��� �������� �����,
            // GameObject bullet1 = Instantiate(BulletPrefab);
            // GameObject bullet2 = Instantiate(BulletPrefab);

            // 3. ���� �Ѿ��� ��ġ�� �ѱ��� ��ġ�� �ٲ۴�.
            // bullet1.transform.position = Muzzle.transform.position;
            // bullet2.transform.position = Muzzle2.transform.position;

            // ��ǥ : �ѱ� ���� ��ŭ �Ѿ��� �����, ���� �Ѿ��� ��ġ�� �� �ѱ��� ��ġ�� �ٲ۴�.
            for (int i = 0; i < Muzzles.Length; i++) 
            {
                // 1. �Ѿ��� �����
                GameObject bullet = Instantiate(BulletPrefab);

                // 2. ��ġ�� �����Ѵ�.
                bullet.transform.position = Muzzles[i].transform.position;
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
