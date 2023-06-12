using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class setting_manager : MonoBehaviour
{
    public Setting S;
    AudioMixer am;
    public Key_manager k;
    public static setting_manager s_manger;
    public bool key_Make_chk;
    private void Awake()
    {
        s_manger = this;
        if (S == null)
        {
            S = new Setting();
        }
        S.load_setting();
        set_setting();
        DontDestroyOnLoad(this.gameObject);
   
    }
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
      
    }
    public void set_setting()
    {
        screen_resol(S.screen_resolution_size_X, S.screen_resolution_size_Y, S.full_scren);
        Key_setting(k);
        sound_manager();
    }
    public void screen_resol(int a, int b, bool ful)
    {
        Screen.SetResolution(a, b, ful);
    }
    public void new_setting()
    {
        Setting s_new = new Setting();
        s_new.initialize_setting();
        s_new.save_setting();
        S = s_new;
        set_setting();
    }
    public void Key_setting(Key_manager k)
    {
        if (!key_Make_chk)
        {
    

            Key_manager.Keys.Add(Key_manager.KeyAction.UP, S.Keys[0]);
            Key_manager.Keys.Add(Key_manager.KeyAction.DOWN, S.Keys[1]);
            Key_manager.Keys.Add(Key_manager.KeyAction.LEFT, S.Keys[2]);
            Key_manager.Keys.Add(Key_manager.KeyAction.RIGHT, S.Keys[3]);
            Key_manager.Keys.Add(Key_manager.KeyAction.ATTACK, S.Keys[4]);
            Key_manager.Keys.Add(Key_manager.KeyAction.JUMP, S.Keys[5]);
            Key_manager.Keys.Add(Key_manager.KeyAction.DASH, S.Keys[6]);
            Key_manager.Keys.Add(Key_manager.KeyAction.INVENTORY, S.Keys[7]);
            Key_manager.Keys.Add(Key_manager.KeyAction.PAUSE, S.Keys[8]);
            Debug.Log("ssss"+Key_manager.Keys[Key_manager.KeyAction.UP]);
            key_Make_chk = true;
        }
        else
        {


            Key_manager.Keys[Key_manager.KeyAction.UP] = S.Keys[0];
            Key_manager.Keys[Key_manager.KeyAction.DOWN] = S.Keys[1];
            Key_manager.Keys[Key_manager.KeyAction.LEFT] = S.Keys[2];
            Key_manager.Keys[Key_manager.KeyAction.RIGHT] = S.Keys[3];
            Key_manager.Keys[Key_manager.KeyAction.ATTACK] = S.Keys[4];
            Key_manager.Keys[Key_manager.KeyAction.JUMP] = S.Keys[5];
            Key_manager.Keys[Key_manager.KeyAction.DASH] = S.Keys[6];
            Key_manager.Keys[Key_manager.KeyAction.INVENTORY] = S.Keys[7];
            Key_manager.Keys[Key_manager.KeyAction.PAUSE] = S.Keys[8];
        }
    }
    public void sound_manager()
    {
        //am.SetFloat("bgm", S.full_volume * S.bgm_volume);
        //am.SetFloat("SFX", S.full_volume * S.sfx_volume);
    }
}
