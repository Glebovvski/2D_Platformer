using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class ScrollRectSnap : MonoBehaviour
{
    public RectTransform panel;
    public Button[] buttons;
    public RectTransform center;

    private float[] distance;
    private bool dragging = false;
    private int btnDistance;
    private int minBtnNum;

    private void Start()
    {
        int btnLength = buttons.Length;
        distance = new float[btnLength];

        btnDistance = (int)Mathf.Abs(buttons[1].GetComponent<RectTransform>().anchoredPosition.x - buttons[0].GetComponent<RectTransform>().anchoredPosition.x);
    }

    private void Update()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            distance[i] = Mathf.Abs(center.transform.position.x - buttons[i].transform.position.x);
        }

        float minDistance = Mathf.Min(distance);

        for (int j = 0; j < buttons.Length; j++)
        {
            buttons[j].enabled = false;
            if (minDistance == distance[j])
            {
                minBtnNum = j;
                buttons[j].enabled = true;
            }
        }

        if (!dragging)
        {
            LerpToButton(minBtnNum * -btnDistance);
        }
    }

    void LerpToButton(int position)
    {
        float newX = Mathf.Lerp(panel.anchoredPosition.x, position, 3f * Time.deltaTime); //10 looks really good on PC

        Vector2 newPos = new Vector2(newX, panel.anchoredPosition.y);

        panel.anchoredPosition = newPos;
    }

    public void StartDrag()
    {
        dragging = true;
    }

    public void EndDrag()
    {
        dragging = false;
    }

    
}
