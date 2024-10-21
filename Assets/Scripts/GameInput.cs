using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance { get; private set; }


    private PlayerInputActions playerInputActions;
    private void Awake()
    {
        Instance = this;
        playerInputActions = new PlayerInputActions();
        playerInputActions.Enable();
    }
    public Vector2 GetMovementVector()
    {
        return playerInputActions.Player.Move.ReadValue<Vector2>();

    }
    public bool IsJumping()
    {
        return playerInputActions.Player.Jump.triggered;
    }
    public bool IsDropingDown()
    {
        return playerInputActions.Player.DropDown.triggered;
    }

    public bool PickUpOrDropNut() 
    {
        return playerInputActions.Player.PickNutOrDrop.triggered;
    }

}








/////////////////////////////////////////////////////////////////////////////////////////////////////////////
///
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class GameInput : MonoBehaviour
//{
//    public static GameInput Instance { get; private set; }


//    private PlayerInputActions playerInputActions;
//    private void Awake()
//    {
//        Instance = this;
//        playerInputActions = new PlayerInputActions();
//        playerInputActions.Enable();
//    }
//    public Vector2 GetMovementVector()
//    {
//        return playerInputActions.Player.Move.ReadValue<Vector2>();

//    }
//    public bool IsJumping()
//    {
//        return playerInputActions.Player.Jump.triggered;
//    }
//    public bool IsDropingDown()
//    {
//        return playerInputActions.Player.DropDown.triggered;
//    }
//}