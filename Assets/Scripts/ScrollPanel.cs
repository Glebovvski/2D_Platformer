using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollPanel : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        //StartCoroutine(ScaleUp(other));
        float progress = 0;
        Vector3 finalScale = new Vector3(1.3f, 1.3f, 1.3f);
        //while (progress <= 1)
        //{
        //    other.transform.localScale = Vector3.Lerp(other.transform.localScale, finalScale, progress);
        //    progress += Time.deltaTime * 0.1f;
        //    yield return null;
        //}
        other.transform.localScale = finalScale;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        other.transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        //StartCoroutine(ScaleDown(other));
        float progress = 0;
        Vector3 finalScale = new Vector3(1f, 1f, 1f);
        //while (progress <= 1)
        //{
        //    other.transform.localScale = Vector3.Lerp(other.transform.localScale, finalScale, progress);
        //    progress += Time.deltaTime * 0.1f;
        //    yield return new WaitForEndOfFrame();
        //}
        other.transform.localScale = finalScale;
    }

    

    IEnumerator ScaleUp(Collider2D other)
    {
        float progress = 0;
        Vector3 finalScale = new Vector3(1.3f, 1.3f, 1.3f);
        while (progress <= 1)
        {
            other.transform.localScale = Vector3.Lerp(other.transform.localScale, finalScale, progress);
            progress += Time.deltaTime * 0.1f;
            yield return null;
        }
        other.transform.localScale = finalScale;
    }

   //IEnumerator ScaleDown(Collider2D other)
   //{
   //    
   //}
}
