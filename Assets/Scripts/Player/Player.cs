using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    private Rigidbody2D rb;

    [SerializeField] private float movingSpeed = 5f;
    [SerializeField] private float jumpForce = 7f;

    [SerializeField] private bool StandingOnGround = false;

    [SerializeField] private bool TouchingFirstFloor = false;
    [SerializeField] private bool IsClingingToFirstFloor = false;
    [SerializeField] private bool StandingOnFirstFloor = false;
    [SerializeField] private bool TouchingSecondFloor = false;
    [SerializeField] private bool IsClingingToSecondFloor = false;
    [SerializeField] private bool StandingOnSecondFloor = false;
    public bool _standingOnSecondFloor
    {
        get
        {
            return StandingOnSecondFloor;
        }
        private set
        { StandingOnSecondFloor =  value;}
    }

    private float minMovingSpeed = 0.1f;
    private bool isRunning = false;
    private bool isJumping = false;
    [SerializeField] private bool gameOver = false;

    private void Awake() //Запускается ДО функции START. В awaek инициализирую, а в Start уже ссылаюсь.
    {
        Instance = this;
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if (GameInput.Instance.IsJumping())
        {
            if (StandingOnGround || StandingOnFirstFloor || StandingOnSecondFloor)
            {
                rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
                isJumping = true;
            }

            else if (IsClingingToFirstFloor)
            {
                // Персонаж висит на перекладине и нажимает прыжок
                IsClingingToFirstFloor = false;
                rb.gravityScale = 1; // Восстанавливаем гравитацию
                rb.AddForce(new Vector2(0, jumpForce - 4), ForceMode2D.Impulse); // Прыжок вверх                                                       
                transform.position = new Vector2(transform.position.x, transform.position.y + 0.5f);
                StandingOnFirstFloor = true;
            }
            else if (IsClingingToSecondFloor)
            {
                IsClingingToSecondFloor = false;
                rb.gravityScale = 1; // Восстанавливаем гравитацию
                rb.AddForce(new Vector2(0, jumpForce - 4), ForceMode2D.Impulse); // Прыжок вверх
                transform.position = new Vector2(transform.position.x, transform.position.y + 0.5f);
                StandingOnSecondFloor = true;
            }
        }
        if (GameInput.Instance.IsDropingDown() && IsClingingToFirstFloor)
        {
            IsClingingToFirstFloor = false;
            rb.gravityScale = 1;
            rb.velocity = new Vector2(rb.velocity.x, 0);

        }
        if (GameInput.Instance.IsDropingDown() && IsClingingToSecondFloor)
        {
            IsClingingToSecondFloor = false;
            rb.gravityScale = 1;
            rb.velocity = new Vector2(rb.velocity.x, 0);

        }
        if (GameInput.Instance.IsDropingDown() && StandingOnFirstFloor)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y - 1f);
            rb.gravityScale = 1;
            rb.velocity = new Vector2(rb.velocity.x, -jumpForce / 2);
            StandingOnFirstFloor = false;
        }

        if (GameInput.Instance.IsDropingDown() && StandingOnSecondFloor)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y - 1f);
            rb.gravityScale = 1;
            rb.velocity = new Vector2(rb.velocity.x, -jumpForce / 2);
            StandingOnSecondFloor = false;
        }
    }
    public bool IsJumping()
    {
        return isJumping;
    }
    private void FixedUpdate()
    {
        HandleMovement();
    }
    private void HandleMovement()
    {
        Vector2 inputVector = GameInput.Instance.GetMovementVector();
        rb.velocity = new Vector2(inputVector.x * movingSpeed, rb.velocity.y);

        if (inputVector.x > 0) { transform.localScale = new Vector2(1, 1); }

        else if (inputVector.x < 0) { transform.localScale = new Vector2(-1, 1); }

        if (Mathf.Abs(inputVector.x) > minMovingSpeed || Mathf.Abs(inputVector.y) > minMovingSpeed)
        {
            isRunning = true;

        }
        else
        {
            isRunning = false;
        }
        if (IsClingingToFirstFloor || IsClingingToSecondFloor)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);

        }
    }
    public bool IsRunning() { return isRunning; }
    private void HandleFloorCollision(Vector2 contactPoint, Vector2 playerPosition, ref bool touchingFloor, ref bool isClingingToFloor, ref bool standingOnFloor)
    {
        // Устанавливаем, что персонаж касается этажа
        touchingFloor = true;


        // Проверяем, находится ли персонаж ниже контактной точки (цепляется за край)
        if (playerPosition.y < contactPoint.y)
        {
            // Логика, если персонаж цепляется за край
            isClingingToFloor = true;
            rb.velocity = Vector2.zero; // Останавливаем движение
            rb.gravityScale = 0;        // Отключаем гравитацию, пока персонаж цепляется


            // Устанавливаем, что персонаж не стоит на этаже, т.к. цепляется
            standingOnFloor = false;
        }
        else
        {
            // Логика, если персонаж стоит на этаже
            isClingingToFloor = false;
            standingOnFloor = true;
            rb.gravityScale = 1;     // Включаем гравитацию, когда персонаж стоит

        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 contactPoint = collision.contacts[0].point;
        Vector2 playerPosition = transform.position;

        if (collision.gameObject.CompareTag("MyTagFirstFloor"))
        {
            HandleFloorCollision(contactPoint, playerPosition, ref TouchingFirstFloor, ref IsClingingToFirstFloor, ref StandingOnFirstFloor);
        }
        else if (collision.gameObject.CompareTag("MyTagSecondFloor"))
        {
            HandleFloorCollision(contactPoint, playerPosition, ref TouchingSecondFloor, ref IsClingingToSecondFloor, ref StandingOnSecondFloor);
        }
        else if (collision.gameObject.CompareTag("MyTagGround"))
        {
            StandingOnGround = true;
        }
        else if (StandingOnGround || StandingOnFirstFloor || StandingOnSecondFloor)
        {
            isJumping = false;
        }
        //else if (collision.gameObject.CompareTag("MyTagGround") || collision.gameObject.CompareTag("MyTagFirstFloor") || collision.gameObject.CompareTag("MyTagSecondFloor"))
        //{
        //    isJumping = false;
        //}


    }
    
    //private void OnTriggerEnter2D(Collider2D other)
    //{
    //    if (other.CompareTag("MyTagObstacle"))
    //    {
    //        Debug.Log("Touched the obstacle. Destroying it...");
    //        Destroy(other.gameObject);
    //        gameOver = true;
    //    }
    //}

    
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("MyTagFirstFloor"))
        {
            TouchingFirstFloor = false;
            IsClingingToFirstFloor = false;
            StandingOnFirstFloor = false;
           
        }
        else if (collision.gameObject.CompareTag("MyTagSecondFloor"))
        {
            TouchingSecondFloor = false;
            IsClingingToSecondFloor = false;
            StandingOnSecondFloor = false;
           
        }
        else if (collision.gameObject.CompareTag("MyTagGround"))
        {
            StandingOnGround = false;
        }
    }


}

