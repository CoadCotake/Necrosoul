using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Animations;

public class AchClear_panel : MonoBehaviour
{
    public Image Image;
    public Text Title;
    public Text Content;
    public Text Count;

    public int index;
    Animator mine;
    // Start is called before the first frame update
    void Awake()
    {
        index = 1;
        mine = this.gameObject.GetComponent<Animator>();
    }


    public void AchPanelEnd()
    {

        if (index <= AchievementsManage.achievementsManage.AchClear_panel.Count)
        {
            Debug.Log("최조클 업적 문구 수정중");
            mine.SetBool("End", false);
            AchievementsManage.achievementsManage.AchClearUpdate(index);
            mine.SetBool("End", true);
            index++;
        }
        else
        {
            index=1;
            this.gameObject.SetActive(false);
        }
    }

    public void Next()
    {

    }
}
