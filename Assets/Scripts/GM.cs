using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
public class GM : MonoBehaviour
{
    int i_place;
    bool inputingName = false;
    private string[] cheatCode;
    public int index;
    int on_level;
    public GameObject greenMonsterFab;
    float prevSpawned;
    float greenMonsterDelay = 10f;
    public GameObject[] levels;
    public int level_i = 0;
    public int lives = 4;
    public int bricks;
    public float resetDelay = 5f;
    public Text livesText;
    public Text scoreText;
    public GameObject gameOver;
    public GameObject youWon;
    public GameObject NameInput;
    public InputField nameIn;
    public GameObject paddle;
    public GameObject deathParticles;
    public GameObject ColParticle;
    public GameObject heartsPrefab;
    public GameObject DieHeartParticles;
    public GameObject pausedText;
    public static GM instance = null;
    public Button save;
    bool paused = false;
    private GameObject clonePaddle;
    public bool boss = false;
    float boss_hp = 100;
    public GameObject bossFab;
    int max_greens = 2;
    //power up

    // Use this for initialization
    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        Setup();

    }
    void Start()
    {
        prevSpawned = Time.time;
        on_level = 1;
        PlayerPrefs.SetInt("Score", 0);
        cheatCode = new string[] { "b", "o", "o", "m" };
        index = 0;
    }
    void Update()
    {
        if (!boss)
        {
            if (!inputingName)
            {
                scoreText.text = "Score:\n" + PlayerPrefs.GetInt("Score");
                if (!paused)
                {
                    GameObject[] gos = GameObject.FindGameObjectsWithTag("GreenMonster");
                    //print(gos.Length);
                    if (gos.Length < max_greens)
                    {
                        if (prevSpawned <= Time.time)
                        {
                            prevSpawned = Time.time + greenMonsterDelay;
                            Instantiate(greenMonsterFab, new Vector3(-11, 9, 0), Quaternion.identity);
                        }
                    }
                    // Check if any key is pressed
                    if (Input.anyKeyDown)
                    {
                        // Check if the next key in the code is pressed
                        if (Input.GetKeyDown(cheatCode[index]))
                        {
                            // Add 1 to index to check the next key in the code
                            index++;
                        }
                        // Wrong key entered, we reset code typing
                        else
                        {
                            index = 0;
                        }
                    }

                    // If index reaches the length of the cheatCode string, 
                    // the entire code was correctly entered
                    if (index == cheatCode.Length)
                    {
                        if (level_i != 5)
                        {

                            //Debug.Log("boom");

                            List<GameObject> brickslist = new List<GameObject>();
                            foreach (Transform child in GameObject.Find(levels[level_i].name + "(Clone)").transform)
                            {
                                brickslist.Add(child.gameObject);
                            }
                            brickslist.ForEach(child => Destroy(child));

                            Instantiate(ColParticle, new Vector3(0, 3, 0), Quaternion.identity);
                            Instantiate(ColParticle, new Vector3(-3, 3, 0), Quaternion.identity);
                            Instantiate(ColParticle, new Vector3(3, 3, 0), Quaternion.identity);
                            Instantiate(ColParticle, new Vector3(-6, 3, 0), Quaternion.identity);
                            Instantiate(ColParticle, new Vector3(6, 3, 0), Quaternion.identity);
                            //Destroy(GameObject.Find(levels[level_i].name + "(Clone)"));
                            GameObject[] monsters = GameObject.FindGameObjectsWithTag("GreenMonster");
                            for (int i = 0; i < monsters.Length; i++)
                            {
                                Destroy(monsters[i]);
                            }

                            //bricks = 0;
                            //CheckGameOver();
                            // Cheat code successfully inputted!
                            // Unlock crazy cheat code stuff
                            index = 0;
                        }
                    }
                }
            }
            else
            {

                Cursor.visible = true;
            }
        }
        else
        {
            //BOSS 
            scoreText.text = "Score:\n" + PlayerPrefs.GetInt("Score");

        }
        if (!inputingName) {

            if (Input.GetKeyDown("r"))
            {
                paused = false;
                Time.timeScale = 1f;
                SceneManager.LoadScene("Level");

            }
            if (Input.GetKeyDown("p"))
            {
                paused = togglePause();
                if (paused)
                {

                    pausedText.SetActive(true);
                }
                else
                {
                    pausedText.SetActive(false);
                }

            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Time.timeScale = 1f;
                Cursor.visible = true;
                SceneManager.LoadScene("Menu");
            }
        }
    }
    public void Setup()
    {
        clonePaddle = Instantiate(paddle, new Vector3(0, -8.5f), Quaternion.identity) as GameObject;
        Instantiate(heartsPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        bricks = levels[level_i].transform.childCount;
        foreach (Transform child in levels[level_i].transform)
        {
            //print(child.gameObject.tag);
            if (child.gameObject.tag == "IndestructibleBrick")
            {
                bricks--;
            }
        }
        Instantiate(levels[level_i], new Vector3(levels[level_i].transform.position.x, levels[level_i].transform.position.y, 0), Quaternion.identity);


    }

    void CheckGameOver()
    {

        if (!inputingName)
        {
            if (!boss)
            {
                if (bricks < 1)
                {
                    if (youWon != null)
                    {
                        youWon.SetActive(true);
                    }
                    foreach (GameObject a in GameObject.FindGameObjectsWithTag("Ball"))
                    {
                        Destroy(a);
                    }
                    Invoke("LoadLevel", resetDelay);

                }
            }
            if (lives == -1)
            {

                /*foreach (GameObject child in GameObject.FindGameObjectsWithTag("PowerUp"))
                {
                    Destroy(child);
                }*/
                Cursor.visible = true;
                gameOver.SetActive(true);
                Invoke("CheckHiScore", resetDelay);
            }
        }
    }
    

    public void LoseLife()
    {
        if (!inputingName)
        {
            lives--;
            if (lives >= 0)
            {
                GameObject hearts = GameObject.Find("Hearts(Clone)");
                List<GameObject> children = new List<GameObject>();
                foreach (Transform child in hearts.transform)
                {
                    children.Add(child.gameObject);
                }
                Instantiate(DieHeartParticles, children[children.Count - 1].transform.position, Quaternion.identity);
                children[children.Count - 1].transform.localScale = new Vector3(8f, 8f, 8f);
                Destroy(children[children.Count - 1], 1f);
            }
            livesText.text = "Lives: " + lives;
            Instantiate(ColParticle, clonePaddle.transform.position - new Vector3(0, 0.88f, 0), Quaternion.identity);
            //Destroy(GameObject.Find("Ball"));
            Destroy(clonePaddle);
            Invoke("SetupPaddle", resetDelay);
            CheckGameOver();


        }
    }

    void SetupPaddle()
    {
        if (!inputingName)
        {
            clonePaddle = Instantiate(paddle, new Vector3(0, -8.5f), Quaternion.identity) as GameObject;
        }

    }

    public void DestroyBrick()
    {
        bricks--;
        CheckGameOver();
    }
    void LoadLevel()
    {
        level_i++;
        foreach (GameObject child in GameObject.FindGameObjectsWithTag("PowerUp"))
        {
            Destroy(child);
        }
        if (level_i == 5)
        {
            youWon.SetActive(false);
            boss = true;
            LoadBoss();
            Destroy(clonePaddle);
            clonePaddle = Instantiate(paddle, new Vector3(0, -8.5f), Quaternion.identity) as GameObject;
            //SceneManager.LoadScene("Level");
        }
        else
        {
            on_level++;
            youWon.SetActive(false);
            Destroy(GameObject.Find(levels[level_i-1].name + "(Clone)"));
            GameObject[] monsters = GameObject.FindGameObjectsWithTag("GreenMonster");
            for (int i = 0; i < monsters.Length; i++)
            {
                Destroy(monsters[i]);
            }
            bricks = levels[level_i].transform.childCount;
            foreach (Transform child in levels[level_i].transform)
            {
                //print(child.gameObject.tag);
                if (child.gameObject.tag == "IndestructibleBrick")
                {
                    bricks--;
                }
            }
            Instantiate(levels[level_i], new Vector3(levels[level_i].transform.position.x, levels[level_i].transform.position.y, 0), Quaternion.identity);
            Destroy(GameObject.Find("Ball"));
            Destroy(GameObject.Find("Paddle(Clone)"));
            SetupPaddle();
        }
    }
    bool togglePause()
    {
        if (Time.timeScale == 0f)
        {
            Time.timeScale = 1f;
            return (false);
        }
        else
        {
            Time.timeScale = 0f;
            return (true);
        }
    }
    HiScore[] hiscores = new HiScore[10];
    void CheckHiScore()
    {
        if (boss) {

            Destroy(GameObject.Find("Boss(Clone)"));
        }
        scoreText.text = "";
        for (int i = 0; i < 10; i++)
        {
            hiscores[i] = new HiScore();
            hiscores[i].rank = PlayerPrefs.GetInt("Rank_" + i);
            hiscores[i].level = PlayerPrefs.GetInt("Level_" + i);
            hiscores[i].score = PlayerPrefs.GetInt("hiScore_" + i);
            hiscores[i].name = PlayerPrefs.GetString("Name_" + i);
            //print(i+". "+hiscores[i].ToString());
        }
        int curr_Score = PlayerPrefs.GetInt("Score");
        for (int i = 0; i < 10; i++)
        {
            if (hiscores[i].score <= curr_Score)
            {

                for (int j = hiscores.Length - 2; j >= i; j--)
                {
                    hiscores[j + 1] = hiscores[j];
                }

                hiscores[i] = new HiScore();
                hiscores[i].level = on_level;
                hiscores[i].score = curr_Score;


                i_place = i;
                inputName();
                i = 20;

            }
        }
        if (!inputingName)
        {
            
            SceneManager.LoadScene("Menu");
        }


    }
    void inputName()
    {
        
        Cursor.visible = true;
        NameInput.SetActive(true);
        GameObject.Find("RankText").GetComponent<Text>().text = "Rank: " + PlayerPrefs.GetInt("Rank_" + i_place);
        GameObject.Find("LevelText").GetComponent<Text>().text = "Level: " + hiscores[i_place].level;
        GameObject.Find("HiScoreText").GetComponent<Text>().text = "Score: " + hiscores[i_place].score;
        save = GameObject.Find("SaveScore").GetComponent<Button>();
        save.onClick.AddListener(SaveName);
        nameIn.text = "Jesus";
        inputingName = true;
    }
    void SaveName()
    {
        string name;
        if (nameIn.text == "")
        {
            name = "Null";
        }
        else
        {
            name = nameIn.text;
        }
        hiscores[i_place].name = name;
        for (int i = 0; i < 10; i++)
        {
            PlayerPrefs.SetInt("Rank_" + i, i + 1);
            PlayerPrefs.SetInt("Level_" + i, hiscores[i].level);
            PlayerPrefs.SetInt("hiScore_" + i, hiscores[i].score);
            PlayerPrefs.SetString("Name_" + i, hiscores[i].name);

        }
        SceneManager.LoadScene("Menu");
    }
    void LoadBoss()
    {
        Instantiate(bossFab);
    }
    public void TakeBossLife()
    {
        boss_hp -= 2;
        if (boss_hp <= 80 && GameObject.Find("Body4") != null)
        {
            Destroy(GameObject.Find("Body4"));
        }
        if (boss_hp <= 60 && GameObject.Find("Body3") != null)
        {
            Destroy(GameObject.Find("Body3"));
        }
        if (boss_hp <= 40 && GameObject.Find("Body2") != null)
        {
            Destroy(GameObject.Find("Body2"));
        }
        if (boss_hp <= 20 && GameObject.Find("Body1") != null)
        {
            Destroy(GameObject.Find("Body1"));
        }

        if (boss_hp <= 0)
        {
            boss = false;
            max_greens = 30;
            greenMonsterDelay = 0.1f;
            foreach (Transform child in GameObject.Find("Boss(Clone)").transform)
            {
                Instantiate(ColParticle, child.transform.position, Quaternion.identity);
            }
            Destroy(GameObject.Find("Boss(Clone)"));
            Invoke("CheckHiScore", 1f);
        }
    }
}

class HiScore
{
    public int rank { set; get; }
    public int level { set; get; }
    public int score { set; get; }
    public string name { set; get; }

    public override string ToString()
    {
        return "Rank: " + rank + ", Level: " + level + ", Score: " + score + ", Name: " + name;
    }
}