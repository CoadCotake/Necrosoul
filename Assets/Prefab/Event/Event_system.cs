using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.UI.Button;

public class Event_system : MonoBehaviour
{
    public List<Button> a = new List<Button>();
   public int select;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void event_end()
    {
        this.transform.parent.gameObject.SetActive(false);
        Gamemanager.GM.can_handle = true;
    }
    // Update is called once per frame
    void Update()
    {
        BtnSystem(a);
    }
    void BtnSystem(List<Button> a)
    {
        for (int i = 0; i < a.Count; i++)
        {
            if (i == select)
            {

                a[i].transform.GetChild(0).gameObject.SetActive(true);
             

            }
            else
            {
                a[i].transform.GetChild(0).gameObject.SetActive(false);
            }
        }
        if (Input.GetKeyDown(Key_manager.Keys[Key_manager.KeyAction.UP]))
        {
            select--;
            if (select < 0)
                select = a.Count - 1;
        }
        else if (Input.GetKeyDown(Key_manager.Keys[Key_manager.KeyAction.DOWN]))
        {
            select++;
            if (select > a.Count - 1)
                select = 0;
        }
            
        if (Input.GetKeyDown(Key_manager.Keys[Key_manager.KeyAction.ATTACK]))
        {

            ButtonClickedEvent btn = a[select].onClick;
            btn.Invoke();
            

        }
    }
    public void event_obj_disable()
    {
        this.transform.parent.gameObject.SetActive(false);
    }
}
