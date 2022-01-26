using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextEffect : MonoBehaviour
{
    Text text;
    [SerializeField]
    Animator anim;

    private void Awake()
    {
        text = GetComponent<Text>();
    }
    private void OnEnable()
    {
        StartCoroutine(FadeTextToZero());
    }

    public IEnumerator FadeTextToZero()  // 알파값 1에서 0으로 전환
    {
            yield return new WaitForSeconds(2.5f);
        anim.Play("TextFade");
    ObjectPool.Instance.ReturnScoreEffect(gameObject);
    }

}
