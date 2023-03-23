using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // 속도 수치 조정 (이동, 점프)
    public float speed = 5f; // 이동 속도 변수
    [SerializeField]
    float jumpPower = 10f; // Addforce에 곱해줄 점프력 변수

    // 입력 받기
    float hAxis; // 수평 입력 받기 (X축)
    float vAxis; // 수직 입력 받기 (Z축)
    [HideInInspector]
    public bool jDown; // 점프 GetButtonDown 입력 boolean 변수
    [HideInInspector]
    public bool isJump; // 점프 중인지 아닌지 (Tag가 Ground인 물체에 닿았는지 판단해 줄 변수)

    // 플레이어의 방향
    [HideInInspector]
    public Vector3 moveVec; // 이동 방향을 정규화 해줄 벡터 변수

    Vector3 dir;
    float rotateSpeed = 10.0f;


    // 점프 제어
    [HideInInspector]
    public Rigidbody rigid; // 리지드바디 컴포넌트를 불러올 변수

    void Awake()
    {
        rigid = GetComponent<Rigidbody>(); // 리지드바디 컴포넌트에 접근
    }

    // Update is called once per frame
    void Update()
    {
        // 실시간 동작될 함수
        GetInput(); // 입력 받기 모음 함수
        Move(); // 움직임 함수
        Turn(); // 플레이어의 방향 제어 함수
        Jump(); // 점프 구현 함수
    }

    // 입력 값을 받는 함수
    void GetInput()
    {
        hAxis = Input.GetAxisRaw("Horizontal"); // 수평 이동 키 값을 불러옴 (float X 값)
        vAxis = Input.GetAxisRaw("Vertical"); // 수직 이동 키 값을 불러옴 (float Z 값)
        jDown = Input.GetButtonDown("Jump"); // 스페이스바 키 값을 눌렀을 때 한 번 true반환 (bool)
    }

    // 움직임 제어 함수
    void Move()
    {
        Vector2 moveInput = new Vector2(hAxis, vAxis); // nomalized = 값을 전부 1로 정규화

        Vector3 lookForward = new Vector3(transform.forward.x, 0f, transform.forward.z).normalized;
        Vector3 lookRight = new Vector3(transform.right.x, 0f, transform.right.z).normalized;
        Vector3 moveVec = lookForward * moveInput.y + lookRight * moveInput.x;

        transform.position += moveVec * speed * Time.deltaTime; // 정규화 된 방향으로 키입력값 * speed수치만큼 실시간 이동
    }

    // 플레이어가 바라보는 방향 제어
    void Turn()
    {
        Vector3 cam = Camera.main.transform.position; // 카메라의 포지션

        dir = new Vector2(transform.position.x - cam.x, transform.position.z - cam.z).normalized; // 카메라가 바라보는 방향 계산

        // 회전하는 부분. Point 1.
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * rotateSpeed); // 카메라가 보는 방향으로 플레이어 회전

        transform.LookAt(dir); // 정규화된 방향으로 바라봄
    }

    // 플레이어의 점프를 구현할 함수
    void Jump()
    {
        // 만약 스페이스바를 눌렀고, 땅에 충돌되지 않았다면 (점프 상태가 false)
        if (jDown && !isJump)
        {
            rigid.AddForce(Vector3.up * jumpPower, ForceMode.Impulse); // 점프력만큼 y축으로 힘을 가해 줌
            isJump = true; // 점프 상태를 true로
        }
    }

    // 물체의 충돌 판정 (캡슐 콜라이더)
    void OnCollisionEnter(Collision collision)
    {
        // 부딪힌 물체의 태그가 Ground라면
        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Block")
        {
            isJump = false; // 점프 상태를 false로
        }
    }
}
