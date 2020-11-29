using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

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
    public float MapDebug;
    public Text CurrentCombotxt;
    public Text MaxCombotxt;
    public Text CurrentScoretxt;
    //счетчики очков
    private int PerfectCounter = 0;
    private int GoodCounter = 0;
    private int NormalCounter = 0;
    private int MissedCounter = 0;
    //количество очков за категорию
    private double PerfectScore = 0;
    private double GoodScore = 0;
    private double NormalScore = 0;

    private int currentCombo;
    private int maxCombo;
    public double currentScore = 0;

    private int tilesNumber;
    private Transform StarHolder;
    void Awake()
    {
        LC = this;
    }
    private void Start()
    {
        StarHolder = transform.Find("StarHolder");
    }
    public void SetScoresAmount(int num)
    {
        tilesNumber = num-1;
        PerfectScore = 1000000.0 / (double)tilesNumber;
        GoodScore = PerfectScore * 0.75;
        NormalScore = PerfectScore * 0.5;
        Debug.Log(PerfectScore);
        Debug.Log(GoodScore);
        Debug.Log(NormalScore);
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
                if (MapDebug>0)
                {
                    Music.Play();
                    
                    Music.time += MapDebug;
                    ArrowHolder.AH.Debug(MapDebug);
                    ArrowHolder.AH.Debug(MusicDelay);
                }
                else
                {
                    Music.PlayDelayed(MusicDelay);
                }
            }
        }

        if (Input.GetKeyDown(pauseButton)) 
        { 
            Pause();
        }
    }
    public void SliderUnpressed(Collider2D note)
    {
        double yRange = note.gameObject.transform.position.y + (note.GetComponent<BoxCollider2D>().size.y * note.gameObject.transform.localScale.y) / 2.0;
        Debug.Log("unpressed " + note.tag + " " + yRange);
        if(yRange < 0.35 && yRange > 0.25)
        {
            NoteNormal();
        }
        else if(yRange <= 0.25 && yRange > 0.15)
        {
            NoteGood();
        }
        else if(yRange <= 0.15)
        {
            NotePerfect();
        }
        else
        {
            NoteMissed(note);
        }

    }
    public void NotePressed(Collider2D note)
    {
        double yRange = note.gameObject.transform.position.y + (note.GetComponent<BoxCollider2D>().size.y * note.gameObject.transform.localScale.y) / 2.0;
        if (note.gameObject.CompareTag("Slider"))
        {
            Debug.Log("pressed " + note.tag + " " + yRange);
            NotePerfect();//если слайдер не отпускали - все отлично
        }
        else if (note.gameObject.CompareTag("Note"))
        {
            Debug.Log("pressed " + note.tag + " " + yRange);
            yRange = Math.Abs(yRange);
            if (yRange < 0.25 && yRange > 0.15)
            {
                NoteNormal();
            }
            else if (yRange <= 0.15 && yRange > 0.09)
            {
                NoteGood();
            }
            else if (yRange <= 0.09)
            {
                NotePerfect();
            }
        }
        note.gameObject.SetActive(false);
        currentCombo++;
        DrawCombo();

    }
    public void NoteMissed(Collider2D note)
    {
        double yRange = note.gameObject.transform.position.y + (note.GetComponent<BoxCollider2D>().size.y * note.gameObject.transform.localScale.y) / 2.0;
        if (note.gameObject.CompareTag("Slider"))
        {
            Debug.Log("fail " + note.tag + " " + yRange);
            
        }
        else if (note.gameObject.CompareTag("Note"))
        {
            Debug.Log("fail " + note.tag + " " + yRange);
        }
        note.gameObject.SetActive(false);
        MissedCounter++;
        currentCombo = 0;
        DrawCombo();
    }
    public void Pause()
    {
        //ArrowHolder.AH.pauseChange(Music);
    }
    private void NotePerfect()
    {
        Debug.Log("perfect");
        PerfectCounter++;
        currentScore += PerfectScore;
        DrawScore();
    }
    private void NoteGood() {
        Debug.Log("good");
        GoodCounter++;
        currentScore += GoodScore;
        DrawScore();
    }
    private void NoteNormal()
    {
        Debug.Log("normal");
        NormalCounter++;
        currentScore += NormalScore;
        DrawScore();
    }
    public void EndMap()
    {
        Debug.Log("Perfect: "+ PerfectCounter +"\nGood: " + GoodCounter + "\nNormal: " + NormalCounter + "\nMissed: " + MissedCounter);
        DontDestroyOnLoad(gameObject);
        Loader.Load(Loader.Scene.ResultScene);
        Music.Stop();
    }
    private void DrawCombo()
    {
        if (currentCombo > maxCombo)
        {
            maxCombo = currentCombo;
            MaxCombotxt.text = "Max combo: " + maxCombo;
        }
        CurrentCombotxt.text = "Current combo: " + currentCombo;
    }
    private void DrawScore()
    {
        //Debug.Log(currentScore);
        int currentscore = (int)Math.Ceiling(currentScore);
        CurrentScoretxt.text = "Score: " + currentscore;
        ActivateStar(StarHolder, currentscore);
    }
    public int[] GetScores()
    {   //передаем значения, полученные в ходе игры
        int[] scoresArr = new int[5] { PerfectCounter, GoodCounter, NormalCounter, MissedCounter, (int)currentScore};
        gameObject.SetActive(false);
        return scoresArr;
    }
    private void ActivateStar(Transform StarHolder, int totalScore)
    {
        if (totalScore > 350000)
        {
            StarHolder.GetChild(0)
                      .GetComponent<SpriteRenderer>().color = new Color(1f, 0.84f, 0f);
        }
        if (totalScore > 500000)
        {
            StarHolder.GetChild(1)
                      .GetComponent<SpriteRenderer>().color = new Color(1f, 0.84f, 0f);
        }
        if (totalScore > 700000)
        {
            StarHolder.GetChild(2)
                      .GetComponent<SpriteRenderer>().color = new Color(1f, 0.84f, 0f);
        }
        if (totalScore > 850000)
        {
            StarHolder.GetChild(3)
                      .GetComponent<SpriteRenderer>().color = new Color(1f, 0.84f, 0f);
        }
        if (totalScore > 910000)
        {
            StarHolder.GetChild(4)
                      .GetComponent<SpriteRenderer>().color = new Color(1f, 0.84f, 0f);
        }
    }
}
