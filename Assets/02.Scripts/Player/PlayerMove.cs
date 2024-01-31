using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    /** 
      목표 : 플레이어를 이동하고 싶다
      필요 속성 : 
         - 이동 속도
      순서 :
         1. 키보드 입력을 받는다.
         2. 키보드 입력에 따라 이동할 방향을 계산한다.
         3. 이동할 방향과 이동 속도에 따라 플레이어를 이동시킨다.
    **/

    private float Speed = 3f;  // 이동 속도 : 초당 3만큼 이동하겠다.
    
    
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
        // deltaTime은 프레임 간 시간 간격을 반환한다.
        // 30fps : d -> 0.03초
        // 60fps : d -> 0.016초


        // 1. 키보드 입력을 받는다.
        // float h = Input.GetAxis("Horizontal");  // -1.0f ~ 0f ~ +1.0f  (수평)
        // float v = Input.GetAxis("Vertical");    // 수직 입력값을 받아온다.(수직)
        float h = Input.GetAxisRaw("Horizontal");  // -1.0f,  0f,  +1.0f  (수평)
        float v = Input.GetAxisRaw("Vertical");    // 수직 입력값을 받아온다.(수직)
        // Debug.Log($"h: {h}, v: {v}");



        // 애니메니티에게 파라미터 값을 넘겨준다.
        MyAnimator.SetInteger("h", (int)h);



        // 2. 키보드 입력에 따라 이동할 방향을 계산한다.
        // Vector2 dir = Vector2.right * h + Vector2.up * v;   // (1, 0) * h + (0, 1) * v = (h, v)

        // 방향을 각 성분으로 제작
        Vector2 dir = new Vector2(h, v);
        // Debug.Log($"정규화 전: {dir.magnitude}");

        // 이동 방향을 정규화 (방향은 같지만 길이는 1로 만들어줌)
        dir = dir.normalized;
        // Debug.Log($"정규화 후: {dir.magnitude}");

        // 3. 이동할 방향과 이동 속도에 따라 플레이어를 이동시킨다.
        // Debug.Log(Time.deltaTime);
        // transform.Translate(dir * Speed * Time.deltaTime); // Translate -> 보고있는 방향으로 움직임

        // 공식을 이용한 이동
        // 새로운 위치 = 현재 위치 + 속도 * 시간
        Vector2 newPosition = transform.position + (Vector3)(dir * Speed) * Time.deltaTime;

        /** 
        새로운 위치를 잘 수정해본다. 
        **/
        // Debug.Log($"x: {newPosition.x}, y:{newPosition.y}");


        // 좌표 밖으로 나가지 않게
        /**if (newPosition.x < MinX) 
        {
            newPosition.x = MinX;
        }
        
        if (newPosition.x > MaxX) 
        {
            newPosition.x = MaxX;
        }**/

        // 한쪽으로 이동하면 반대편으로 나오는거
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

        transform.position = newPosition; // 플레이어의 위치 = 새로운 위치



        // 현재 위치 출력
        // Debug.Log(transform.position); // 움직일 때마다 좌표 뜸
                                       // transform.position = new Vector2(0, 1); -> 이렇게 해도 되지만 매 프레임마다 실행됨

    }
    private void SpeedUpDown() 
    {
        // 키보드 E버튼을 누르면 스피드 1업 Q버튼 누르면 스피드 1다운
        // 속성
        // - 속력 (Speed)
        // 순서 : 
        // 1. Q / E 버튼 입력을 판단한다.
        // 2. Q 버튼이 눌렸다면 스피드 1다운
        // 3. E 버튼이 눌렸다면 스피드 1업
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
    public float GetSpeed() 
    {
        return Speed;
    }
    public void SetSpeed(float speed) 
    {
        Speed = speed;
    }
    public void AddSpeed(float speed) 
    {
        Speed += speed;
    }
}
