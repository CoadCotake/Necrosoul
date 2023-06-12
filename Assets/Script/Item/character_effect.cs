using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class character_effect : MonoBehaviour
{
    public struct effect
    {
        public float effect_timer;
        public float effect_time;
        public int effect_num;
        public void effect_set(float a,int i){
            a = effect_time;
            effect_timer = effect_time;
            i = effect_num;
            }
        public void active_Timer()
        {
            effect_timer -= Time.deltaTime;
        }
    }

    public List<effect> c_effect = new List<effect>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < c_effect.Count; i++)
        {
            switch (c_effect[i].effect_num)
            {
                case 1:
                    if (c_effect[i].effect_timer < 0)
                    {
                        un_paralyze();
                        c_effect.RemoveAt(i);
                    }
                    else
                    {
                        c_effect[i].active_Timer();
                    }
                    paralyze();
                    break;
            }
        }
    }

    public void paralyze()//애니메이션 효과 추가하기-> 애니메이션중 정지 피격애니메이션으로 정지
    {
        PlayerCharacter p_chr = this.GetComponent<PlayerCharacter>();
        p_chr.can_move = false;
       
    }
    public void un_paralyze()
    {
        PlayerCharacter p_chr = this.GetComponent<PlayerCharacter>();
        p_chr.can_move = true;
     
    }
}
