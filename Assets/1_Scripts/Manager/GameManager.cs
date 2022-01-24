using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public float limitMinX = -27.8f, limitMaxX = 27.8f, limitMinY = -12.5f, limitMaxY = 12.5f; //����

    public int score; //����
    public int highScore; //�ְ�����

    public float playerInitScale = 0.6f; //�÷��̾� �⺻ ũ��
    public float moveSpeed = 8f; //�÷��̾� ���ǵ�

    private void Awake()
    {
        Instance = this;
    }
}
