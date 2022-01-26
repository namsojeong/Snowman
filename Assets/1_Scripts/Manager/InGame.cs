using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Experimental.Rendering.Universal;

public class InGame : MonoBehaviour
{
    [SerializeField]
    GameObject angelInven;
    [SerializeField]
    GameObject stoneInven;
    [SerializeField]
    GameObject plusScore;

    public static InGame Instance;

    public GameObject player;

    public Vector2 targetBullet = Vector2.zero;

    public float snowball = 0; //눈 개수
    public float playerScale = 0.6f;

    bool isStone = false;
    bool isAngel = false;
    public bool isLighting = false;
    public bool haveAngel = false;
    public bool haveStone = false;

    int lightTime = 0;
    int minusTime = 0;

    private float playerPlusScale = 0.0035f; //플레이어 움직일 때 초당 증가하는 크기
    private float playerMinusScale = 0.2f; //플레이어 발사할 때 감소하는 크기



    private void Awake()
    {
        Instance = this;
        if (Instance != null)
        {
            Instance = GetComponent<InGame>();
        }
    }
    private void Update()
    {
        CheckScore();
    }

    //총알 줄어들기
    public void SnowBall()
    {
        snowball -= 1;
    }

    //플레이어 크기 바꾸기
    public void PlayerScale(bool isBig)
    {
        if (isBig)
        {
            playerScale += playerPlusScale;
            if (playerScale >= 1.6f)
            {
                playerScale = 1.6f;
            }
        }
        else
        {
            playerScale -= playerMinusScale;
            if (playerScale <= 0.6f)
            {
                playerScale = 0.6f;
            }
        }
    }

    void PlayerMinusScale()
    {
        minusTime++;
        playerScale -= 0.01f;
        if (playerScale <= 0.6f)
        {
            playerScale = 0.6f;
            minusTime = 0;
            CancelInvoke("PlayerMinusScale");
            
        }
    }

    public void TimeCheck()
    {
        InvokeRepeating("PlayerMinusScale", 1f, 0.0005f);
        if(minusTime>=10)
        {
            minusTime = 0;
            CancelInvoke("PlayerMinusScale");
        }
    }
    public void OnStart()
    {
            InvokeRepeating("SpawnFoot", 0f, 2.5f);
    }

    //점수 확인
    void CheckScore()
    {
        if (GameManager.Instance.score == 11)
        {
            if (isLighting) return;
            isLighting = true;
            InvokeRepeating("SpawnLight", 0f, Random.Range(5f, 30f));
        }
        if (GameManager.Instance.score == 6)
        {
            if (isStone) return;
            isStone = true;
            InvokeRepeating("SpawnStone", 0f, 3f);
            InvokeRepeating("SpawnAngel", 0f, 3f);
        }
    }

    //빛 스폰
    private void SpawnLight()
    {
        lightTime++;
        if (lightTime >= 5)
        {
            CancelInvoke("SpawnLight");
            lightTime = 0;
            isLighting = false;
        }
        GameObject light = ObjectPool.Instance.GetObject(PoolObjectType.LIGHT);
        light.transform.position = new Vector3(Random.Range(-27.8f, 27.8f), Random.Range(-12.5f, 12.5f), 0f);
    }

    //눈천사 아이템 스폰
    private void SpawnAngel()
    {
        GameObject angel = ObjectPool.Instance.GetObject(PoolObjectType.ANGEL);
        angel.transform.position = new Vector3(Random.Range(-27.8f, 27.8f), Random.Range(-12.5f, 12.5f), 0f);
    }

    //돌 아이템 스폰
    private void SpawnStone()
    {
        GameObject stone = ObjectPool.Instance.GetObject(PoolObjectType.STONE);
        stone.transform.position = new Vector3(Random.Range(-27.8f, 27.8f), Random.Range(-12.5f, 12.5f), 0f);
    }

    //발 스폰
    public void SpawnFoot()
    {
        GameObject foot = ObjectPool.Instance.GetObject(PoolObjectType.FOOT);
        foot.transform.position = player.transform.position;
    }

    public void SpawnText()
    {
        //plusScore.transform.position = Camera.main.transform.position;
        plusScore.transform.position = new Vector3(Random.Range(Camera.main.transform.position.x-1f, Camera.main.transform.position.x + 1f), Random.Range(Camera.main.transform.position.y - 1f, Camera.main.transform.position.y + 1f), 0);
        plusScore.SetActive(true);
    }

    //게임 리셋
    public void Reset()
    {
        CancelInvoke("SpawnLight");
        CancelInvoke("SpawnFoot");
        CancelInvoke("SpawnStone");
        CancelInvoke("SpawnAngel");
        isLighting = false;
        isStone = false;
        isAngel = false;
        haveStone = false;

        ObjectPool.Instance.ResetObj();
        PlayerPrefs.SetInt("HIGHSCORE", GameManager.Instance.highScore);
        PlayerPrefs.SetInt("SCORE", GameManager.Instance.score);

        snowball = 0;
        playerScale = GameManager.Instance.playerInitScale;
        player.transform.localScale = new Vector2(GameManager.Instance.playerInitScale, GameManager.Instance.playerInitScale);
        player.transform.position = new Vector2(0f, 0f);

        InvenAngel(false);
        InvenStone(false);

        plusScore.SetActive(false);

        GameManager.Instance.score = 0;
    }

    //인벤토리 에인젤
    public void InvenAngel(bool action)
    {
        haveAngel = action;
        angelInven.SetActive(action);
    }

    //인벤토리 스톤
    public void InvenStone(bool action)
    {
        haveStone = action;
        stoneInven.SetActive(action);
    }

    public void AngelClick()
    {
        if (!haveAngel)
        {
            return;
        }
        InvenAngel(false);
        SoundM.Instance.SoundOn("SFX", 0);
        GameObject trap = ObjectPool.Instance.GetObject(PoolObjectType.ANGELITEM);
        trap.transform.position = player.transform.position;
    }

}
