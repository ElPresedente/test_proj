using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    // Start is called before the first frame update
    public static LevelController LC; //чтобы мы не создавали сотни копий скрипта
    public float SliderTimeDelay;
    public float tempo;
    public KeyCode startButton; // кнопка начала движения
    public KeyCode pauseButton; // кнопка паузы
    private bool Playing;
    public AudioSource Music;
    public float MusicDelay;
    void Awake()
    {
        LC = this;
    }
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        if (!Playing)
        {
            if (Input.GetKeyDown(startButton)) 
            {
                Playing = true; // начинаем движение при нажатии старт кнопки
                ArrowHolder.AH.playable = true;
                Music.PlayDelayed(MusicDelay);

            }
        }

        if (Input.GetKeyDown(pauseButton)) 
        { 
            Pause();
        }
    }
    public void NotePressed(Collider2D note)
    {
        Debug.Log("pressed"+note.tag);
        note.gameObject.SetActive(false);
    }
    public void NoteMissed(Collider2D note)
    {
        Debug.Log("missed"+ note.tag);
        note.gameObject.SetActive(false);
    }
    public void Pause()
    {
        ArrowHolder.AH.pauseChange(Music) ;
    }
}
