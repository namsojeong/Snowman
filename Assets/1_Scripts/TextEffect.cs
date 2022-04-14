using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ������ ����� �� ���� ȿ��
/// </summary>
public class TextEffect : MonoBehaviour
{
    Coroutine FadeC;
    private void OnEnable()
    {
        FadeC = StartCoroutine(FadeTextToZero());
    }
    public IEnumerator FadeTextToZero()  // ���İ� 1���� 0���� ��ȯ
    {
        yield return new WaitForSeconds(2f);
        //anim.Play("TextFade");
        ObjectPool.Instance.ReturnObject(PoolObjectType.PlusText, gameObject);
    }
}