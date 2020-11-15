using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading;
using UnityEngine;

public class ArrowHolder : MonoBehaviour
{
    public float BPM; // скорость движения в ударах в минуту
    public bool playable = false; // движутся плитки в данный момент или нет
    public KeyCode startButton; // кнопка начала движения
    public KeyCode pauseButton;

    private bool pauseToggle = false;
    void Start()
    {
        BPM = BPM / 60f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!playable)
        {
            if (Input.GetKeyDown(startButton)) playable = true; // начинаем движение при нажатии старт кнопки
        }
        else
        {
            transform.position = transform.position - new Vector3(0f, BPM * Time.deltaTime, 0f); // приращиваем к У координате смещение вниз
        }
        if (Input.GetKeyDown(pauseButton)) pauseChange();
    }
    public void pauseChange()
    {
        // установка "паузы" 
        pauseToggle = !pauseToggle;
        if (pauseToggle)
        {
            playable = false;
            transform.position += new Vector3(0f, BPM , 0f); //отодвигаем плитки повыше "как в пианисте"
        }
        else
        {
            playable = true;
        }
    }
}
