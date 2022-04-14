using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 점수를 얻었을 때 점수 효과
/// </summary>
public class TextEffect : MonoBehaviour
{
    Coroutine FadeC;
    private void OnEnable()
    {
        FadeC = StartCoroutine(FadeTextToZero());
    }
    public IEnumerator FadeTextToZero()  // 알파값 1에서 0으로 전환
    {
        yield return new WaitForSeconds(2f);
        //anim.Play("TextFade");
        ObjectPool.Instance.ReturnObject(PoolObjectType.PlusText, gameObject);
    }
}