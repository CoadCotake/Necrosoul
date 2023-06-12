using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InvenTitle : MonoBehaviour
{
    [Header("≈∏¿Ã∆≤")]
    public Image AchImage;
    public Text AchName;

    [SerializeField]Inven_Book_Button Inven_p;

    [Header("¿Œ∫• πˆ∆∞ º±≈√")]
    public GameObject Total_ContentPlace;
    public List<Toggle> Total_Content;

    [Header("¿Œ∫•ø‰º“")]
    public GameObject Inven_ContentPlace;
    public List<GameObject> Inven_Content;

    [Header("Ω∫∆‰º»¿Œ∫•ø‰º“")]
    public GameObject SpInven_ContentPlace;
    public List<GameObject> spInven_Content;

    [Header("Ω√≥ ¡ˆø‰º“")]
    public GameObject synerg_ContentPlace;
    public List<GameObject> synerg_Content;
    public Text PageText;

    [Header("≥ªøÎ √‚∑¬")]
    public Image AchImage_C;
    public Text AchName_C;
    public Text AchConect;

    public Toggle My_toggle;

    [Header("¿Œ∫•∆–≥Œ")]
    [SerializeField] List<int> PageNumberlimit = new List<int>();
    [SerializeField] List<int> PageNow = new List<int>();
    [SerializeField] List<int> PageTotal= new List<int>();

    public int ActiveButton;
    public Button R_Btn;
    public Button L_Btn;

    public int key;
    int num;

    private void Awake()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        My_toggle = this.gameObject.GetComponent<Toggle>();
        for(int i=0; i<Total_ContentPlace.transform.childCount;i++)
        {
            Total_Content.Add(Total_ContentPlace.transform.GetChild(i).GetComponent<Toggle>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(My_toggle.isOn)
        {
            ButtonOn();
        }
        else
        {

        }
    }

    public void ChangeElement(Inven_Book_Button Inven_b)        //πˆ∆∞¿« ø‰º“
    {
        this.Inven_p = Inven_b;
        AchImage.sprite = Inven_p.Inven_Button_Image;
        AchName.text = Inven_p.Inven_Button_Name;
        key = Inven_b.key-1;
    }
    public void ChangeElement_panel()        //∆–≥Œ¿« ø‰º“
    {
        if (this.gameObject.activeSelf)
        {
            switch (key)
            {
                case 0:
                    synerg_Inven();
                    break;
                case 1:
                    inven_Inven();
                    break;
                case 2:
                    Sp_Inven();
                    break;
                default:
                    break;
            }
        }
    }

    public void synerg_Inven()
    {
        Debug.Log("Ω√≥ ¡ˆ√‚∑¬");
        ActiveButton = 0;
        for (int i = (PageNow[key] - 1) * PageNumberlimit[key]; i < PageNow[key] * PageNumberlimit[key]; i++)
        {
            if (i >= Synergy_Manager.Sy_manager.Sy_total_List.Count)
            {
                synerg_Content[i % PageNumberlimit[key]].gameObject.SetActive(false);
            }
            else
            {
                synerg_Content[i % PageNumberlimit[key]].gameObject.SetActive(true);
                ActiveButton++;
                //Debug.Log(ItemDatabase.itemDatabase.item_list[i].Name);
                synerg_Content[i % PageNumberlimit[key]].GetComponent<Inven_Content_Sy>().ChangeSy(Synergy_Manager.Sy_manager.Sy_total_List[i]);
            }
        }
    }

    public void inven_Inven()
    {
        Debug.Log("¿Œ∫• √‚∑¬");
        ActiveButton = 0;
        for (int i = (PageNow[key] - 1) * PageNumberlimit[key]; i < PageNow[key] * PageNumberlimit[key]; i++)
        {
            if (i >= ItemDatabase.itemDatabase.GetItemList.Count)
            {
                Inven_Content[i % PageNumberlimit[key]].gameObject.SetActive(false);
            }
            else
            {
                Inven_Content[i % PageNumberlimit[key]].gameObject.SetActive(true);
                ActiveButton++;
                //Debug.Log(ItemDatabase.itemDatabase.item_list[i].Name);
                Inven_Content[i % PageNumberlimit[key]].GetComponent<Inven_Content>().ChangeAch(ItemDatabase.itemDatabase.GetItemList[i]);
            }
        }
    }
    public void Sp_Inven()
    {
        ActiveButton = 0;
        for (int i = (PageNow[key] - 1) * PageNumberlimit[key]; i < PageNow[key] * PageNumberlimit[key]; i++)
        {
            if (i >= Sp_ItemEffect.sp_itemeffect.Sp_have.Count)
            {
                spInven_Content[i % PageNumberlimit[key]].gameObject.SetActive(false);
            }
            else
            {
                spInven_Content[i % PageNumberlimit[key]].gameObject.SetActive(true);
                ActiveButton++;
                //Debug.Log(ItemDatabase.itemDatabase.item_list[i].Name);
                num = Sp_ItemEffect.sp_itemeffect.Sp_have[i];
                spInven_Content[i % PageNumberlimit[key]].GetComponent<Inven_Content_Sp>().ChangeAch(Sp_ItemDatabase.Sp_itemDatabase.Sp_item_all[num]);
                spInven_Content[i % PageNumberlimit[key]].GetComponent<Inven_Content_Sp>().item_forginkey = i;
            }
        }
    }

    public void FristStart()        //∏Ó∆‰¿Ã¡ˆ ∏∏µÈ∞«¡ˆ √º≈©
    {
        if (this.gameObject.activeSelf)
        {
            switch (key)
            {
                case 0:
                    SynergFrist();
                    break;
                case 1:
                    invenFrist();
                    break;
                case 2:
                    SpFrist();
                    break;
                default:
                    break;
            }
        }
    }

    public void invenFrist()
    {
        Inven_Content.Clear();
        Debug.Log("¿Œ∫•√π");
        for (int i = 0; i < Inven_ContentPlace.transform.childCount; i++)
        {
            Inven_Content.Add(Inven_ContentPlace.transform.GetChild(i).gameObject);
        }
        PageTotal[key] = ItemDatabase.itemDatabase.item_list.Count / PageNumberlimit[key];
        if (ItemDatabase.itemDatabase.item_list.Count % PageNumberlimit[key] != 0)
        {
            PageTotal[key]++;
        }
    }

    public void SpFrist()
    {
        spInven_Content.Clear();
        Debug.Log("Ω∫««√π");
        for (int i = 0; i < SpInven_ContentPlace.transform.childCount; i++)
        {
            spInven_Content.Add(SpInven_ContentPlace.transform.GetChild(i).gameObject);
        }
        //ºˆ¡§
        PageTotal[key] = Sp_ItemEffect.sp_itemeffect.Sp_have.Count / PageNumberlimit[key];
        if (Sp_ItemEffect.sp_itemeffect.Sp_have.Count % PageNumberlimit[key] != 0)
        {
            PageTotal[key]++;
        }
    }
    public void SynergFrist()
    {
        synerg_Content.Clear();
        Debug.Log("Ω√≥ ¡ˆ√π"+ Synergy_Manager.Sy_manager.Sy_total_List.Count+" / ");
        for (int i = 0; i < synerg_ContentPlace.transform.childCount; i++)
        {
            synerg_Content.Add(synerg_ContentPlace.transform.GetChild(i).gameObject);
        }
        //Ω√≥ ¡ˆ πˆ∆∞ √ﬂ∞°«œ∏È ≥÷±‚
        PageTotal[key] = Synergy_Manager.Sy_manager.Sy_total_List.Count / PageNumberlimit[key];
        if (Synergy_Manager.Sy_manager.Sy_total_List.Count % PageNumberlimit[key] != 0)
        {
            PageTotal[key]++;
        }
    }

    public void ButtonOn()
    {
        //R_Btn.onClick.RemoveAllListeners();
        //L_Btn.onClick.RemoveAllListeners();
        //ChangeElement_panel();
        //R_Btn.onClick.AddListener(R_Button);
        //L_Btn.onClick.AddListener(L_Button);
        ChangeElement_panel();
    }

    public void L_Button()
    {
        Debug.Log("≈∏¿Ã∆≤ øﬁ¬  πˆ∆∞ øœ");
        if (PageNow[key] > 1)
        {
            PageNow[key]--;
        }
        ChangeElement_panel();
        PageText.text = PageNow[key] + " / " + "3";
    }
    public void R_Button()
    {
        Debug.Log("≈∏¿Ã∆≤ ø¿∏•¬  πˆ∆∞ øœ");
        if (PageNow[key] < PageTotal[key])
        {
            PageNow[key]++;
        }
        ChangeElement_panel();
        PageText.text = PageNow[key] + " / " + "3";
    }

    public void SetOnButtonPage()
    {
        
        Debug.Log("ø¿∆R»˛" + this.gameObject.name);
        Total_Content[key].SetIsOnWithoutNotify(true);
        Total_Content[key].gameObject.GetComponent<OpenMyInven>().PageOn();
        //for (int i = 0; i < Total_Content.Count; i++)
        //{
        //    Debug.Log("§æøÀπÃ");
        //    Total_Content[i].gameObject.GetComponent<OpenMyInven>().PageOn();
        //}
    }

}
