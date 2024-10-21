using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Nut : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private bool hasLanded = false; // Флаг, чтобы определить, приземлился ли орех
    [SerializeField] private Vector3 nutPosition;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Проверяем, что орех лежит на платформе с тегом "MyTagThirdFloor"
        if (collision.gameObject.CompareTag("MyTagThirdFloor"))
        {
            IdentifyThePositionOfFallenNut(); // Сохраняем позицию ореха
        }
    }

    private void IdentifyThePositionOfFallenNut()
    {
        nutPosition = transform.position; // Сохраняем позицию ореха
        NutOnThirdFloor = true; // Устанавливаем флаг, что орех на третьем этаже
    }

    public Vector3 NutPosition
    {
        get { return nutPosition; }
        private set { nutPosition = value; }
    }

    public event Action OnNutOnThirdFloorChanged;

    public bool NutOnThirdFloor
    {
        get { return hasLanded; }
        private set
        {
            if (hasLanded != value) // Если новое значение отличается от текущего.
            {
                hasLanded = value;
                //Debug.Log("Состояние NutOnThirdFloor изменено на: " + hasLanded); // Проверка изменения состояния
                // Срабатывание события, когда NutOnThirdFloor становится true
                if (hasLanded)
                {
                    OnNutOnThirdFloorChanged?.Invoke(); // Синтаксис вызова события.
                }
            }
        }
    }
}
//public class Nut : MonoBehaviour
//{
//    private Rigidbody2D rb;
//    [SerializeField] private bool hasLanded = false; // Флаг, чтобы определить, приземлился ли орех
//    [SerializeField] private Vector3 nutPosition;

//    private void Awake()
//    {
//        rb = GetComponent<Rigidbody2D>();
//    }

//    private void OnCollisionEnter2D(Collision2D collision)
//    {
//        // Проверяем, что орех лежит на платформе с тегом "MyTagThirdFloor"
//        if (collision.gameObject.CompareTag("MyTagThirdFloor"))
//        { 
//            hasLanded = true;
//            IdentifyThePositionOfFallenNut(); // Сохраняем позицию ореха
//        }
//    }

//    private void IdentifyThePositionOfFallenNut()
//    {
//        nutPosition = transform.position; // Сохраняем позицию ореха
//        Debug.Log("Орех остановился на позиции: " + nutPosition);
//    }

//    public Vector3 NutPosition
//    {
//        get { return nutPosition; }
//        private set { nutPosition = value; }
//    }

//    public event Action OnNutOnThirdFloorChanged;

//    public bool NutOnThirdFloor
//    {
//        get { return hasLanded; }
//        private set
//        {
//            if (hasLanded != value)//Если новое значение отличается от текущего.
//            {
//                hasLanded = value;
//                // Срабатывание события, когда NutOnThirdFloor становится true
//                if (hasLanded)
//                {
//                    OnNutOnThirdFloorChanged?.Invoke(); //Синтаксис вызова события.
//                }
//            }
//        }
//    }
//}




//
