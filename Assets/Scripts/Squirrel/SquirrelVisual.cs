using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEditor.Tilemaps;
using UnityEngine;

public class SquirrelVisual : MonoBehaviour
{
    private Animator animator;
    private SquirrelMovement squirrelMovement;

    private const string IS_RUNNING = "SqIsRunning";
    private const string IS_IDLE = "IsIdle";

    private void Awake()
    {
        animator = GetComponent<Animator>();
        squirrelMovement = GetComponentInParent<SquirrelMovement>(); // Получаем компонент SquirrelMovement

    }

    private void Update()
    {
        //Обновляем состояние анимаций на основе логики поведения из SquirrelMovement.

        animator.SetBool(IS_RUNNING, squirrelMovement.IsRunning()); //Если IsRunning() возвращает true, то IS_RUNNING устанавливается в true.
        animator.SetBool(IS_IDLE, squirrelMovement.IsIdle());

        Flip(squirrelMovement.GetDirection());
    }

    public void Flip(int newDirection)
    {
        // Получаем компонент Renderer
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        spriteRenderer.flipX = newDirection == -1;
    }
}