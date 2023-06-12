using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour
{
    public Text NameText;
    public Text DescriptionText;
    public Text RareText;
    public Image IconImage;
    
    [SerializeField]float halfWidth;
    [SerializeField]float halfheight;

    int x;
    int y;

    Vector2 xy;

    RectTransform Rt;

    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.SetActive(false);
        halfWidth = GetComponentInParent<CanvasScaler>().referenceResolution.x * 0.5f;
        halfheight = GetComponentInParent<CanvasScaler>().referenceResolution.y * 0.5f;
        Rt = GetComponent<RectTransform>();
    }
    public void set_pos()
    {
        Vector3 v3 =new Vector3(960/2+180, 0, 0);
        transform.position = v3;
    }
    // Update is called once per frame
    void Update()
    {
       

        if(Rt.anchoredPosition.x+Rt.sizeDelta.x>halfWidth)
        {
            xy.x=1;
        }
        else
        {
            xy.x = 0;
        }

        if(Rt.anchoredPosition.y + Rt.sizeDelta.y > halfWidth)
        {
            xy.y = 1;
        }
        else
        {
            xy.y = 0;
        }

        Rt.pivot =xy;
    }
    public void SetupTooltip(Item item)
    {
        if (item.num <= 0)
        {
            NameText.text = " ??? ";
            DescriptionText.text = "????????????";
            RareText.text = "??";

            if (item.Rarity == "ÀÏ¹Ý")
            {
                RareText.color = new Color(1, 1, 1);
            }
            else if (item.Rarity == "Èñ±Í")
            {
                RareText.color = new Color(0, 153 / 255f, 255 / 255f);
            }
            else
            {
                RareText.color = new Color(180 / 255f, 85 / 255f, 162 / 255f);
            }
        }
        else
        {
            NameText.text = item.Name + " x " + item.num;
            DescriptionText.text = item.Description;
            IconImage.sprite = item.Sprite;
            RareText.text = item.Rarity;

            if (item.Rarity == "ÀÏ¹Ý")
            {
                RareText.color = new Color(1, 1, 1);
            }
            else if (item.Rarity == "Èñ±Í")
            {
                RareText.color = new Color(0, 153 / 255f, 255 / 255f);
            }
            else
            {
                RareText.color = new Color(180 / 255f, 85 / 255f, 162 / 255f);
            }
        }
    }
}
