using UnityEngine;
using UnityEngine.UI;

public class InGame : MonoBehaviour
{
    public static InGame Instance;
    
    [SerializeField]
    Image[] invenAngel;
    [SerializeField]
    Image[] invenStone;

    [SerializeField]
    Button[] fireButton;
    [SerializeField]
    Image[] fireColor;


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

        //총알 버튼 클릭
        fireButton[0].onClick.AddListener(() => BulletButton("UP"));
        fireButton[1].onClick.AddListener(() => BulletButton("DOWN"));
        fireButton[2].onClick.AddListener(() => BulletButton("LEFT"));
        fireButton[3].onClick.AddListener(() => BulletButton("RIGHT"));
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
        if (minusTime >= 10)
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
        GameObject scoreText = ObjectPool.Instance.GetObject(PoolObjectType.PlusText);
        scoreText.transform.position = new Vector3(Random.Range(Camera.main.transform.position.x - 2f, Camera.main.transform.position.x + 2f), Random.Range(Camera.main.transform.position.y - 1.5f, Camera.main.transform.position.y + 1.5f), 0);
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

        InvenOn("STONE", false);
        InvenOn("ANGEL", false);
        FireColor(false);

        GameManager.Instance.score = 0;
    }

    //인벤토리 스톤
    public void InvenOn(string item, bool action)
    {
        if (item == "ANGEL")
        {
            haveAngel = action;
            invenAngel[1].gameObject.SetActive(action);
        }
        else if (item == "STONE")
        {
            haveStone = action;
            invenStone[1].gameObject.SetActive(action);
        }
    }

    //엔젤 버튼 클릭 시
    public void AngelClick()
    {
        if (!haveAngel)
        {
            return;
        }
        InvenOn("ANGEL", false);
        SoundM.Instance.SoundOn("SFX", 0);
        GameObject trap = ObjectPool.Instance.GetObject(PoolObjectType.ANGELITEM);
        trap.transform.position = player.transform.position;
    }


    //총알 발사 버튼 
    public void BulletButton(string dir)
    {
        if (dir == "UP")
        {
            targetBullet = Vector3.up;
        }
        else if (dir == "DOWN")
        {
            targetBullet = Vector3.down;
        }
        else if (dir == "LEFT")
        {
            targetBullet = Vector3.left;
        }
        else if (dir == "RIGHT")
        {
            targetBullet = Vector3.right;
        }
        OnClickFIre();
    }
    void OnClickFIre()
    {
        if (snowball < 1) return;
        SnowBall();
        GameObject bullet;
        bullet = ObjectPool.Instance.GetObject(PoolObjectType.BULLET);
        bullet.transform.position = player.transform.position;
        PlayerScale(false);
        if (haveStone)
        {
            SoundM.Instance.SoundOn("SFX", 3);
            bullet.tag = "STONESNOW";
            InvenOn("STONE", false);
            FireColor(false);
        }
        else
        {
            SoundM.Instance.SoundOn("SFX", 2);
        }
    }

    //총알 버튼 색 바꾸기
    public void FireColor(bool isStone)
    {
        if (isStone)
        {
            for (int i = 0; i < 4; i++)
            {
                fireColor[i].color = Color.gray;
            }
        }
        else
        {
            for (int i = 0; i < 4; i++)
            {
                fireColor[i].color = Color.white;
            }

        }
    }

}
