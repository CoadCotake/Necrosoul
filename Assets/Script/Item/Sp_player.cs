using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sp_player : MonoBehaviour
{
    public GameObject Sp0Image;
    public GameObject Sp3Ojbect;
    // Start is called before the first frame update
    void Start()
    {
        Sp0Image.SetActive(false);
        Sp3Ojbect.SetActive(false);
    }
    private void FixedUpdate()
    {
        if (Sp_ItemEffect.sp_itemeffect.Sp_Ef[0])
        {
            Sp0_player();
        }

        if(Sp_ItemEffect.sp_itemeffect.sp3_On)
        {
            Sp3Ojbect.SetActive(true);
        }
    }

    public void Sp0_player()
    {
        if (Sp_ItemEffect.sp_itemeffect.sp0_timer >= 10)
        {
            Sp0Image.SetActive(true);
        }
        else
        {
            Sp0Image.SetActive(false);

        }
    }

    public void Sp3_player_destory()
    {
        Sp_ItemEffect.sp_itemeffect.sp3_On = false;
        Sp3Ojbect.SetActive(false);
    }
    
}
