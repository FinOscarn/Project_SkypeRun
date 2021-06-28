using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public CanvasGroup sPanel, oPanel;

    // 오브젝트들
    public GameObject player;
    public GameObject InGame;
    public AudioSource bgm;
    public TMP_Text score;
    public TMP_Text count;
    public TMP_Text sCount;

    public TMP_Text best; // 그냥 문자
    public TMP_Text bestScore; // 얘가 베스트 스코어
    public TMP_Text shadowBestCore; // 얘는 백그라운드 그림자

    private int sscore = 0;
    private int bsscore;
    private float yPos;
    private GameObject shadow;
    private GameObject bBlock;
    private GameObject dLight;
    private GameObject tGenerator;
    private GameObject bTile;

    private bool gameStart = false;
    private bool isDeath = true;

    void Awake()
    {
        player = GameObject.Find("player");
        shadow = GameObject.Find("shadow");
        bBlock = GameObject.Find("baseBlock");
        dLight = GameObject.Find("Directional Light");
        tGenerator = GameObject.Find("tileGenerator");
        bTile = GameObject.Find("bTile");

        player.SetActive(false);
        shadow.SetActive(false);
        bBlock.SetActive(false);
        dLight.SetActive(false);
        tGenerator.SetActive(false);
        bTile.SetActive(false);

        sPanel = GameObject.Find("startPanel").GetComponent<CanvasGroup>();
        oPanel = GameObject.Find("overPanel").GetComponent<CanvasGroup>();

    }
    public void StartGame()
    {
        if(player == null)
        {
            Debug.Log("asd");
        }    
        sPanel.alpha = 0;
        sPanel.interactable = false;
        oPanel.blocksRaycasts = false;

        player.SetActive(true);
        shadow.SetActive(true);
        bBlock.SetActive(true);
        dLight.SetActive(true);
        tGenerator.SetActive(true);
        bTile.SetActive(true);

        score.enabled = true;
        count.enabled = true;
        sCount.enabled = true;
        bgm.enabled = true;

        bestScore.enabled = true;
        shadowBestCore.enabled = true;
        best.enabled = true;
        gameStart = true;

        bsscore = PlayerPrefs.GetInt("BestScore");

        bestScore.text = $"{bsscore}";
        shadowBestCore.text = $"{bsscore}";

    }
    public void EndGame()
    {
        isDeath = false;

        oPanel.alpha = 1;
        oPanel.interactable = true;
        oPanel.blocksRaycasts = true;

        player.SetActive(false);
        if (sscore > bsscore)
        {
            bsscore = sscore;
            PlayerPrefs.SetInt("BestScore", sscore);
            
        }
    }
    public void Restart()
    {
        SceneManager.LoadScene("SampleScene");
        sscore = 0;
    }
    
    IEnumerator Timer()
    {
        yield return new WaitForSeconds(0.1f);
        sscore += 2;
    }

    public void Death()
    {
        // 죽는 조건 : shadow와의 거리
        if ((shadow.transform.position.z > (player.transform.position.z + 5)) || (player.transform.position.y < -5))
        {
            bgm.enabled = false;
            EndGame();
        }
    }
    public void PosCheck()
    {
        yPos = player.transform.position.y;
    }
    
    void Update()
    {
        // 게임이 진행되고 있다면, 업데이트문 
        if (!gameStart) return;



        if (isDeath)
        {
            StartCoroutine("Timer");
            count.text = $"{sscore}";
            sCount.text = $"{sscore}";
        }

        Death();
    }
}
