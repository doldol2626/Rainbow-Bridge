using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // �ӵ� ��ġ ���� (�̵�, ����)
    public float speed = 5f; // �̵� �ӵ� ����
    [SerializeField]
    float jumpPower = 10f; // Addforce�� ������ ������ ����

    // �Է� �ޱ�
    float hAxis; // ���� �Է� �ޱ� (X��)
    float vAxis; // ���� �Է� �ޱ� (Z��)
    [HideInInspector]
    public bool jDown; // ���� GetButtonDown �Է� boolean ����
    [HideInInspector]
    public bool isJump; // ���� ������ �ƴ��� (Tag�� Ground�� ��ü�� ��Ҵ��� �Ǵ��� �� ����)

    // �÷��̾��� ����
    [HideInInspector]
    public Vector3 moveVec; // �̵� ������ ����ȭ ���� ���� ����

    Vector3 dir;
    float rotateSpeed = 10.0f;


    // ���� ����
    [HideInInspector]
    public Rigidbody rigid; // ������ٵ� ������Ʈ�� �ҷ��� ����

    void Awake()
    {
        rigid = GetComponent<Rigidbody>(); // ������ٵ� ������Ʈ�� ����
    }

    // Update is called once per frame
    void Update()
    {
        // �ǽð� ���۵� �Լ�
        GetInput(); // �Է� �ޱ� ���� �Լ�
        Move(); // ������ �Լ�
        Turn(); // �÷��̾��� ���� ���� �Լ�
        Jump(); // ���� ���� �Լ�
    }

    // �Է� ���� �޴� �Լ�
    void GetInput()
    {
        hAxis = Input.GetAxisRaw("Horizontal"); // ���� �̵� Ű ���� �ҷ��� (float X ��)
        vAxis = Input.GetAxisRaw("Vertical"); // ���� �̵� Ű ���� �ҷ��� (float Z ��)
        jDown = Input.GetButtonDown("Jump"); // �����̽��� Ű ���� ������ �� �� �� true��ȯ (bool)
    }

    // ������ ���� �Լ�
    void Move()
    {
        Vector2 moveInput = new Vector2(hAxis, vAxis); // nomalized = ���� ���� 1�� ����ȭ

        Vector3 lookForward = new Vector3(transform.forward.x, 0f, transform.forward.z).normalized;
        Vector3 lookRight = new Vector3(transform.right.x, 0f, transform.right.z).normalized;
        Vector3 moveVec = lookForward * moveInput.y + lookRight * moveInput.x;

        transform.position += moveVec * speed * Time.deltaTime; // ����ȭ �� �������� Ű�Է°� * speed��ġ��ŭ �ǽð� �̵�
    }

    // �÷��̾ �ٶ󺸴� ���� ����
    void Turn()
    {
        Vector3 cam = Camera.main.transform.position; // ī�޶��� ������

        dir = new Vector2(transform.position.x - cam.x, transform.position.z - cam.z).normalized; // ī�޶� �ٶ󺸴� ���� ���

        // ȸ���ϴ� �κ�. Point 1.
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * rotateSpeed); // ī�޶� ���� �������� �÷��̾� ȸ��

        transform.LookAt(dir); // ����ȭ�� �������� �ٶ�
    }

    // �÷��̾��� ������ ������ �Լ�
    void Jump()
    {
        // ���� �����̽��ٸ� ������, ���� �浹���� �ʾҴٸ� (���� ���°� false)
        if (jDown && !isJump)
        {
            rigid.AddForce(Vector3.up * jumpPower, ForceMode.Impulse); // �����¸�ŭ y������ ���� ���� ��
            isJump = true; // ���� ���¸� true��
        }
    }

    // ��ü�� �浹 ���� (ĸ�� �ݶ��̴�)
    void OnCollisionEnter(Collision collision)
    {
        // �ε��� ��ü�� �±װ� Ground���
        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Block")
        {
            isJump = false; // ���� ���¸� false��
        }
    }
}
