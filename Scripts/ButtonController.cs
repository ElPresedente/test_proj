using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour {

    private SpriteRenderer theSR;
    //public Sprite defaultImage;
    //public Sprite pressedImage;

    public KeyCode keyToPress;
    public Color defaultColor = new Color(1f, 1f, 1f);
    public Color pressedColor = new Color(0.7f, 0.7f, 0.7f, 0.7f);
    public bool pressed; //нажата кнопка сейчас или нет (хотя ля, можно же клавишу отслеживать...)
    public bool blocked;
    void Start()
    {
        
        theSR = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(keyToPress)) {
            theSR.color = pressedColor; //немного "анимации" для нажимающихся кнопОчек
            pressed = true;
        }
        if (Input.GetKeyUp(keyToPress)) {
            theSR.color = defaultColor;
            pressed = false;
        }
        
    }
    private void OnTriggerEnter2D(Collider2D note)
    {
        if (pressed)//если во время "входа" плашки кнопка нажата
        {
            blocked = true;//то она блокируется то ее отжатия и повторного нажатия
        }//крч не выйдет просто зажать все кнопки и выиграть
        
        
    }
    private void OnTriggerStay2D(Collider2D note)
    {
        if (!pressed && blocked)
        { 
            blocked = false; //снятие блокировки
        }
        if (!blocked && pressed)
        {
            note.gameObject.SetActive(false);//удаление ноты (будет более подробная обработка нажатия)
        }
    }
}
