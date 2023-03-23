using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorFuction : MonoBehaviour
{
    // ���� ������ �� Enum ����
    enum ColorCode
    {
        White,  // FFFFFF (255, 255, 255) // 0
        Red,    // FF0000 (255,   0,   0) // 1
        Orange, // FF7F00 (255, 127,   0) // 2
        Yellow, // FFFF00 (255, 255,   0) // 3
        Green,  // 00FF00 (  0, 255,   0) // 4
        Blue,   // 00FFFF (  0, 255, 255) // 5
        Navy,   // 0000FF (  0,   0, 255) // 6
        Violet, // FF00FF (255,   0, 255) // 7
        Black   // 000000 (  0,   0,   0) // 8
    }

    ColorCode cCode; // Enum ���� ����

    Player player;

    GameObject block;

    Material blockMat; // Block�� Alpha���� ������ �� Block�� Material ����
    Color blockColor; // �÷��̾ �浹�� ��� ������ ������ ����

    Dictionary<Color, ColorCode> colorKey = new Dictionary<Color, ColorCode>(); // �浹�� ���� ������ ������ �� Ž������ Dictionary ����

    [SerializeField]
    float redBlockAlpha = 0.5f; // ���� Ŭ���� ���� ���������� alpha�� ��� ���� ����
    [SerializeField]
    float orangeBooster = 15f; // ������ ����� �� �ν���ó�� �о��ִ� �� ��ġ
    [SerializeField]
    float yellowJump = 30f;
    [SerializeField]
    List<Transform> BlueTr;

          // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Player>();

        // ��������Ʈ�� ���� �ڵ�� ������ ���� ���� 
        colorKey.Add(Color.white, ColorCode.White); // ������ ���(0)�� �����ڵ� (1, 1, 1, 1)
        colorKey.Add(Color.red, ColorCode.Red); // ������ ������(1)�� �����ڵ� (1, 0, 0, 1)
        colorKey.Add(new Color(1f, 127f / 255f, 0f), ColorCode.Orange); // ������ ��Ȳ��(2)�� �����ڵ� (1, �� 0.5, 0, 1)
        colorKey.Add(new Color(1f, 1f, 0f), ColorCode.Yellow); // ������ �����(3)�� �����ڵ� (1, 1, 0, 1)
        colorKey.Add(Color.green, ColorCode.Green); // ������ �ʷϻ�(4)�� �����ڵ� (0, 1, 0, 1)
        colorKey.Add(Color.cyan, ColorCode.Blue); // ������ �Ķ���(5)�� �����ڵ� (0, 1, 1, 1)
        colorKey.Add(Color.blue, ColorCode.Navy); // ������ ����(6)�� �����ڵ� (0, 0, 1, 1)
        colorKey.Add(Color.magenta, ColorCode.Violet); // ������ �����(7)�� �����ڵ� (1, 0, 1, 1)
        colorKey.Add(Color.black, ColorCode.Black); // ������ ������(8)�� �����ڵ� (0, 0, 0, 1)
    }

    // Update is called once per frame
    void Update()
    {
        switch (cCode)
        {
            case ColorCode.White:

                break;
            case ColorCode.Red:
                Red();
                break;
            case ColorCode.Orange:
                Orange();
                break;
            case ColorCode.Yellow:
                Yellow();
                break;
            case ColorCode.Green:
                Green();
                break;
            case ColorCode.Blue:
                Blue();
                break;
            case ColorCode.Navy:
                Navy();
                break;
            case ColorCode.Violet:
                Violet();
                break;
            case ColorCode.Black:

                break;
            default:
                break;
        }
    }

    // ���� ����� �������̶�� ȣ��Ǵ� �Լ�
    void Red()
    {
        Debug.Log("Red");

        // ����� ���� ���� 0���� �۰ų� ���ٸ�
        if (blockColor.a <= 0)
        {
            // �� ����� �ı�
            Destroy(block);

            FunctionOut(); // �Լ����� ���� ����
        }
        // �װ� �ƴ϶��
        else
        {
            // ����ؼ� ���������� �Լ�
            FadeOut(redBlockAlpha);
        }

    }

    // �������� ���� ����� �� ȣ��Ǵ� �Լ� (�ν��͹���)
    void Orange()
    {
        Debug.Log("Orange");

        Vector3 boosterVec = player.moveVec; // �÷��̾ ���� �ִ� ������ ����

        player.rigid.AddForce(boosterVec * orangeBooster, ForceMode.Force); // �÷��̾ ���� �ִ� �������� ���� ����
         
        Invoke("FunctionOut", 0.1f); // 0.1�� ���� �� �Լ����� ���� ����
    }


    void Yellow()
    {
        Debug.Log("Yellow");

        player.rigid.AddForce(Vector3.up * yellowJump, ForceMode.Force); // �����¸�ŭ y������ ���� ���� ��
        player.isJump = true; // ���� ���¸� true��

        Invoke("FunctionOut", 0.1f); // 0.1�� ���� �� �Լ����� ���� ����
    }

    void Green()
    {
        Debug.Log("Green");
    }

    void Blue()
    {
        Debug.Log("Blue");
    }

    void Navy()
    {
        Debug.Log("Navy");
    }

    void Violet()
    {
        Debug.Log("Violet");
    }

    // �浹 ���� (�� �� ȣ��)
    private void OnTriggerEnter(Collider other)
    {
        // �浹�� ��ü�� �±װ� Block�̶��
        if (other.gameObject.tag == "Block")
        {
            block = other.gameObject;

            blockMat = block.GetComponent<Renderer>().material; // �浹�� ����� Material�� ����

            blockColor = blockMat.color; // ����� ������ ������ ��� ��
            cCode = colorKey[blockColor]; // ��� ���� Ž���Ͽ� ȣ���� �Լ��� �Ǻ� (Dictionary(colorKey[blockColor]) -> Switch(cCode[0 ~ 8]) -> �Լ� ȣ��)
        }
    }

    // ����ġ ���� �������� �� �ְ� ���ִ� �Լ�
    void FunctionOut()
    {
        cCode = (ColorCode)30; // ����ġ�� ���� ������ ������� ���� �ٲپ���
    }

    // ����� ������ ���������� �Լ�
    void FadeOut(float alphaMinus) // ���̵� ������ ���� �Ķ����
    {
        blockColor.a -= alphaMinus * Time.deltaTime; // alpha���� �� ��

        blockMat.SetColor("_Color", blockColor); // Material�� �÷��� ����
    }
}
