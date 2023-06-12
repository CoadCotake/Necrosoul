using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sp_ItemEffect : MonoBehaviour
{
    public static Sp_ItemEffect sp_itemeffect;

    public List<bool> Sp_Ef = new List<bool>();
    public List<int> Sp_have = new List<int>();

    [Header("Æ¯¼ºº¯¼öµé")]
    public float sp0_timer;
    public int sp0_dmg;
    public bool sp0_chk;
    public List< Unit> sp2_enemies=new List<Unit>();
    public Unit sp2_enemy;
    public GameObject sp2_effect;
    public float sp3_timer;
    public bool sp5_used;
    public int used_revive_item_num;
    public int sp2_dmg;
    public bool sp3_On;
    public int sp8_num;
    public int sp9_killenemy;
    public spEF_11_illusion EF_11_illusion;
    public spEF_11_illusion EF_11_illusion_instansi;
    public bool EF_11_room_chk;
    private void Awake()
    {
        sp_itemeffect = this;
    }
    private void FixedUpdate()
    {
        if (Sp_Ef[0])       //È¹µæ½Ã
        {
            if (sp0_timer<10)
            {
                sp0_timer += Time.deltaTime;
            }
        }
        if (Sp_Ef[3] && !sp3_On)       //È¹µæ½Ã
        {
            if(sp3_timer<60)
                sp3_timer += Time.deltaTime;
            else
            {
                sp3_On=true;
                sp3_timer = 0;
            }
        
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < Sp_ItemDatabase.Sp_itemDatabase.Sp_Item_total; i++)
        {
            Sp_Ef.Add(false);
        }
    }

    public void Sp_item_HaveCheck()
    {
        Sp_have.Clear();
        for (int i=0; i<Sp_Ef.Count;i++)
        {
            if(Sp_Ef[i]==true)
            {
                Sp_have.Add(i);
            }
        }
    }

    public int Sp_0(int dmg)
    {
        if (Sp_Ef[0])       //È¹µæ½Ã
        {
            if (sp0_timer >= 10)
            {
                sp0_dmg = Mathf.RoundToInt(Player_status.p_status.get_atk()*0.5f);
                SP_0_timer_initialize();
                sp0_dmg= dmg + sp0_dmg;
                
            }
            else
            {
                SP_0_timer_initialize();
                sp0_dmg = dmg;
            }
            return sp0_dmg;
        }
        else
        {
            return dmg;
        }
     
    }
   
    public void SP_0_timer_initialize()
    {
        if (Sp_Ef[0])       //È¹µæ½Ã
        {
            sp0_timer = 0;
        }
    }
    public void Sp_1(Collider2D col)       //°ø°Ý Ãæµ¹½Ã
    {
        if (Sp_Ef[1])       //È¹µæ½Ã
        {
            if (col.gameObject.CompareTag("Enemy_bullet"))
            {
                Destroy(col.gameObject);
            }
        }
    }

    public void Sp_2(Collider2D col)
    {
        if (Sp_Ef[2])       
        {
            
            if (col.gameObject.CompareTag("Enemy"))
            {
                sp2_enemy = col.transform.parent.GetComponent<Unit>();
                if (!sp2_enemies.Contains(sp2_enemy))
                {
                    Instantiate(sp2_effect, sp2_enemy.transform.position, Quaternion.identity);
                    sp2_enemies.Add(sp2_enemy);
                    sp2_dmg = Mathf.RoundToInt(Player_status.p_status.get_atk() * 0.8f);
                    
                    sp2_enemy.hitted_SP(sp2_dmg);
                }
            }
        }
    }
    public void Sp_2_reset_list()
    {
        int a = sp2_enemies.Count;
        for(int i = 0; i < a; i++)
        {
            sp2_enemies.RemoveAt(0);
        }
    }
    public bool Sp_3()
    {
        if (Sp_Ef[3] && sp3_On)
        {
            sp3_On = false;
            return false;
        }
        else
        {
            return true;
        }
       

    }
    public void Sp_4(int a)      
    {
        if (Sp_Ef[4])
        {
            Player_status.p_status.set_hp(a / 10);
        }
    }
    public bool SP_5()
    {
        if (Sp_Ef[5]&&!sp5_used)
        {
          
            used_revive_item_num = 5;
            return false;
        }
        else
        {
            return true;
        }
    }
    public void Sp_6(Item i)       //±¸½½ È¹µæ Àû¿ëÇÏ´Â°÷¿¡ ³Ö±â
    {
        if (Sp_Ef[6])       //È¹µæ½Ã
        {
            Gamemanager.GM.get_item_SP(i);
            
        }
        
    }

    public void Sp_8(Unit enemy)       //Àû Å¸°ÝÇÏ´Â °÷¿¡ if¹®À¸·Î Sp_Ef[8]°É°í ÀÌ°Å ³Ö±â
    {
        if (Sp_Ef[8]&&enemy.enemy_rank==0)
        {
            sp8_num = Random.Range(0, 10000);
            if (0 == sp8_num)
            {
                enemy.death();
            }
           
        }
       
    }
    public void Sp_9()       //Àû Å³¼ö°¡ ¿Ã¶ó°¡´Â °Í¿¡ kill++rhk sp_9 Àû±â
    {
        if (Sp_Ef[9])
        {
            sp9_killenemy++;
            Player_status.p_status.set_spatk(sp9_killenemy / 20);
            Player_status.p_status.set_max_spHP(sp9_killenemy / 20);
        }
    }
   public void SP_11(Transform pos)
    {
        if (!EF_11_room_chk&&Sp_Ef[11])
        {
            
            if (EF_11_illusion_instansi == null)
            {
                EF_11_illusion_instansi = Instantiate(EF_11_illusion.gameObject, pos.position, Quaternion.identity).GetComponent<spEF_11_illusion>();
            }
            else
            {
                EF_11_illusion_instansi.transform.position = pos.position;
                EF_11_illusion_instansi.gameObject.SetActive(true);
            }
            EF_11_room_chk = true;
        }
    }
    public void SP_11_room_chk()
    {
        if (Sp_Ef[11])
        {
            EF_11_room_chk = false;
        }
    }
}
