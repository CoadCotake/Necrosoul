using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Sp_item_slot : MonoBehaviour, IPointerClickHandler
{
    public GameObject Parent;
    public Image Image;
    public Text Name;
    public Text Descrition;

    public bool choice;     //선택된 대상

    [SerializeField]
    Sp_Item sp_item=new Sp_Item();

    private void Start()
    {
       // Parent.SetActive(false);
    }
    private void OnEnable()
    {
        Debug.Log("시자아아아악 하겠습니다아아아아아ㅏㅏㅏㅏㅏㅏㅏㅏ 스페셜 아이템 ");
        sp_item = Sp_ItemDatabase.Sp_itemDatabase.Random_Sp_Item();
        Image.sprite = sp_item.Sprite;
        Name.text = sp_item.Name.ToString();
        Descrition.text = sp_item.Description.ToString();
    }

    private void OnDisable()
    {
        if (choice == false)   //선택된 아이템이 아니라면 다시 리스트에 넣기
        {
            Debug.Log("난 선택되지 못했어 ...");
            Sp_ItemDatabase.Sp_itemDatabase.Classify(sp_item);
        }
        choice = false;
        sp_item = null;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            choice = true;
            Gamemanager.GM.sp_item_check=true;
            Gamemanager.GM.can_handle = true;
            //Sp_ItemEffect.sp_itemeffect.Sp_Ef[sp_item.Foreignkey] = true;     아직 테스트 x
            Parent.SetActive(false);
        }
    }

    public void SelectItem()
    {
        choice = true;
        Gamemanager.GM.sp_item_check = true;
        Gamemanager.GM.can_handle = true;
        Sp_ItemEffect.sp_itemeffect.Sp_Ef[sp_item.Foreignkey-1] = true;
        Sp_ItemEffect.sp_itemeffect.Sp_item_HaveCheck();
        Parent.SetActive(false);

    }
}
