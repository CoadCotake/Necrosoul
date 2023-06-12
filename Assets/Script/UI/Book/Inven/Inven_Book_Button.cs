using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]
public class Inven_Book_Button
{
    public Sprite Inven_Button_Image;
    public string Inven_Button_Name;
    public int key;

    public Inven_Book_Button Create()
    {
        Inven_Book_Button I = new Inven_Book_Button();
        I.Inven_Button_Image = this.Inven_Button_Image;
        I.Inven_Button_Name = this.Inven_Button_Name;
        I.key = this.key;
        return I;
    }
}
