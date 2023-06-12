using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillClass
{
    public string skill_name;       //이름
    public int foreignkey;          //연결 키
    public Sprite skill_sprite;     //이미지
    public float skill_cooltime;    //쿨타임
    public bool skill_check=false;        //스킬이 있는지 체크

    public bool skill_available;    //스킬 작동이 가능한가?
    public GameObject skill_effect;//임시? 어떤 방식으로?

    List<Dictionary<string, object>> Data = CSVReader.Read("SkillTree");

    public SkillClass(string c, float t)
    {
        skill_name = c;
        skill_cooltime = t;
        skill_available = true;
    }

    public void skill_active()
    {
        if (skill_available == true)    //작동
        {
            Debug.Log("작동합니다 스킬작동");
            //skill_available =false;
            skill_available = false;
        }
    }

    public void GetSkillItem(Item item)
    {
        //foreignkey = item.Foreignkey;
        ChangeSkill();
    }
    public void ChangeSkill()
    {
        skill_name = Data[foreignkey]["Name"].ToString();
        skill_cooltime = float.Parse(Data[foreignkey]["CoolTime"].ToString());
        skill_sprite = Resources.Load(Data[foreignkey]["Image"].ToString(), typeof(Sprite)) as Sprite;
    }

    public IEnumerator CoolTIme()
    {
        var wait = new WaitForSeconds(1f);

        for (float i = skill_cooltime; i >= 0; i--)
        {
            yield return wait;
        }
        Debug.Log(skill_name + "쿨타임끝");
        skill_available = true;
        yield return null; //

    }
}
