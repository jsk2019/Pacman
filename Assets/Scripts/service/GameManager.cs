using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            return _instance;
        }
    }

    public GameObject pacman;
    public GameObject blinky;
    public GameObject clyde;
    public GameObject inky;
    public GameObject pinky;
    public GameObject startPanel;
    public GameObject gamePanel;
    public GameObject startCountDownPrefab;
    public GameObject gameoverPrefab;
    public GameObject winPrefab;
    public AudioClip startClip;
    public Text remainText;
    public Text nowText;
    public Text scoreText;
    public Text finalScore;
    
    public bool isSuperPacman = false;
    public List<int> usingIndex = new List<int>();
    public List<int> rawIndex = new List<int> { 0, 1, 2, 3 };
    private List<GameObject> pacdotGos = new List<GameObject>();
    private int pacdotNum = 0;
    private int nowEat = 0;
    public int score = 0;
    private float timer = 5f;
    public GameObject time;
    private bool isStart = false;


    private void Awake()
    {
        _instance = this;
        Screen.SetResolution(1024, 768, false);
        int tempCount = rawIndex.Count;

        for (int i = 0; i < tempCount; i++)
        {
            int tempIndex = Random.Range(0, rawIndex.Count);
            usingIndex.Add(rawIndex[tempIndex]);
            rawIndex.RemoveAt(tempIndex);
        }

        foreach (Transform t in GameObject.Find("Maze").transform)
        {
            pacdotGos.Add(t.gameObject);
        }

        pacdotNum = GameObject.Find("Maze").transform.childCount;
    }

    private void Start()
    {
        SetGameState(false);
    }


    private void Update()
    {

        if (nowEat == pacdotNum && pacman.GetComponent<PacmanMove>().enabled != false)
        {
            gamePanel.SetActive(false);
            Instantiate(winPrefab);
            StopAllCoroutines();
            SetGameState(false);
        }

        if (nowEat == pacdotNum)
        {
            if (Input.anyKeyDown)
            {
                SceneManager.LoadScene(0);
            }
        }

        if (gamePanel.activeInHierarchy)
        {
            remainText.text = "Remain:\n\n" + (pacdotNum - nowEat);
            nowText.text = "Eaten:\n\n" + nowEat;
            scoreText.text = "Score:\n\n" + score;
        }
        if (isStart)
        {
              Invoke("startTime", 4f);
            //startTime();
        }
    }

    public void OnStartButton()
    {
        StartCoroutine(PlayStartCountDown());

        AudioSource.PlayClipAtPoint(startClip, new Vector3(0, 0, -5));

        startPanel.SetActive(false);

        isStart = true;
    }

    public void OnExitButton()
    {
        Application.Quit();
    }


    IEnumerator PlayStartCountDown()
    {
        GameObject go = Instantiate(startCountDownPrefab);
        yield return new WaitForSeconds(4f);
        Destroy(go);
        SetGameState(true);

        Invoke("CreateSuperPacdot", 10f);
        Invoke("CreateMidPacdot", 15f);

        gamePanel.SetActive(true);
        GetComponent<AudioSource>().Play();
    }

    public void OnEatPacdot(GameObject go)
    {
        nowEat++;
        score += 100;
        GhostMove.nowScore += 100;
        pacdotGos.Remove(go);
    }

    public void OnEatSuperPacdot()
    {
        score += 200;

        GhostMove.nowScore += 200;

        Invoke("CreateSuperPacdot", 10f);

        isSuperPacman = true;

        FreezeEnemy();

        StartCoroutine(RecoveryEnemy());
    }


    public void OnEatMidPacdot()
    {
        score += 150;

        GhostMove.nowScore += 150;

        Invoke("CreateMidPacdot", 15f);

        timer += 20;
    }

    IEnumerator RecoveryEnemy()
    {
        yield return new WaitForSeconds(3f);

        DisFreezeEnemy();

        isSuperPacman = false;
    }

    private void CreateSuperPacdot()
    {
        if (pacdotGos.Count < 5)
        {
            return;
        }

        int tempIndex = Random.Range(0, pacdotGos.Count);

        pacdotGos[tempIndex].transform.localScale = new Vector3(7, 7, 7);

        pacdotGos[tempIndex].GetComponent<Pacdot>().isSuperPacdot = true;
    }


    private void CreateMidPacdot()
    {
        if (pacdotGos.Count < 5)
        {
            return;
        }

        int tempIndex = Random.Range(0, pacdotGos.Count);

        pacdotGos[tempIndex].transform.localScale = new Vector3(3, 3, 3);

        pacdotGos[tempIndex].GetComponent<Pacdot>().isMidPacdot = true;
    }

    private  void FreezeEnemy()
    {
        blinky.GetComponent<GhostMove>().enabled = false;
        clyde.GetComponent<GhostMove>().enabled = false;
        inky.GetComponent<GhostMove>().enabled = false;
        pinky.GetComponent<GhostMove>().enabled = false;

        blinky.GetComponent<SpriteRenderer>().color = new Color(0.7f, 0.7f, 0.7f, 0.7f);
        clyde.GetComponent<SpriteRenderer>().color = new Color(0.7f, 0.7f, 0.7f, 0.7f);
        inky.GetComponent<SpriteRenderer>().color = new Color(0.7f, 0.7f, 0.7f, 0.7f);
        pinky.GetComponent<SpriteRenderer>().color = new Color(0.7f, 0.7f, 0.7f, 0.7f);
    }


    private void DisFreezeEnemy()
    {
        blinky.GetComponent<GhostMove>().enabled = true;
        clyde.GetComponent<GhostMove>().enabled = true;
        inky.GetComponent<GhostMove>().enabled = true;
        pinky.GetComponent<GhostMove>().enabled = true;

        blinky.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        clyde.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        inky.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        pinky.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
    }


    private void SetGameState(bool state)
    {
        pacman.GetComponent<PacmanMove>().enabled = state;
        blinky.GetComponent<GhostMove>().enabled = state;
        clyde.GetComponent<GhostMove>().enabled = state;
        inky.GetComponent<GhostMove>().enabled = state;
        pinky.GetComponent<GhostMove>().enabled = state;
    }


    private void startTime()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            time.GetComponent<Text>().text = timer.ToString("00");
        }

        else
        {
            GameManager.Instance.gamePanel.SetActive(false);
            Instantiate(GameManager.Instance.gameoverPrefab);
            FreezeEnemy();
            GameObject.Destroy(pacman);
            UICon.nowScore = score;
            finalScore.text = "Score："+score;
            finalScore.color = new Color(1,1,1,1);
            //Instantiate(GameManager.Instance.finallyScroe);
            Invoke("ReStart", 3f);
            SceneManager.LoadScene("Score");
        }
    }

    private void ReStart()
    {
        SceneManager.LoadScene(0);
    }

}
