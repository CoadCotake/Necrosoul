using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]
public class Ach_Book_Button
{
    public Sprite Ach_Button_Image;
    public string Ach_Button_Name;
    public string Ach_Button_Conect;

    public Ach_Book_Button Create()
    {
        Ach_Book_Button A = new Ach_Book_Button();
        A.Ach_Button_Conect = this.Ach_Button_Conect;
        A.Ach_Button_Image = this.Ach_Button_Image;
        A.Ach_Button_Name = this.Ach_Button_Name;
        return A;
    }
}
