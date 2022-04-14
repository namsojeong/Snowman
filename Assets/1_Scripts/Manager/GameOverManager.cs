using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
/// <summary>
/// ���ӿ��� ���� �ʿ��� ��� �� (�ַ� UI)
/// </summary>
public class GameOverManager : MonoBehaviour
{
    [SerializeField]
    Text overText;
    [SerializeField]
    private Text overScoreText;
    [SerializeField]
    private Text overHighScoreText;

    private void Start()
    {
        //UI �Լ� ����
        {
            overText.text = "";
            OverText();
            UpdateOverUI();
        }
       
    }

    //���ӿ��� UI
    public void UpdateOverUI()
    {
        overHighScoreText.text = string.Format($"HIGHSCORE {PlayerPrefs.GetInt("HIGHSCORE", 0)}");
        overScoreText.text = string.Format($"SCORE\n{PlayerPrefs.GetInt("SCORE", 0)}");
    }
    private void OverText()
    {
        overText.DOText("GAME OVER", 2f);
    }
}
