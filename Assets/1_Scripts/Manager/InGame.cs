using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 인게임의 모든 시스템 (주로 적과 아이템 스폰, 플레이어 크기 조절, 총알 발사)
/// </summary>
public class InGame : MonoBehaviour
{
    public static InGame instance;
    public static InGame Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<InGame>();
                if (instance != null)
                {
                    GameObject container = new GameObject("InGame");
                    instance = container.AddComponent<InGame>();
                }
            }
            return instance;
        }
    }

    public GameObject player;

    [Header("ITEM")]
    [SerializeField]
    Image[] invenAngel;

    [Header("BULLET")]
    [SerializeField]
    Button[] fireButton;
    [SerializeField]
    Image[] fireColor;
    public Vector2 targetBullet = Vector2.zero;

    [Header("FOOT")]
    public Sprite[] footSprite;

    public float snowball = 0; //눈 개수
    public float playerScale = 0.6f;

    bool isStone = false;
    bool isAngel = false;

    public bool haveAngel = false;
    public bool haveStone = false;

    int minusTime = 0;

    private float playerPlusScale = 0.0035f; //플레이어 움직일 때 초당 증가하는 크기
    private float playerMinusScale = 0.2f; //플레이어 발사할 때 감소하는 크기

    private void Awake()
    {
        instance = this;

        //총알 버튼 클릭 시
        {
            fireButton[0].onClick.AddListener(() => BulletButton("UP"));
            fireButton[1].onClick.AddListener(() => BulletButton("DOWN"));
            fireButton[2].onClick.AddListener(() => BulletButton("LEFT"));
            fireButton[3].onClick.AddListener(() => BulletButton("RIGHT"));
        }
    }
    private void Start()
    {
        //발 스폰
        {
            SpawnFoot();
        }
    }

    private void Update()
    {
        //점수 확인
        {
            CheckScore();
        }
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

    //점수 확인
    void CheckScore()
    {
        if (GameManager.Instance.score == 6)
        {
            if (isStone) return;
            isStone = true;
            InvokeRepeating("SpawnStone", 0f, 3f);
            InvokeRepeating("SpawnAngel", 0f, 3f);
        }
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
        CancelInvoke("SpawnStone");
        CancelInvoke("SpawnAngel");
        isStone = false;
        isAngel = false;
        haveStone = false;

        ObjectPool.Instance.ResetObj();

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

    //총알 발사
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
