using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{

    private SpriteRenderer theSR;
    //public Sprite defaultImage;
    //public Sprite pressedImage;

    public KeyCode keyToPress;
    public Color defaultColor = new Color(1f, 1f, 1f);
    public Color pressedColor = new Color(0.7f, 0.7f, 0.7f, 0.7f);
    public bool ButtonPressed; //нажата кнопка сейчас или нет (хотя ля, можно же клавишу отслеживать...)
    public bool ButtonBlocked;
    private bool CurrNotePressed;
    private bool CurrNoteAct;
    private bool CurrSlidePressed;
    private float CurrSlideTime;
    void Start()
    {
        
        theSR = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(keyToPress))
        {
            theSR.color = pressedColor; //немного "анимации" для нажимающихся кнопОчек
            ButtonPressed = true;
        }
        if (Input.GetKeyUp(keyToPress))
        {
            theSR.color = defaultColor;
            ButtonPressed = false;
        }

    }
    private void OnTriggerEnter2D(Collider2D note)
    {
        if (ButtonPressed)//если во время "входа" плашки кнопка нажата
        {
            ButtonBlocked = true;//то она блокируется то ее отжатия и повторного нажатия
        }//крч не выйдет просто зажать все кнопки и выиграть
        if (note.tag == "Slider") {
            
            CurrSlideTime = LevelController.LC.SliderTimeDelay;
            Debug.Log(CurrSlideTime);
            CurrSlidePressed = false;//сброс переменных про слайдерам
            
        }
        CurrNoteAct = true;
        CurrNotePressed = false;

    }
    private void OnTriggerStay2D(Collider2D note)
    {
        if (!ButtonPressed && ButtonBlocked)
        {
            ButtonBlocked = false; //снятие блокировки
        }
        if (!ButtonBlocked && ButtonPressed)
        {
            CurrNotePressed = true; //булевое значение, отвечающее за условие "нажатия" любого тайла
        }
        if (!ButtonPressed)
        {
            CurrNotePressed = false;
        }
        if (note.tag == "Slider") //блок обработки слайдеров
        {
            //Debug.Log("72");
            if (!CurrNotePressed)//если слайдер не нажат
            {
                //Debug.Log("75");
                CurrSlideTime -= Time.deltaTime; //даем игроку время на нажатие слайдера
                if (CurrSlideTime <= 0) //если оно истекает - промах
                {
                    //Debug.Log("79");
                    CurrNoteAct = false;
                    LevelController.LC.NoteMissed(note);
                    
                }
            }
            else
            {
                //Debug.Log("86");
                CurrSlidePressed = true; //когда игрок зажал слайдер - начинаем это отслеживать
            }
            if(CurrSlidePressed && !ButtonPressed) //если слайдер отпущен раньше времени - промах
            {                                      //в будущем нужно дать возможность отпустить слайдер раньше времени, ухудшая показатель точности
                //Debug.Log("91");
                CurrNoteAct = false;
                LevelController.LC.NoteMissed(note);
            }

        }
        if(note.tag == "Note")//блок обработки нот
        {
            
            if (CurrNotePressed)
            {
                //Debug.Log("103");
                CurrNoteAct = false;
                LevelController.LC.NotePressed(note);
            }
            
        }
    }
    private void OnTriggerExit2D(Collider2D note)
    {
        if(note.tag == "Note" && CurrNoteAct)
        {
            LevelController.LC.NoteMissed(note);
        }
        if(note.tag == "Slider" && CurrNoteAct)
        {
            //Debug.Log("117");
            if (CurrSlidePressed)
            {
                //Debug.Log("120");
                //удаление ноты (будет более подробная обработка нажатия)
                LevelController.LC.NotePressed(note);
            }
            else
            {
                //Debug.Log("126");
                //удаление ноты (будет более подробная обработка нажатия)
                LevelController.LC.NoteMissed(note);
            }
        }
    }
}
