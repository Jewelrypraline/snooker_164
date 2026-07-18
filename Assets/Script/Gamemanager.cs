using UnityEngine;

public class Gamemanager : MonoBehaviour
{
    [SerializeField]
    private int playerScore;
    
    public int PlayerScore {  get { return playerScore; } set { playerScore = value; } }

    public static Gamemanager instance;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instance = this; //ใส่ในสตาร์ทข้อเสีย มันจะอยู่ในบอล มี instance เพื่อความชัวร์
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
