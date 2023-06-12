using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skill : MonoBehaviour
{
    static public List<SkillClass> Skill_Slot = new List<SkillClass>();
    public Image Skillimage1;
    public Image Skillimage2;

    public Image CoolTiem1;
    public Image CoolTiem2;

    void Start()
    {
        Skillimage1= GameObject.Find("Skill_Image1").GetComponent<Image>();
        Skillimage2 = GameObject.Find("Skill_Image2").GetComponent<Image>();
        CoolTiem1 = GameObject.Find("Skill_CoolTime1").GetComponent<Image>();
        CoolTiem2 = GameObject.Find("Skill_CoolTime1").GetComponent<Image>();
        Skill_Slot.Add(new SkillClass("테스트1", 1.0f));
        Skill_Slot.Add(new SkillClass("테스트2", 1.0f));
    }

    // Update is called once per frame
    private void Update()
    {
        /*skill_system();
        Skillimage1.sprite = Skill_Slot[0].skill_sprite;
        Skillimage2.sprite = Skill_Slot[1].skill_sprite;  */  
    }
    void skill_system()
    {

        if (Input.GetKeyDown(KeyCode.F) && Skill_Slot[0].skill_available)
        {
            Debug.Log(Skill_Slot[0].skill_name+": 작동중입니다. F사용 ");
            Skill_Slot[0].skill_active();
            StartCoroutine(Skill_Slot[0].CoolTIme());
        }
        if (Input.GetKeyDown(KeyCode.G) && Skill_Slot[1].skill_available)
        {
            Debug.Log(Skill_Slot[1].skill_name + ": 작동중입니다. G사용 ");
            Skill_Slot[1].skill_active();
            StartCoroutine(Skill_Slot[1].CoolTIme());
        }


        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Debug.Log("위치변환 ");
            //if (!skill_slot_change)
            //    skill_slot_change = true;
            //else
            //    skill_slot_change = false;

            Skill_Slot.Reverse();
        }
    }
}

