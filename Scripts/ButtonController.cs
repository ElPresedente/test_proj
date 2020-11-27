using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{

    private SpriteRenderer theSR;
    //public Sprite defaultImage;
    //public Sprite pressedImage;
    private enum TileStateWord
    {
        NotActive,
        ButtonBlocked,
        ButtonUnblocked,
        TilePressed,
        SliderPressed,//отдельное состояние под слайдер для его отслеживания
    }

    public KeyCode keyToPress;
    public Color defaultColor = new Color(1f, 1f, 1f);
    public Color pressedColor = new Color(0.7f, 0.7f, 0.7f, 0.7f);
    public bool ButtonPressed; //нажата кнопка сейчас или нет (хотя ля, можно же клавишу отслеживать...)
    private float CurrSlideTime;
    private TileStateWord TSW;
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
            TSW  = TileStateWord.ButtonBlocked;//то она блокируется до ее отжатия и повторного нажатия
        }//крч не выйдет просто зажать все кнопки и выиграть
        else
        {
            TSW = TileStateWord.ButtonUnblocked;
        }
        if (note.gameObject.CompareTag("Slider")) {
            
            CurrSlideTime = LevelController.LC.SliderTimeDelay;
            //Debug.Log(CurrSlideTime);//сброс переменных про слайдерам
            
        }
        if (note.gameObject.CompareTag("End"))
        {
            LevelController.LC.EndMap();
        }


    }

    private void OnTriggerStay2D(Collider2D note)
    {
        //Debug.Log(TSW);
        if (!ButtonPressed && TSW == TileStateWord.ButtonBlocked)
        {
            TSW = TileStateWord.ButtonUnblocked; //снятие блокировки
        }
        if (TSW == TileStateWord.ButtonUnblocked && ButtonPressed)//при разблокированной и нажатой кнопке активируем тайл
        {
            TSW = TileStateWord.TilePressed; 
        }
        
        if (note.gameObject.CompareTag("Slider")) //блок обработки слайдеров
        {
            //Debug.Log("72");
            if (TSW < TileStateWord.TilePressed)//если слайдер не нажат
            {
                //Debug.Log("75");
                CurrSlideTime -= Time.deltaTime; //даем игроку время на нажатие слайдера
                if (CurrSlideTime <= 0) //если оно истекает - промах
                {
                    //Debug.Log("79");
                    TSW = TileStateWord.NotActive;
                    LevelController.LC.NoteMissed(note);
                }
            }
            else
            {
                //Debug.Log("86");
                TSW = TileStateWord.SliderPressed;// когда игрок зажал слайдер - начинаем это отслеживать
                
            };
            if(TSW == TileStateWord.SliderPressed && !ButtonPressed) //если слайдер отпущен раньше времени - промах
            {                                      //в будущем нужно дать возможность отпустить слайдер раньше времени, ухудшая показатель точности
                //Debug.Log("91");
                TSW = TileStateWord.NotActive;
                LevelController.LC.SliderUnpressed(note);
            }

        }
        if(note.gameObject.CompareTag("Note"))//блок обработки нот
        {
            
            if (TSW == TileStateWord.TilePressed)
            {
                //Debug.Log("103");
                TSW = TileStateWord.NotActive;
                LevelController.LC.NotePressed(note);
            }
            
        }
    }
    private void OnTriggerExit2D(Collider2D note)
    {
        if(note.gameObject.CompareTag("Note") && TSW > 0)
        {
            LevelController.LC.NoteMissed(note);
        }
        if(note.gameObject.CompareTag("Slider") && TSW > 0)
        {
            //Debug.Log("117");
            if (TSW == TileStateWord.SliderPressed)
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
