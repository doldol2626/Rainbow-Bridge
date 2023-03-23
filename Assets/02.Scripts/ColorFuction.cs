using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorFuction : MonoBehaviour
{
    // 색을 구분해 줄 Enum 정의
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

    ColorCode cCode; // Enum 변수 선언

    Player player;

    GameObject block;

    Material blockMat; // Block의 Alpha값을 제어해 줄 Block의 Material 변수
    Color blockColor; // 플레이어가 충돌한 블록 색깔을 저장할 변수

    Dictionary<Color, ColorCode> colorKey = new Dictionary<Color, ColorCode>(); // 충돌한 색을 열거형 색으로 비교 탐색해줄 Dictionary 선언

    [SerializeField]
    float redBlockAlpha = 0.5f; // 수가 클수록 빨리 투명해지는 alpha값 제어를 위한 변수
    [SerializeField]
    float orangeBooster = 15f; // 발판을 밟았을 때 부스터처럼 밀어주는 힘 수치
    [SerializeField]
    float yellowJump = 30f;
    [SerializeField]
    List<Transform> BlueTr;

          // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Player>();

        // 사전리스트에 색상 코드와 열거형 값을 저장 
        colorKey.Add(Color.white, ColorCode.White); // 열거형 흰색(0)은 색상코드 (1, 1, 1, 1)
        colorKey.Add(Color.red, ColorCode.Red); // 열거형 빨간색(1)은 색상코드 (1, 0, 0, 1)
        colorKey.Add(new Color(1f, 127f / 255f, 0f), ColorCode.Orange); // 열거형 주황색(2)은 색상코드 (1, 약 0.5, 0, 1)
        colorKey.Add(new Color(1f, 1f, 0f), ColorCode.Yellow); // 열거형 노란색(3)은 색상코드 (1, 1, 0, 1)
        colorKey.Add(Color.green, ColorCode.Green); // 열거형 초록색(4)은 색상코드 (0, 1, 0, 1)
        colorKey.Add(Color.cyan, ColorCode.Blue); // 열거형 파란색(5)은 색상코드 (0, 1, 1, 1)
        colorKey.Add(Color.blue, ColorCode.Navy); // 열거형 남색(6)은 색상코드 (0, 0, 1, 1)
        colorKey.Add(Color.magenta, ColorCode.Violet); // 열거형 보라색(7)은 색상코드 (1, 0, 1, 1)
        colorKey.Add(Color.black, ColorCode.Black); // 열거형 검은색(8)은 색상코드 (0, 0, 0, 1)
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

    // 닿은 블록이 빨간색이라면 호출되는 함수
    void Red()
    {
        Debug.Log("Red");

        // 블록의 알파 값이 0보다 작거나 같다면
        if (blockColor.a <= 0)
        {
            // 그 블록을 파괴
            Destroy(block);

            FunctionOut(); // 함수에서 빠져 나옴
        }
        // 그게 아니라면
        else
        {
            // 계속해서 투명해지는 함수
            FadeOut(redBlockAlpha);
        }

    }

    // 오렌지색 블럭을 밟았을 때 호출되는 함수 (부스터발판)
    void Orange()
    {
        Debug.Log("Orange");

        Vector3 boosterVec = player.moveVec; // 플레이어가 보고 있는 방향을 저장

        player.rigid.AddForce(boosterVec * orangeBooster, ForceMode.Force); // 플레이어가 가고 있는 방향으로 힘을 가함
         
        Invoke("FunctionOut", 0.1f); // 0.1초 지연 후 함수에서 빠져 나옴
    }


    void Yellow()
    {
        Debug.Log("Yellow");

        player.rigid.AddForce(Vector3.up * yellowJump, ForceMode.Force); // 점프력만큼 y축으로 힘을 가해 줌
        player.isJump = true; // 점프 상태를 true로

        Invoke("FunctionOut", 0.1f); // 0.1초 지연 후 함수에서 빠져 나옴
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

    // 충돌 판정 (한 번 호출)
    private void OnTriggerEnter(Collider other)
    {
        // 충돌한 물체의 태그가 Block이라면
        if (other.gameObject.tag == "Block")
        {
            block = other.gameObject;

            blockMat = block.GetComponent<Renderer>().material; // 충돌한 블록의 Material에 접근

            blockColor = blockMat.color; // 블록의 색깔을 변수에 담아 줌
            cCode = colorKey[blockColor]; // 블록 색을 탐색하여 호출할 함수를 판별 (Dictionary(colorKey[blockColor]) -> Switch(cCode[0 ~ 8]) -> 함수 호출)
        }
    }

    // 스위치 문을 빠져나갈 수 있게 해주는 함수
    void FunctionOut()
    {
        cCode = (ColorCode)30; // 스위치문 제어 변수를 상관없는 수로 바꾸어줌
    }

    // 블록이 서서히 투명해지는 함수
    void FadeOut(float alphaMinus) // 난이도 조절을 위한 파라미터
    {
        blockColor.a -= alphaMinus * Time.deltaTime; // alpha값을 빼 줌

        blockMat.SetColor("_Color", blockColor); // Material의 컬러를 변경
    }
}
