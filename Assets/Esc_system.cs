 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.UI.Button;

public class Esc_system : MonoBehaviour
{
    int select = 0;
    public List<Button> esc_btn;
    public bool selected;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!selected)
        {
            BtnSystem(esc_btn);
        }
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
        float vr = Input.GetAxis("Vertical");
        if (Input.GetButtonDown("Vertical"))
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

        if (Input.GetButtonDown("active"))
        {

            ButtonClickedEvent btn = a[select].onClick;
            btn.Invoke();
            selected = true;
        }
    }
}
