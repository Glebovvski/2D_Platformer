using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollPanel : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        StartCoroutine(ScaleUp(other));
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        //StopAllCoroutines();
        StartCoroutine(ScaleDown(other));
    }

    IEnumerator ScaleUp(Collider2D other)
    {
        float progress = 0;
        Vector3 finalScale = new Vector3(1.3f, 1.3f, 1.3f);
        while (progress <= 1)
        {
            other.transform.localScale = Vector3.Lerp(other.transform.localScale, finalScale, progress);
            progress += Time.deltaTime * 2f;
            yield return null;
        }
        other.transform.localScale = finalScale;
    }

   IEnumerator ScaleDown(Collider2D other)
   {
        float progress = 0;
        Vector3 finalScale = new Vector3(1f, 1f, 1f);
        while (progress <= 1)
        {
            other.transform.localScale = Vector3.Lerp(other.transform.localScale, finalScale, progress);
            progress += Time.deltaTime * 2f;
            yield return new WaitForEndOfFrame();
        }
        other.transform.localScale = finalScale;
    }
}
