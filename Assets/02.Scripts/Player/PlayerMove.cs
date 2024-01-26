using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    /** 
      ��ǥ : �÷��̾ �̵��ϰ� �ʹ�
      �ʿ� �Ӽ� : 
         - �̵� �ӵ�
      ���� :
         1. Ű���� �Է��� �޴´�.
         2. Ű���� �Է¿� ���� �̵��� ������ ����Ѵ�.
         3. �̵��� ����� �̵� �ӵ��� ���� �÷��̾ �̵���Ų��.
    **/

    public float Speed = 3f;  // �̵� �ӵ� : �ʴ� 3��ŭ �̵��ϰڴ�.
    
    
    public const float MinX = -3f;
    public const float MaxX = 3f;
    public const float MinY = -6f;
    public const float MaxY = 0f;

    public Animator MyAnimator;

    private void Awake()
    {
        MyAnimator = this.gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        Move();
        SpeedUpDown();
    }

    private void Move() 
    {
        // transform.Translate(Vector2.up * Speed * Time.deltaTime);
        // (0, 1) * 3 -> (0, 3) * Time.deltaTime
        // deltaTime�� ������ �� �ð� ������ ��ȯ�Ѵ�.
        // 30fps : d -> 0.03��
        // 60fps : d -> 0.016��


        // 1. Ű���� �Է��� �޴´�.
        // float h = Input.GetAxis("Horizontal");  // -1.0f ~ 0f ~ +1.0f  (����)
        // float v = Input.GetAxis("Vertical");    // ���� �Է°��� �޾ƿ´�.(����)
        float h = Input.GetAxisRaw("Horizontal");  // -1.0f,  0f,  +1.0f  (����)
        float v = Input.GetAxisRaw("Vertical");    // ���� �Է°��� �޾ƿ´�.(����)
        // Debug.Log($"h: {h}, v: {v}");



        // �ִϸ޴�Ƽ���� �Ķ���� ���� �Ѱ��ش�.
        MyAnimator.SetInteger("h", (int)h);



        // 2. Ű���� �Է¿� ���� �̵��� ������ ����Ѵ�.
        // Vector2 dir = Vector2.right * h + Vector2.up * v;   // (1, 0) * h + (0, 1) * v = (h, v)

        // ������ �� �������� ����
        Vector2 dir = new Vector2(h, v);
        // Debug.Log($"����ȭ ��: {dir.magnitude}");

        // �̵� ������ ����ȭ (������ ������ ���̴� 1�� �������)
        dir = dir.normalized;
        // Debug.Log($"����ȭ ��: {dir.magnitude}");

        // 3. �̵��� ����� �̵� �ӵ��� ���� �÷��̾ �̵���Ų��.
        // Debug.Log(Time.deltaTime);
        // transform.Translate(dir * Speed * Time.deltaTime); // Translate -> �����ִ� �������� ������

        // ������ �̿��� �̵�
        // ���ο� ��ġ = ���� ��ġ + �ӵ� * �ð�
        Vector2 newPosition = transform.position + (Vector3)(dir * Speed) * Time.deltaTime;

        /** 
        ���ο� ��ġ�� �� �����غ���. 
        **/
        // Debug.Log($"x: {newPosition.x}, y:{newPosition.y}");


        // ��ǥ ������ ������ �ʰ�
        /**if (newPosition.x < MinX) 
        {
            newPosition.x = MinX;
        }
        
        if (newPosition.x > MaxX) 
        {
            newPosition.x = MaxX;
        }**/

        // �������� �̵��ϸ� �ݴ������� �����°�
        if (newPosition.x < MinX)
        {
            newPosition.x = MaxX;
        }
        else if (newPosition.x > MaxX)
        {
            newPosition.x = MinX;
        }



        // newPosition.y = Mathf.Max(MinY, newPosition.y);
        // newPosition.y = Mathf.Max(newPosition.y, MaxY);

        // newPosition.y = Mathf.Clamp(newPosition.y, MinY, MaxY);    // Clamp


        if (newPosition.y < MinY) 
        {
            newPosition.y = MinY;
        }
        
        if (newPosition.y > MaxY) 
        {
            newPosition.y = MaxY;
        }

        transform.position = newPosition; // �÷��̾��� ��ġ = ���ο� ��ġ



        // ���� ��ġ ���
        // Debug.Log(transform.position); // ������ ������ ��ǥ ��
                                       // transform.position = new Vector2(0, 1); -> �̷��� �ص� ������ �� �����Ӹ��� �����

    }
    private void SpeedUpDown() 
    {
        // Ű���� E��ư�� ������ ���ǵ� 1�� Q��ư ������ ���ǵ� 1�ٿ�
        // �Ӽ�
        // - �ӷ� (Speed)
        // ���� : 
        // 1. Q / E ��ư �Է��� �Ǵ��Ѵ�.
        // 2. Q ��ư�� ���ȴٸ� ���ǵ� 1�ٿ�
        // 3. E ��ư�� ���ȴٸ� ���ǵ� 1��
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Speed--;
        }
        Debug.Log(Speed);
        if (Input.GetKeyDown(KeyCode.E))
        {
            Speed++;
        }

    }
}
