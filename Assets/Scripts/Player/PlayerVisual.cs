using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerVisual : MonoBehaviour
{
    private Animator animator;

    private const string IS_RUNNING = "IsRunning";
    private const string IS_JUMPING = "IsJumping";
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        animator.SetBool(IS_RUNNING, Player.Instance.IsRunning());
        animator.SetBool(IS_JUMPING, Player.Instance.IsJumping());

    }
}












/////////////////////////////////////////////////////////////////////
///
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;


//public class PlayerVisual : MonoBehaviour
//{
//    private Animator animator;

//    private const string IS_RUNNING = "IsRunning";
//    private const string IS_JUMPING = "IsJumping";
//    private void Awake()
//    {
//        animator = GetComponent<Animator>();
//    }

//    private void Update()
//    {
//        animator.SetBool(IS_RUNNING, Player.Instance.IsRunning());
//        animator.SetBool(IS_JUMPING, Player.Instance.IsJumping());

//    }
//}