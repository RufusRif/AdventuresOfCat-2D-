using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NutIconManager : MonoBehaviour
{
    [SerializeField] private Image nutIcon; 

    [SerializeField] private TreeNuts treeNuts;  



    private void Start()
    {
        if(treeNuts == null)
        {
            treeNuts = FindObjectOfType<TreeNuts>();
        }
    }
    private void Update()
    {
        if (treeNuts != null)
        {
            UpdateNutIcon(treeNuts.HasNut);
        }
    }


    private void UpdateNutIcon(bool everything) 
    {
        if (nutIcon != null) 
        {
            nutIcon.color = everything ? Color.white : new Color(1, 1, 1, 0.4f); // (Работает так:)     условие ? выражение1 : выражение 2
                                                                             // (замена if) если условие верно выполняется выражение1 иначе выражение2
        }
    }
}





//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
///
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;

//public class NutIconManager : MonoBehaviour
//{
//    [SerializeField] private Image nutIcon; //Объявляется переменная nutIcon типа Image.(Это делается для того , чтобы в инспекторе можно было добавить конкретный элемент UI-изображения в это поле.

//    [SerializeField] private TreeNuts treeNuts;  //Объявляется переменная treeNuts типа TreeNuts. Для того чтобы можно было обращаться в различным кускам кода в скрипте TreeNuts. В данном случае к свойству HasNut.



//    private void Start()
//    {
//        if (treeNuts == null) //Если ссылка на объект не найдена то использовать метод FindObjectOfType который ищет объект с сцене который содержит компонент TreeNuts
//        {
//            treeNuts = FindObjectOfType<TreeNuts>();
//        }
//    }
//    private void Update()
//    {
//        if (treeNuts != null) //Если treeNuts не null (то есть если он существует) то сработает функция UpdateNutIcon в которую передается значение переменной hasNut(взятой из свойства из скрипта TreeNuts через обращение к ней по ссылке treeNuts.
//        {
//            UpdateNutIcon(treeNuts.HasNut);
//        }
//    }


//    private void UpdateNutIcon(bool hasNut) //функция принимает параметр булевая переменная (название не обязательно должно совпадать с hasNut (или должно?)
//    {
//        if (nutIcon != null) //Если обект nutIcon типа Image существует то объект nutIcon окращивается в яркий цвет.
//        {
//            nutIcon.color = hasNut ? Color.white : new Color(1, 1, 1, 0.4f); // (Работает так:)     условие ? выражение1 : выражение 2
//                                                                             // (замена if) если условие верно выполняется выражение1 иначе выражение2
//        }
//    }
//}