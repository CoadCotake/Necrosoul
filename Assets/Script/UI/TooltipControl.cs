using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipControl : MonoBehaviour, IPointerEnterHandler,IPointerExitHandler
{
    public Tooltip tooltip;

    public void OnPointerEnter(PointerEventData eventData)
    {
        Item item = GetComponent<Shop>().item;
        if(GetComponent<Shop>().ClickCheck)
        {
            Debug.Log("À¸À×?"+item.Name);
            tooltip.gameObject.SetActive(true);
            tooltip.SetupTooltip(item);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tooltip.gameObject.SetActive(false); 
    }

}
