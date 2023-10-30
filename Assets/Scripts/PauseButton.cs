using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PauseButton : MonoBehaviour
{
    private bool isHovering = false;
    public Button but;
    private Color normalColor;
    private Color opaqueColor;

    // Start is called before the first frame update
    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovering = true;
        Debug.Log("Mouse entered the button");
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        isHovering = false;
        Debug.Log("Mouse exited the button");
    }
    void Start()
    {
        normalColor = but.image.color;
        opaqueColor = normalColor;
        opaqueColor.a = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (isHovering)
        {
            but.image.color = opaqueColor;
            Debug.Log("Mouse is hovering over the button");
        }
        else
        {
            but.image.color = normalColor;
        }
    }
}
