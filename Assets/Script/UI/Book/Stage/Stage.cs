using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]
public class Stage
{
    public Sprite Stage_Image;
    public string Stage_Name;
    public string Stage_Descrition;
    public string Stage_Exit;

    public Stage Create()
    {
        Stage S = new Stage();
        S.Stage_Image = this.Stage_Image;
        S.Stage_Name = this.Stage_Name;
        S.Stage_Descrition = this.Stage_Descrition;
        S.Stage_Exit = this.Stage_Exit;
        return S;
    }
}
