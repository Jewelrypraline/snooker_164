using System;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class Gamemanager : MonoBehaviour
{
    [SerializeField]
    private int playerScore;

    public int PlayerScore { get { return playerScore; } set { playerScore = value; } }

    [SerializeField]
    private GameObject[] ballPositions;

    [SerializeField]
    private GameObject ballPrefab;

    [SerializeField]
    private GameObject cueBall;

    [SerializeField]
    private float xInput;

    [SerializeField]
    private GameObject ballLine;

    [SerializeField]
    private GameObject cam;

    [SerializeField]
    private TMP_Text notiText;


    public static Gamemanager instance;

    private bool isShooting = false;
    private Rigidbody cueRb;

    // ตัวแปรใหม่ เอาไว้หน่วงเวลาไม่ให้กล้องดึงกลับทันทีตอนเพิ่งยิง
    private float shootTimer = 0f;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        if (cueBall != null)
            cueRb = cueBall.GetComponent<Rigidbody>();

        CameraBehideCueball();

        SetBall(BallColor.Red, 1);
        SetBall(BallColor.Yellow, 2);
        SetBall(BallColor.Green, 3);
        SetBall(BallColor.Brown, 4);
        SetBall(BallColor.Blue, 5);
        SetBall(BallColor.Pink, 6);
        SetBall(BallColor.Black, 7);
    }

    void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame && !isShooting)
            ShootBall();

        if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed)
            xInput = -0.05f;
        else if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed)
            xInput = 0.05f;
        else
            xInput = 0f;

        if (Keyboard.current.backspaceKey.wasPressedThisFrame)
            StopBall();

        RotateBall();

        // เช็คว่าถ้ายิงไปแล้ว
        if (isShooting && cueRb != null)
        {
            shootTimer += Time.deltaTime; // เริ่มนับเวลา

            // รอให้ผ่านไป 0.5 วินาที เพื่อให้ลูกบอลเริ่มกลิ้งก่อน แล้วค่อยเช็คความเร็ว
            if (shootTimer > 0.5f && cueRb.linearVelocity.magnitude < 0.1f)
            {
                StopBall();
            }
        }
    }

    private void SetBall(BallColor col, int i)
    {
        GameObject obj = Instantiate(ballPrefab,
                    ballPositions[i].transform.position,
                    Quaternion.identity);

        Ball b = obj.GetComponent<Ball>();
        b.SetColorAndPoint(col);
    }

    private void ShootBall()
    {
        isShooting = true;
        shootTimer = 0f; // รีเซ็ตตัวนับเวลาทุกครั้งที่ยิงใหม่

        if (ballLine != null)
            ballLine.SetActive(false);

        // 1. ถอดกล้องออกจากการเป็นลูกของ CueBall ก่อน
        cam.transform.parent = null;

        // 2. ย้ายกล้องไปมุมสูง
        cam.transform.position = new Vector3(0f, 30f, -42f);
        cam.transform.eulerAngles = new Vector3(45f, 0f, 0f);

        // 3. ออกแรงผลัก
        cueRb.AddRelativeForce(Vector3.forward * 50, ForceMode.Impulse);
    }

    private void RotateBall()
    {
        if (cueBall != null && !isShooting)
            cueBall.transform.Rotate(new Vector3(0f, xInput, 0f));
    }

    private void StopBall()
    {
        if (cueRb != null)
        {
            cueRb.linearVelocity = Vector3.zero;
            cueRb.angularVelocity = Vector3.zero;
        }

        cueBall.transform.eulerAngles = Vector3.zero;

        if (ballLine != null)
            ballLine.SetActive(true);

        CameraBehideCueball();

        isShooting = false;
    }

    private void CameraBehideCueball()
    {
        // เอากล้องกลับมาเป็นลูกของ CueBall 
        cam.transform.parent = cueBall.transform;

        // **ใช้ localPosition แทน position ธรรมดา เพื่อให้ล็อคตำแหน่งสัมพัทธ์ ป้องกันกล้องเพี้ยน**
        cam.transform.localPosition = new Vector3(0f, 7f, -15f);
        cam.transform.localEulerAngles = new Vector3(30f, 0f, 0f);
    }

    public void ShowScoreText(int n)
    {
        playerScore += n;
        notiText.text = $"Ball Point:{n}\nTotal Score:{playerScore}";
    }

    public void ShowString(string s)
    {
        notiText.text = s;
    }
}