using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
                if (instance != null)
                {
                    GameObject container = new GameObject("GameManager");
                    instance = container.AddComponent<GameManager>();
                }
            }
            return instance;
        }
    }

    public float limitMinX = -27.8f, limitMaxX = 27.8f, limitMinY = -12.5f, limitMaxY = 12.5f; //����

    public int score; //����
    public int highScore; //�ְ�����
    public int plusScore=20; //�߰�����

    public float playerInitScale = 0.6f; //�÷��̾� �⺻ ũ��
    public float moveSpeed = 8f; //�÷��̾� ���ǵ�

    private void Awake()
    {
        instance = this;
    }
}
