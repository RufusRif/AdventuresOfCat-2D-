using UnityEngine;
using System.Collections;
using System;

public class PlayerNutInteraction : MonoBehaviour
{
    [SerializeField] private bool touchedTreeNut = false; // Флаг, указывающий, что игрок находится у дерева с орехом
    [SerializeField] private Transform throwPoint; // Точка, из которой будет выбрасываться орех
    [SerializeField] private GameObject nutPrefab; // Префаб ореха

    [SerializeField] private SquirrelMovement squirrelMovement;



    private float throwForce = 8.2f; // Сила броска ореха

    private TreeNuts treeNuts; // Ссылка на скрипт TreeNuts
    private const string TREE_NUT_TAG = "MyTagTreeNuts"; // Тэг дерева с орехами

    private void Start()
    {
        // Находим скрипт TreeNuts в сцене
        treeNuts = FindObjectOfType<TreeNuts>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Проверяем, что игрок соприкасается с деревом, содержащим орехи
        if (collision.CompareTag(TREE_NUT_TAG))
        {
            touchedTreeNut = true; // Устанавливаем флаг, что орех доступен для сбора
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Проверяем, что игрок покидает область дерева с орехами
        if (collision.CompareTag(TREE_NUT_TAG))
        {
            touchedTreeNut = false; // Сбрасываем флаг, так как орех больше недоступен
        }
    }

    private void Update()
    {
        // Обрабатываем логику подбора и броска ореха
        PickUpOrDrop();
    }

    private void PickUpOrDrop()
    {
        // Проверяем, была ли нажата клавиша (пробел) для подбора или броска ореха.
        if (GameInput.Instance.PickUpOrDropNut())
        {
            if (touchedTreeNut)
            {
                // Если игрок находится у дерева, подбираем орех
                treeNuts.HasNut = true;
            }
            else if (Player.Instance._standingOnSecondFloor && treeNuts.HasNut)
            {
                // Если игрок на втором этаже и орех у него в инвентаре, бросаем орех
                treeNuts.HasNut = false;
                GameObject nutObject = Instantiate(nutPrefab, throwPoint.position, Quaternion.identity);
                Nut nutComponent = nutObject.GetComponent<Nut>();
                squirrelMovement.AssignNut(nutComponent);
                Rigidbody2D rb = nutObject.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    // Устанавливаем импульс для ореха
                    rb.velocity = new Vector2(0, throwForce);

                    // Устанавливаем орех в слой, игнорирующий платформы
                    nutObject.layer = LayerMask.NameToLayer("IgnorePlatform");

                    // Запускаем корутину для возврата слоя после пролетания платформ
                    StartCoroutine(ResetNutLayer(rb, nutObject));
                }
            }
        }
    }
    private IEnumerator ResetNutLayer(Rigidbody2D rb, GameObject nut)
    {
        // Ожидаем 0.7 секунды, чтобы орех поднялся над платформами
        yield return new WaitForSeconds(0.7f);

        if (rb != null)
        {
            rb.velocity = Vector2.zero; // Останавливаем орех
            rb.angularVelocity = 0; // Останавливаем вращение ореха
            rb.mass = 3; // Устанавливаем массу ореха
            
        }

        // Возвращаем орех на стандартный слой для взаимодействия с платформами
        nut.layer = LayerMask.NameToLayer("MyLayerNut");

    }
    // Объявление события для уведомления о том, что орех на третьем этаже
    



    
}





///////////////////////////////////////
///
//using UnityEngine;
//using System.Collections;

//public class PlayerNutInteraction : MonoBehaviour
//{
//    [SerializeField] private bool touchedTreeNut = false; // Флаг, указывающий, что игрок находится у дерева с орехом
//    [SerializeField] private Transform throwPoint; // Точка, из которой будет выбрасываться орех
//    [SerializeField] private GameObject nutPrefab; // Префаб ореха
//    [SerializeField] private bool nutOnThirdFloor = false; // Флаг, указывающий, что орех находится на третьем этаже
//    private float throwForce = 8.2f; // Сила броска ореха

