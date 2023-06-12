using System.Collections;
using System.Collections.Generic;
using UnityEngine;
    using UnityEngine.UI;

public class item_text : MonoBehaviour
{
    Animator ani;
    public GameObject last_enemy_ui;
    public GameObject item_frame;
    public Image item_UI;
    public Text item_name_text;
    public Text item_desc_text;
    public Text pause_key_text;
    public GameObject inventory;
    public Text last_enemy;
    bool get_item_chk;
    Item i;
    // Start is called before the first frame update
    void Start()
    {

        ani = this.GetComponent<Animator>();
        reset_ani();
    }

    // Update is called once per frame
    void Update()
    {
        ani.SetInteger("loot_count", Gamemanager.GM.loot_box.Count);
        ani.SetBool("inventory", get_item_chk);
        if (get_item_chk)
        {
            inventory.SetActive(true);
        }
        else
        {
            inventory.SetActive(false);
        }
        if (Key_manager.Keys[Key_manager.KeyAction.PAUSE] == KeyCode.Escape)
        {
            pause_key_text.text = "ESC";
        }
        else
        {
            pause_key_text.text = Key_manager.Keys[Key_manager.KeyAction.PAUSE].ToString();
        }
        if (Gamemanager.GM.last_enemy > 0)
        {
            last_enemy_ui.SetActive(true);
            last_enemy.text = Gamemanager.GM.last_enemy.ToString();
        }
        else
        {
            last_enemy_ui.SetActive(false);
        }
    }
    void get_loot_box()
    {
        i = Gamemanager.GM.loot_box[0].CreateItem();
        item_name_text.text = i.Name;
        item_desc_text.text = i.Description;
        item_UI.sprite = i.Sprite;
        Gamemanager.GM.loot_box.RemoveAt(0);
        get_item_chk = true;
    }
    private void OnDisable()
    {
        reset_ani();
    }
    public void reset_ani()
    {
        for (int i = 0; i < Gamemanager.GM.loot_box.Count; i++)
        {
            Gamemanager.GM.loot_box.RemoveAt(0);
        }
        get_item_chk = false;
        item_frame.SetActive(false);
        inventory.SetActive(false);
        ani.SetTrigger("skip");

       
    }
   public void reset_trigger()
    {
        ani.ResetTrigger("skip");
    }
    private void OnEnable()
    {
       

    }
}
