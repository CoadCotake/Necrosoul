using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]
public class Setting_class
{
    public Sprite Setting_Image;
    public string Setting_Name;
    public string Setting_Descrition;

    public Setting_class Create()
    {
        Setting_class S = new Setting_class();
        S.Setting_Image = this.Setting_Image;
        S.Setting_Name = this.Setting_Name;
        S.Setting_Descrition = this.Setting_Descrition;
        return S;
    }
}
