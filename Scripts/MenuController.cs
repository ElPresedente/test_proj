using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{

    //сцена главного меню
    public void OnLevelButtonClick()
    {
        Loader.Load(Loader.Scene.Levels);
    }
    public void OnCreditsButtonClick()
    {
        transform.Find("Panel").gameObject.SetActive(true);
    }
    public void OnCreditsCloseButtonClick()
    {
        transform.Find("Panel").gameObject.SetActive(false);
    }
    public void OnExitButtonClick()
    {
        Application.Quit();
    }
    //общая кнопка выхода в меню
    public void OnMenuButtonClick()
    {
        Loader.Load(Loader.Scene.MainMenu);
    }
}
