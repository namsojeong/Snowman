using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class InGame : MonoBehaviour
{
    [SerializeField]
    private Light2D mainLight;

    public static InGame Instance;

    public GameObject player;

    public Vector2 targetBullet = Vector2.zero;

    public float snowball = 0; //�� ����
    public float playerScale = 0.6f;

    bool isLighting = false;

    int lightTime = 0;

    private float playerPlusScale = 0.001f; //�÷��̾� ������ �� �ʴ� �����ϴ� ũ��
    private float playerMinusScale = 0.2f; //�÷��̾� �߻��� �� �����ϴ� ũ��

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

    //�Ѿ� �پ���
    public void SnowBall()
    {
            snowball -= 1;
    }

    //�÷��̾� ũ�� �ٲٱ�
    public void PlayerScale(bool isBig)
    {
        if (isBig)
        {
            playerScale += playerPlusScale;
            if(playerScale>=1.6f)
            {
                playerScale = 1.6f;
            }
        }
        else
        {
            playerScale -= playerMinusScale;
            if(playerScale<=0.6f)
            {
                playerScale = 0.6f;
            }
        }
    }

    //���� Ȯ��
    void CheckScore()
    {
        if (GameManager.Instance.score == 6)
        {
            if (isLighting) return;
            isLighting = true;
            mainLight.intensity = 0.3f;
            InvokeRepeating("SpawnLight", 0f, Random.Range(5f, 30f));
        }
    }

    //�� ����
    private void SpawnLight()
    {
        lightTime++;
        if (lightTime >= 5)
        {
            CancelInvoke("SpawnLight");
            mainLight.intensity = 1f;
            isLighting = false;
        }
        GameObject light = ObjectPool.Instance.GetObject(PoolObjectType.LIGHT);
        light.transform.position = new Vector3(Random.Range(-27.8f, 27.8f), Random.Range(-12.5f, 12.5f), 0f);
    }

    //�� ����
    public void SpawnFoot()
    {
        GameObject foot = ObjectPool.Instance.GetObject(PoolObjectType.FOOT);
        foot.transform.position = new Vector2(player.transform.position.x, player.transform.position.y);
    }

    //���� ����
    public void Reset()
    {
        CancelInvoke("SpawnLight");
        mainLight.intensity = 1f;
        isLighting = false;

        ObjectPool.Instance.ResetObj();
        PlayerPrefs.SetInt("HIGHSCORE", GameManager.Instance.highScore);
        PlayerPrefs.SetInt("SCORE", GameManager.Instance.score);

        snowball = 0;
        playerScale = GameManager.Instance.playerInitScale;
        player.transform.localScale = new Vector2(GameManager.Instance.playerInitScale, GameManager.Instance.playerInitScale);
        player.transform.position = new Vector2(0f, 0f);

        GameManager.Instance.score = 0;
    }

}
