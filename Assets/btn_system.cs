using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.UI.Button;

public class btn_system : MonoBehaviour
{
    public List<Button> a = new List<Button>();
    int select;
    public float timer;
    // Start is called before the first frame update
    void Start()
    {
        
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
                if (a[i].IsInteractable() == true)
                {
                    a[i].interactable = false;
                }

            }
            else
            {
                if (a[i].IsInteractable() == false)
                {
                    a[i].interactable = true;
                }
            }
        }
        float vr = Input.GetAxis("Horizontal");
        if (Input.GetButtonDown("Horizontal"))
        {
            if (vr > 0)
            {

                select--;
                if (select < 0)
                    select = a.Count - 1;
            }
            else
            {
                select++;
                if (select > a.Count - 1)
                    select = 0;
            }
        }
       
        if (Input.GetKeyDown(Key_manager.Keys[Key_manager.KeyAction.ATTACK]))
        {

            ButtonClickedEvent btn = a[select].onClick;
            btn.Invoke();
            Toggle t;
           
        }
    }
}
