using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquirrelAnimationController : MonoBehaviour
{
    [SerializeField] private Animator animator; // Ссылка на аниматор

    public void SetRunning(bool isRunning)
    {
        if (animator != null)
        {
            animator.SetBool("isRunning", isRunning);
        }
    }
    public void SetIdle(bool isIdle)
    {
        if (animator != null)
        {
            animator.SetBool("isRunning", isIdle);
        }
    }

    // Можно добавить другие методы для управления анимациями (например, прыжок, еда и т.д.)
}
