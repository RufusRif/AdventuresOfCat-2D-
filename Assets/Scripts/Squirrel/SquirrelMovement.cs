using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class SquirrelMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float speed = 5f; // Скорость движения
    [SerializeField] private int minSeconds = 3; // Минимальное время движения/остановки
    [SerializeField] private int maxSeconds = 10; // Максимальное время движения/остановки
    public int direction; // -1 для движения влево, 1 для движения вправо
    private Vector3 targetPosition;
    [SerializeField] private bool isMovingToNut = false;

    [SerializeField] private Nut nut;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        direction = Random.Range(0, 2) == 0 ? -1 : 1;

        StartCoroutine(SquirrelMove()); // Начинаем движение
    }

    public void AssignNut(Nut newNut)
    {
        nut = newNut;
        nut.OnNutOnThirdFloorChanged += HandleNutOnThirdFloorChanged;
        Debug.Log("Орех назначен и подписан на событие! Nut: " + nut);
    }
    private void HandleNutOnThirdFloorChanged()
    {
        Debug.Log("Запускаем поведение подбегания белки к ореху.");
        targetPosition = nut.NutPosition;
        isMovingToNut = true; // Устанавливаем флаг, что белка движется к ореху
    }
    private void Update()
    {
        if (isMovingToNut) 
        {
            MoveTowardsNut();
        }
    }
    private void MoveTowardsNut()
    {
        // Двигаемся к цели
        Vector3 direction = (targetPosition - transform.position).normalized;
        rb.velocity = new Vector2(direction.x * speed, rb.velocity.y);

        // Проверяем, достигла ли белка ореха
        if (Vector2.Distance(transform.position, targetPosition) < 0.1f)
        {
            rb.velocity = Vector2.zero; // Останавливаем белку
            isMovingToNut = false; // Белка достигла ореха
            Debug.Log("Белка достигла ореха и остановилась.");
        }
    }

    private IEnumerator SquirrelMove()
    {
        while (true)
        {
            rb.velocity = new Vector2(direction * speed, rb.velocity.y);


            float movementDuration = Random.Range(minSeconds, maxSeconds);
            yield return new WaitForSeconds(movementDuration); // Ждем, пока не истечет время движения

            rb.velocity = Vector2.zero;


            float stopDuration = Random.Range(minSeconds, maxSeconds);
            yield return new WaitForSeconds(stopDuration); // Ждем, пока не истечет время остановки

            direction = Random.Range(0, 2) == 0 ? -1 : 1;

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("MyTagWalls"))
        {
            direction = -direction;


            rb.velocity = new Vector2(direction * speed, rb.velocity.y); // Устанавливаем новую скорость
        }
    }

    public bool IsRunning()
    {
        return rb.velocity.x != 0; // Возвращает true если скорость не нулевая.
    }

    public bool IsIdle()
    {
        return rb.velocity.x == 0; // Возвращает true если скорость нулевая.
    }

    public int GetDirection()
    {
        return direction;
    }
}
//public class SquirrelMovement : MonoBehaviour
//{
//    private Rigidbody2D rb;
//    [SerializeField] private float speed = 5f; // Скорость движения
//    [SerializeField] private int minSeconds = 3; // Минимальное время движения/остановки
//    [SerializeField] private int maxSeconds = 10; // Максимальное время движения/остановки
//    public int direction; // -1 для движения влево, 1 для движения вправо

//    [SerializeField] private Nut nut;

//    private void Awake()
//    {
//        rb = GetComponent<Rigidbody2D>();
//        direction = Random.Range(0, 2) == 0 ? -1 : 1; //Если усовие "Random.Range(0, 2) == 0" true то присваетвается значение -1,  иначе (если false) то присваевается значение 1.
//        StartCoroutine(SquirrelMove()); // Начинаем движение
//    }
//    public void AssignNut(Nut newNut)
//    {
//        nut = newNut;
//        nut.OnNutOnThirdFloorChanged += HandleNutOnThirdFloorChanged;
//        Debug.Log("Орех назначен и подписан на событие!");
//    }
//    private void Start()
//    {


//    }

//    private void HandleNutOnThirdFloorChanged()
//    {
//        Debug.Log("Орех достиг третьего этажа!");
//        // Здесь можно реализовать поведение белки в ответ на событие.
//    }
//    private IEnumerator SquirrelMove()
//    {
//        while (true)
//        {
//            rb.velocity = new Vector2(direction * speed, rb.velocity.y);

//            float movementDuration = Random.Range(minSeconds, maxSeconds);
//            yield return new WaitForSeconds(movementDuration); // Ждем, пока не истечет время движения



//            rb.velocity = Vector2.zero;

//            float stopDuration = Random.Range(minSeconds, maxSeconds);
//            yield return new WaitForSeconds(stopDuration); // Ждем, пока не истечет время остановки

//            direction = Random.Range(0, 2) == 0 ? -1 : 1; //Если усовие "Random.Range(0, 2) == 0" true то присваетвается значение -1,  иначе (если false) то присваевается значение 1.
//        }
//    }
//    private void OnCollisionEnter2D(Collision2D collision)
//    {
//        if (collision.collider.CompareTag("MyTagWalls"))
//        {
//            direction = -direction;

//            rb.velocity = new Vector2(direction * speed, rb.velocity.y); // Устанавливаем новую скорость
//        }
//    }

//    public bool IsRunning()
//    {
//        return rb.velocity.x != 0; //Возвращает true если скорость не нулевая.
//    }
//    public bool IsIdle()
//    {
//        return rb.velocity.x == 0; //Возвращает true если скорость нулевая.
//    }
//    public int GetDirection()
//    {
//        return direction;
//    }
//}











