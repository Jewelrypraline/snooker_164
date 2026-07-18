using System;
using UnityEngine;
using UnityEngine.EventSystems;

public enum BallColor
{
    White,
    Red,
    Yellow,
    Green,
    Brown,
    Blue,
    Pink,
    Black
}

public class Ball : MonoBehaviour, IPointerClickHandler  // : คอลัมหมายถึง "ของ" ipoter ต้องใส่ UnityEngine.event ก่อนถึงจะขึ้น
{
    [SerializeField]
    private int point;

    [SerializeField]
    private BallColor color;

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log(point); //เมื่อคลิกโดนลูกไหน จะแจ้งคะแนน
        Gamemanager.instance.PlayerScore += point;
        Destroy(gameObject);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
           
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
