using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class ButtonOnClicks : MonoBehaviour
{
    Button play;
    Button options;
    Button info;
    Button hiscore;
    Button history;
    Button quit;
    public GameObject can;
    GameObject buttons;
    Vector3 endPositionButton;
    bool movebuttons = false;
    bool buttonsMoved = false;

    public GameObject gameoptObj;
    public GameObject infoObj;
    public GameObject hiscoreObj;
    public GameObject historyObj;

    public Button SoundOn;
    public Button SoundOff;
    public Button MusicOn;
    public Button MusicOff;
    public Button Low;
    public Button Med;
    public Button High;

    Vector3 endPositionRight;
    Vector3 outPositionRight;
    bool moveRight = false;
    bool objectOnRight = false;
    GameObject movingObj;
    GameObject prev;
    float secondsPassed;
    float secondsPassedDelay = 0.7f;


    bool moveCan = false;

    public Text rankText;
    public Text levelText;
    public Text scoreText;
    public Text nameText;



    // Use this for initialization
    void Start()
    {
        Cursor.visible = true;
        rankText.text = "";
        levelText.text = "";
        scoreText.text = "";
        nameText.text = "";

        for (int i = 0; i < 10; i++)
        {
            rankText.text = rankText.text + PlayerPrefs.GetInt("Rank_" + i) + "\n";
            levelText.text = levelText.text + PlayerPrefs.GetInt("Level_" + i) + "\n";
            scoreText.text = scoreText.text + PlayerPrefs.GetInt("hiScore_" + i) + "\n";
            nameText.text = nameText.text + PlayerPrefs.GetString("Name_" + i) + "\n";
            //Debug.Log("Rank: " + PlayerPrefs.GetInt("Rank_" + i) +
            //            ", Level: " + PlayerPrefs.GetInt("Level_" + i) +
            //            ", Score: " + PlayerPrefs.GetInt("hiScore_" + i) +
            //            ", Name: " + PlayerPrefs.GetString("Name_" + i));
        }

        SoundOn.onClick.AddListener(SoundOnFunc);
        SoundOff.onClick.AddListener(SoundOffFunc);
        if (PlayerPrefs.GetInt("Sound") == 0)
        {
            SoundOff.interactable = false;
        }
        else
        {
            SoundOn.interactable = false;
        }
        MusicOn.onClick.AddListener(MusicOnFunc);
        MusicOff.onClick.AddListener(MusicOffFunc);
        if (PlayerPrefs.GetInt("Music") == 0)
        {
            MusicOff.interactable = false;
        }
        else
        {
            MusicOn.interactable = false;
        }

        Low.onClick.AddListener(LowFunc);
        Med.onClick.AddListener(MedFunc);
        High.onClick.AddListener(HighFunc);
        if (PlayerPrefs.GetInt("MouseSpeed") == 0)
        {
            Low.interactable = false;
        }
        else if (PlayerPrefs.GetInt("MouseSpeed") == 1)
        {
            Med.interactable = false;
        }
        else
        {
            High.interactable = false;
        }


        play = GameObject.Find("Play").GetComponent<Button>();
        play.onClick.AddListener(PlayOnClick);
        options = GameObject.Find("Options").GetComponent<Button>();
        options.onClick.AddListener(OptionsOnClick);
        info = GameObject.Find("Info").GetComponent<Button>();
        info.onClick.AddListener(InfoOnClick);
        hiscore = GameObject.Find("HiScore").GetComponent<Button>();
        hiscore.onClick.AddListener(HiscoreOnClick);
        history = GameObject.Find("History").GetComponent<Button>();
        history.onClick.AddListener(HistoryOnClick);
        quit = GameObject.Find("Quit").GetComponent<Button>();
        quit.onClick.AddListener(QuitOnClick);
        buttons = GameObject.Find("Buttons");

        endPositionButton = new Vector3(-40, buttons.transform.position.y, 90);
        endPositionRight = new Vector3(0, buttons.transform.position.y, 90);
        outPositionRight = GameObject.Find("GameOptionsObject").transform.position;



    }

    void SoundOnFunc()
    {
        SoundOn.interactable = false;
        SoundOff.interactable = true;
        PlayerPrefs.SetInt("Sound", 1);
    }
    void SoundOffFunc()
    {
        SoundOn.interactable = true;
        SoundOff.interactable = false;
        PlayerPrefs.SetInt("Sound", 0);
    }
    void MusicOnFunc()
    {
        MusicOn.interactable = false;
        MusicOff.interactable = true;
        PlayerPrefs.SetInt("Music", 1);
        foreach (AudioSource a in GameObject.Find("MusicPlayer").GetComponents<AudioSource>())
        {
            a.mute = false;
        }
    }
    void MusicOffFunc()
    {
        MusicOn.interactable = true;
        MusicOff.interactable = false;
        PlayerPrefs.SetInt("Music", 0);
        foreach (AudioSource a in GameObject.Find("MusicPlayer").GetComponents<AudioSource>()) {
            a.mute = true;
        }
    }
    void LowFunc()
    {
        Low.interactable = false;
        Med.interactable = true;
        High.interactable = true;
        PlayerPrefs.SetInt("MouseSpeed", 0);
    }
    void MedFunc()
    {
        Low.interactable = true;
        Med.interactable = false;
        High.interactable = true;
        PlayerPrefs.SetInt("MouseSpeed", 1);
    }
    void HighFunc()
    {
        Low.interactable = true;
        Med.interactable = true;
        High.interactable = false;
        PlayerPrefs.SetInt("MouseSpeed", 2);
    }
    void PlayOnClick()
    {
        moveCan = true;
        secondsPassed = Time.time + secondsPassedDelay;
    }
    void Update()
    {
        if (moveCan)
        {
            can.transform.position = Vector3.Lerp(can.transform.position, new Vector3(1000, 0, 90), 1.5f * Time.deltaTime);
            if (Time.time >= secondsPassed)
            {
                moveCan = false;
                SceneManager.LoadScene("Level");
            }
        }
        //Debug.Log("moveRight: "+ moveRight+ ", objectOnRight: "+ objectOnRight);
        if (movebuttons)
        {
            buttons.transform.position = Vector3.Lerp(buttons.transform.position, endPositionButton, 4f * Time.deltaTime);

            if (endPositionButton == buttons.transform.position)
            {
                movebuttons = false;
            }
        }
        if (moveRight)
        {
            slideIn();
        }
    }
    void OptionsOnClick()
    {
        if (!buttonsMoved)
        {
            movebuttons = true;
            buttonsMoved = true;
        }
        if (!moveRight)
        {
            movingObj = GameObject.Find("GameOptionsObject");
            moveRight = true;
            secondsPassed = Time.time + secondsPassedDelay;
            play.interactable = true;
            options.interactable = false;
            info.interactable = true;
            hiscore.interactable = true;
            history.interactable = true;
            quit.interactable = true;
        }
    }
    void InfoOnClick()
    {
        if (!buttonsMoved)
        {
            movebuttons = true;
            buttonsMoved = true;
        }

        if (!moveRight)
        {
            movingObj = GameObject.Find("InformationObject");
            moveRight = true;
            secondsPassed = Time.time + secondsPassedDelay;
            play.interactable = true;
            options.interactable = true;
            info.interactable = false;
            hiscore.interactable = true;
            history.interactable = true;
            quit.interactable = true;
        }
    }
    void HiscoreOnClick()
    {
        if (!buttonsMoved)
        {
            movebuttons = true;
            buttonsMoved = true;
        }

        if (!moveRight)
        {
            movingObj = GameObject.Find("HighScoresObject");
            moveRight = true;
            secondsPassed = Time.time + secondsPassedDelay;

            play.interactable = true;
            options.interactable = true;
            info.interactable = true;
            hiscore.interactable = false;
            history.interactable = true;
            quit.interactable = true;
        }
    }
    void HistoryOnClick()
    {
        if (!buttonsMoved)
        {
            movebuttons = true;
            buttonsMoved = true;
        }
        if (!moveRight)
        {
            movingObj = GameObject.Find("HistoryObject");
            moveRight = true;
            secondsPassed = Time.time + secondsPassedDelay;

            play.interactable = true;
            options.interactable = true;
            info.interactable = true;
            hiscore.interactable = true;
            history.interactable = false;
            quit.interactable = true;
        }
    }
    void QuitOnClick()
    {
        Application.Quit();
    }
    // Update is called once per frame
    void slideIn()
    {
        if (objectOnRight)
        {
            slideOut();
        }
        else
        {
            movingObj.transform.position = Vector3.Lerp(movingObj.transform.position, endPositionRight, 6f * Time.deltaTime);
            if (Time.time >= secondsPassed)
            {
                moveRight = false;
                objectOnRight = true;
                prev = movingObj;
            }
        }

    }
    void slideOut()
    {
        //
        prev.transform.position = Vector3.Lerp(prev.transform.position, outPositionRight, 6f * Time.deltaTime);
        if (Time.time >= secondsPassed)
        {
            secondsPassed = Time.time + secondsPassedDelay;
            objectOnRight = false;
        }
    }
}
