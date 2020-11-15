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
    private bool CurrSlideAct;
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
            CurrSlidePressed = false;//сброс переменных про слайдерам
            CurrSlideAct = true;
        }
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
            if (!CurrNotePressed)//если слайдер не нажат
            {
                CurrSlideTime -= Time.deltaTime; //даем игроку время на нажатие слайдера
                if (CurrSlideTime <= 0) //если оно истекает - промах
                {
                    LevelController.LC.NoteMissed(note);
                    CurrSlideAct = false;
                }
            }
            else
            {
                CurrSlidePressed = true; //когда игрок зажал слайдер - начинаем это отслеживать
            }
            if(CurrSlidePressed && !ButtonPressed) //если слайдер отпущен раньше времени - промах
            {                                      //в будущем нужно дать возможность отпустить слайдер раньше времени, ухудшая показатель точности
                Debug.Log("here");
                CurrSlideAct = false;
                LevelController.LC.NoteMissed(note);
                
            }

        }
        if(note.tag == "Note")//блок обработки нот
        {
            if (CurrNotePressed)
            {
                LevelController.LC.NotePressed(note);
            }
            
        }
    }
    private void OnTriggerExit2D(Collider2D note)
    {
        if(note.tag == "Tile")
        {
            LevelController.LC.NoteMissed(note);
        }
        if(note.tag == "Slider" && CurrSlideAct == true)
        {
            if (CurrSlidePressed)
            {
                //Debug.Log("here");
                //удаление ноты (будет более подробная обработка нажатия)
                LevelController.LC.NotePressed(note);
            }
            else
            {
                //удаление ноты (будет более подробная обработка нажатия)
                LevelController.LC.NoteMissed(note);
            }
        }
    }
}
