using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
/// <summary>
/// 게임오버 씬에 필요한 모든 것 (주로 UI)
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
        //UI 함수 실행
        {
            overText.text = "";
            OverText();
            UpdateOverUI();
        }
       
    }

    //게임오버 UI
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
