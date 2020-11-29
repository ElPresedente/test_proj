using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LearnScript : MonoBehaviour
{
    private KeyCode activateButton;
    private bool active = true;
    // Start is called before the first frame update
    void Start()
    {
        //получаем кнопку назала игры
        activateButton = LevelController.LC.startButton;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(activateButton) && active)
        {//если она нажата - убираем подсказки
            transform.Find("Learn").gameObject.SetActive(false);
            active = false;
        }
    }
}
