using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelsPicker : MonoBehaviour
{
    
    public void OnLevelButtonClick()
    {
        //тк у меня толко один уровень, то идем на него
        //позже сделать переход по тексту в кнопке
        Loader.Load(Loader.Scene.FirstLevel);
    } 
}
