using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading;
using UnityEngine;

public class ArrowHolder : MonoBehaviour
{
    private float BPM; // скорость движения в ударах в минуту
    public bool playable; // движутся плитки в данный момент или нет
    public static ArrowHolder AH;
    private bool pauseToggle = false;
    void Start()
    {
        AH = this;
        playable = false;
        BPM = LevelController.LC.Tempo / 60f;
    }

    // Update is called once per frame
    void Update()
    {
        if(playable)
        {
            transform.position = transform.position - new Vector3(0f, BPM * Time.deltaTime, 0f); // приращиваем к У координате смещение вниз
        }
    }
    public void pauseChange(AudioSource Music)
    {
        // установка "паузы" 
        pauseToggle = !pauseToggle;
        if (pauseToggle)
        {
            playable = false;
            transform.position += new Vector3(0f, BPM , 0f); //отодвигаем плитки повыше "как в пианисте"
            Music.Pause();
            Music.time -= 1f;
        }
        else
        {
            playable = true;
            Music.UnPause();
        }
    }
}