//    private TreeNuts treeNuts; // Ссылка на скрипт TreeNuts

//    private const string TREE_NUT_TAG = "MyTagTreeNuts"; // Тэг дерева с орехами

//    public bool NutOnThirdFloor
//    {
//        get { return nutOnThirdFloor; } // Получение статуса ореха на третьем этаже
//        private set { nutOnThirdFloor = value; } // Установка статуса ореха на третьем этаже
//    }

//    private void Start()
//    {
//        // Находим скрипт TreeNuts в сцене
//        treeNuts = FindObjectOfType<TreeNuts>();
//    }

//    private void OnTriggerEnter2D(Collider2D collision)
//    {
//        // Проверяем, что игрок соприкасается с деревом, содержащим орехи
//        if (collision.CompareTag(TREE_NUT_TAG))
//        {
//            touchedTreeNut = true; // Устанавливаем флаг, что орех доступен для сбора
//        }
//    }

//    private void OnTriggerExit2D(Collider2D collision)
//    {
//        // Проверяем, что игрок покидает область дерева с орехами
//        if (collision.CompareTag(TREE_NUT_TAG))
//        {
//            touchedTreeNut = false; // Сбрасываем флаг, так как орех больше недоступен
//        }
//    }

//    private void Update()
//    {
//        // Обрабатываем логику подбора и броска ореха
//        PickUpOrDrop();
//    }

//    private void PickUpOrDrop()
//    {
//        // Проверяем, была ли нажата клавиша для подбора или броска ореха
//        if (GameInput.Instance.PickUpOrDropNut())
//        {
//            if (touchedTreeNut)
//            {
//                // Если игрок находится у дерева, подбираем орех
//                treeNuts.HasNut = true;
//            }
//            else if (Player.Instance._standingOnSecondFloor && treeNuts.HasNut)
//            {
//                // Если игрок на втором этаже и орех у него в инвентаре, бросаем орех
//                treeNuts.HasNut = false;
//                GameObject nut = Instantiate(nutPrefab, throwPoint.position, Quaternion.identity);
//                Rigidbody2D rb = nut.GetComponent<Rigidbody2D>();

//                if (rb != null)
//                {
//                    // Устанавливаем импульс для ореха
//                    rb.velocity = new Vector2(0, throwForce);

//                    // Устанавливаем орех в слой, игнорирующий платформы
//                    nut.layer = LayerMask.NameToLayer("IgnorePlatform");

//                    // Запускаем корутину для возврата слоя после пролетания платформ
//                    StartCoroutine(ResetNutLayer(rb, nut));
//                }

//                // Устанавливаем трансформ ореха для белки
//                SquirrelRunAndEat squirrel = FindAnyObjectByType<SquirrelRunAndEat>();
//                if (squirrel != null)
//                {
//                    squirrel.SetNutTransform(nut.transform);
//                }
//            }
//        }
//    }

//    private IEnumerator ResetNutLayer(Rigidbody2D rb, GameObject nut)
//    {
//        // Ожидаем 0.7 секунды, чтобы орех поднялся над платформами
//        yield return new WaitForSeconds(0.7f);

//        if (rb != null)
//        {
//            rb.velocity = Vector2.zero; // Останавливаем орех
//            rb.angularVelocity = 0; // Останавливаем вращение ореха
//            rb.mass = 3; // Устанавливаем массу ореха
//            nutOnThirdFloor = true; // Устанавливаем флаг, что орех на третьем этаже
//        }

//        // Возвращаем орех на стандартный слой для взаимодействия с платформами
//        nut.layer = LayerMask.NameToLayer("MyLayerNut");

//        // Устанавливаем трансформ ореха для белки
//        SquirrelRunAndEat squirrel = FindAnyObjectByType<SquirrelRunAndEat>();
//        if (squirrel != null)
//        {
//            squirrel.SetNutTransform(nut.transform);
//        }
//    }
//}