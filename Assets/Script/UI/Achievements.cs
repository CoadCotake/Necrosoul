using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Achievements
{
    public int foreignkey;
    public string type;
    public string title;
    public string contents;
    public string requirements_num;
    public string state_text;
    public string stats_name;
    public string stats_plus_num;
    public string ach_index;
    public bool Statapply;
    public Achievements Create()
    {
        Achievements a = new Achievements();
        a.foreignkey = foreignkey;
        a.type = this.type;
        a.title = this.title;
        a.contents = this.contents;
        a.requirements_num = this.requirements_num;
        a.state_text = this.state_text;
        a.stats_name = this.stats_name;
        a.stats_plus_num = this.stats_plus_num;
        a.ach_index = this.ach_index;
        return a;
    }
}
