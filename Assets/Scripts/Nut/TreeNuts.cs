using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class TreeNuts : MonoBehaviour
{
    
        [SerializeField] private bool hasNut = false; // Флаг, чтобы определить, есть ли орех

        public bool HasNut
        {
            get => hasNut;
            set => hasNut = value;
        }

        // Другие методы, связанные с деревом, могут быть добавлены здесь
    
}











//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;

//public class TreeNuts : MonoBehaviour
//{
//    [SerializeField] private bool HasNut = false;
//    [SerializeField] private bool TouchedTreeNut = false;
//    [SerializeField] private bool NutAtSquirrel = false;
//    [SerializeField] private Image nutIcon;

//    private const string PLAYER = "MyTagPlayer";

//    private void Start()
//    {
//        if (nutIcon == null)
//        {
//            Debug.LogError("не назначен в инспекторе");
//        }
//    }

//    private void OnTriggerEnter2D(Collider2D collision)
//    {
//        if (collision.gameObject.CompareTag(PLAYER))
//        {
//            TouchedTreeNut = true;
//        }
//    }
//    private void OnTriggerExit2D(Collider2D collision)
//    {
//        if (collision.gameObject.CompareTag(PLAYER))
//        {
//            TouchedTreeNut = false;
//        }
//    }
//    private void Update()
//    {
//        ActionNut();
//        UpdateNutIcon();
//    }
//    private void ActionNut()
//    {
//        if (GameInput.Instance.PickUpOrDropNut())
//        {
//            if (TouchedTreeNut)
//            {
//                HasNut = true;
//            }
//            if (Player.Instance._standingOnSecondFloor)
//            {
//                HasNut = false;
//                NutAtSquirrel = true;
//            }
//        }
//    }
//    private void UpdateNutIcon()
//    {
//        if (nutIcon != null)
//        {
//            if (HasNut)
//            {
//                nutIcon.color = Color.white; // Сделать орех ярким
//            }
//            else
//            {
//                nutIcon.color = new Color(1, 1, 1, 0.5f); // Сделать орех полупрозрачным
//            }
//        }
//    }
//}