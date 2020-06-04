using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GhostMove : MonoBehaviour
{
    public GameObject pacman;
    public GameObject blinky;
    public GameObject clyde;
    public GameObject inky;
    public GameObject pinky;
    public GameObject[] wayPointsGos;
    public static float speed;
    private List<Vector3> wayPoints = new List<Vector3>();
    private int index = 0;
    private Vector3 startPos;
    public Text finalScore;
    public static int nowScore;


    private void Start()
    {
        startPos = transform.position + new Vector3(0, 3, 0);
        LoadAPath(wayPointsGos[GameManager.Instance.usingIndex[GetComponent<SpriteRenderer>().sortingOrder - 2]]);
    }

    private void FixedUpdate()
    {

        if (transform.position != wayPoints[index])
        {
            Vector2 temp = Vector2.MoveTowards(transform.position, wayPoints[index], speed);
            GetComponent<Rigidbody2D>().MovePosition(temp);
        }

        else
        {
            index++;
            if (index >= wayPoints.Count)
            {
                index = 0;
                LoadAPath(wayPointsGos[Random.Range(0, wayPointsGos.Length)]);
            }
        }

        Vector2 dir = wayPoints[index] - transform.position;
        GetComponent<Animator>().SetFloat("DirX", dir.x);
        GetComponent<Animator>().SetFloat("DirY", dir.y);
    }

    private void LoadAPath(GameObject go)
    {

        wayPoints.Clear();

        foreach (Transform t in go.transform)
        {
            wayPoints.Add(t.position);
        }

        wayPoints.Insert(0, startPos);
        wayPoints.Add(startPos);
    }


    private void FreezeEnemy()
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "firePoint")
        {
            GameManager.Instance.score += 500;
            nowScore += 500;
        }
        if (collision.gameObject.name == "Pacman")
        {

            if (GameManager.Instance.isSuperPacman)
            {
                transform.position = startPos - new Vector3(0, 3, 0);
                index = 0;
                GameManager.Instance.score += 500;
                nowScore += 500;
            }

            else
            {
                collision.gameObject.SetActive(false);
                GameManager.Instance.gamePanel.SetActive(false);
                Instantiate(GameManager.Instance.gameoverPrefab);
                FreezeEnemy();
                finalScore.text = "Score：" + nowScore;
                UICon.nowScore = nowScore;
                finalScore.color = new Color(1, 1, 1, 1);
               
                GameObject.Destroy(pacman);
                Invoke("ReStart", 6f);
                SceneManager.LoadScene("Score");
            }
        }
    }

    private void ReStart()
    {
        SceneManager.LoadScene(0);
    }
}