//////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    private Rigidbody2D rb;

    [SerializeField] private float movingSpeed = 5f;
    [SerializeField] private float jumpForce = 7f;

    [SerializeField] private bool StandingOnGround = false;

    [SerializeField] private bool TouchingFirstFloor = false;
    [SerializeField] private bool IsClingingToFirstFloor = false;
    [SerializeField] private bool StandingOnFirstFloor = false;
    [SerializeField] private bool TouchingSecondFloor = false;
    [SerializeField] private bool IsClingingToSecondFloor = false;
    [SerializeField] private bool StandingOnSecondFloor = false;

    private float minMovingSpeed = 0.1f;
    private bool isRunning = false;
    private bool isJumping = false;//



    private void Awake() //Запускается ДО функции START. В awaek инициализирую, а в Start уже ссылаюсь.
    {
        Instance = this;
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if (GameInput.Instance.IsJumping())
        {
            if (StandingOnGround || StandingOnFirstFloor || StandingOnSecondFloor)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                isJumping = true;
            }

            else if (IsClingingToFirstFloor)
            {
                // Персонаж висит на перекладине и нажимает прыжок
                IsClingingToFirstFloor = false;
                rb.gravityScale = 1; // Восстанавливаем гравитацию
                rb.velocity = new Vector2(rb.velocity.x, jumpForce - 4); // Прыжок вверх                                                       
                transform.position = new Vector2(transform.position.x, transform.position.y + 0.5f);
                StandingOnFirstFloor = true;
            }
            else if (IsClingingToSecondFloor)
            {
                IsClingingToSecondFloor = false;
                rb.gravityScale = 1; // Восстанавливаем гравитацию
                rb.velocity = new Vector2(rb.velocity.x, jumpForce - 4); // Прыжок вверх
                transform.position = new Vector2(transform.position.x, transform.position.y + 0.5f);
                StandingOnSecondFloor = true;
            }
        }
        if (GameInput.Instance.IsDropingDown() && IsClingingToFirstFloor)
        {
            IsClingingToFirstFloor = false;
            rb.gravityScale = 1;
            rb.velocity = new Vector2(rb.velocity.x, 0);

        }
        if (GameInput.Instance.IsDropingDown() && IsClingingToSecondFloor)
        {
            IsClingingToSecondFloor = false;
            rb.gravityScale = 1;
            rb.velocity = new Vector2(rb.velocity.x, 0);

        }
        if (GameInput.Instance.IsDropingDown() && StandingOnFirstFloor)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y - 1f);
            rb.gravityScale = 1;
            rb.velocity = new Vector2(rb.velocity.x, -jumpForce / 2);
            StandingOnFirstFloor = false;
        }

        if (GameInput.Instance.IsDropingDown() && StandingOnSecondFloor)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y - 1f);
            rb.gravityScale = 1;
            rb.velocity = new Vector2(rb.velocity.x, -jumpForce / 2);
            StandingOnSecondFloor = false;
        }
    }
    public bool IsJumping()
    {
        return isJumping;
    }
    private void FixedUpdate()
    {
        HandleMovement();
    }
    private void HandleMovement()
    {
        Vector2 inputVector = GameInput.Instance.GetMovementVector();
        rb.velocity = new Vector2(inputVector.x * movingSpeed, rb.velocity.y);

        if (inputVector.x > 0) { transform.localScale = new Vector2(1, 1); }

        else if (inputVector.x < 0) { transform.localScale = new Vector2(-1, 1); }

        if (Mathf.Abs(inputVector.x) > minMovingSpeed || Mathf.Abs(inputVector.y) > minMovingSpeed)
        {
            isRunning = true;

        }
        else
        {
            isRunning = false;
        }
        if (IsClingingToFirstFloor || IsClingingToSecondFloor)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);

        }
    }
    public bool IsRunning() { return isRunning; }
    private void HandleFloorCollision(Vector2 contactPoint, Vector2 playerPosition, ref bool touchingFloor, ref bool isClingingToFloor, ref bool standingOnFloor)
    {
        // Устанавливаем, что персонаж касается этажа
        touchingFloor = true;


        // Проверяем, находится ли персонаж ниже контактной точки (цепляется за край)
        if (playerPosition.y < contactPoint.y)
        {
            // Логика, если персонаж цепляется за край
            isClingingToFloor = true;
            rb.velocity = Vector2.zero; // Останавливаем движение
            rb.gravityScale = 0;        // Отключаем гравитацию, пока персонаж цепляется


            // Устанавливаем, что персонаж не стоит на этаже, т.к. цепляется
            standingOnFloor = false;
        }
        else
        {
            // Логика, если персонаж стоит на этаже
            isClingingToFloor = false;
            standingOnFloor = true;
            rb.gravityScale = 1;     // Включаем гравитацию, когда персонаж стоит

        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 contactPoint = collision.contacts[0].point;
        Vector2 playerPosition = transform.position;

        if (collision.gameObject.CompareTag("MyTagFirstFloor"))
        {
            HandleFloorCollision(contactPoint, playerPosition, ref TouchingFirstFloor, ref IsClingingToFirstFloor, ref StandingOnFirstFloor);
        }
        else if (collision.gameObject.CompareTag("MyTagSecondFloor"))
        {
            HandleFloorCollision(contactPoint, playerPosition, ref TouchingSecondFloor, ref IsClingingToSecondFloor, ref StandingOnSecondFloor);
        }
        else if (collision.gameObject.CompareTag("MyTagGround"))
        {
            StandingOnGround = true;
        }
        if (StandingOnGround || StandingOnFirstFloor || StandingOnSecondFloor)
        {
            isJumping = false;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("MyTagFirstFloor"))
        {
            TouchingFirstFloor = false;
            IsClingingToFirstFloor = false;
            StandingOnFirstFloor = false;
            Debug.Log($"TouchingFirstFloor = {TouchingFirstFloor}");
        }
        else if (collision.gameObject.CompareTag("MyTagSecondFloor"))
        {
            TouchingSecondFloor = false;
            IsClingingToSecondFloor = false;
            StandingOnSecondFloor = false;
            Debug.Log($"TouchingSecondFloor = {TouchingSecondFloor}");
        }
        else if (collision.gameObject.CompareTag("MyTagGround"))
        {
            StandingOnGround = false;
            Debug.Log($"StandingOnGround = {StandingOnGround}");
        }
    }


}
*/