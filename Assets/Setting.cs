using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Setting
{

    public bool full_scren;
    public int screen_resolution_size_X;
    public int screen_resolution_size_Y;
    public float full_volume;
    public float bgm_volume;
    public int screen_resol_index;
    public float sfx_volume;
    //string Application.persistentDataPath + "/" + SavePath.path + "/" + "SSSSS.es3" = Application.persistentDataApplication.persistentDataPath + "/" + SavePath.path + "/" + "SSSSS.es3" + "/save/" + "setting.es3";
    public List<KeyCode> Keys = new List<KeyCode>();
    public Setting()
    {
       // load_setting();
    }
    // Update is called once per frame
   
    public void save_setting()
    {
        if(ES3.FileExists(Application.persistentDataPath + "/" + SavePath.path + "/" + "SSSSS.es3"))
        ES3.Save("setting", this, Application.persistentDataPath + "/" + SavePath.path + "/" + "SSSSS.es3");

        
    }
    
     public void load_setting()
    {
        if (ES3.FileExists(Application.persistentDataPath + "/" + SavePath.path + "/" + "SSSSS.es3"))
        {
            
            Setting s3 = ES3.Load<Setting>("setting", Application.persistentDataPath + "/" + SavePath.path + "/" + "SSSSS.es3");

            //if(ES3.KeyExists(s3.full_scren.ToString()))
                full_scren = s3.full_scren;
            //if(ES3.KeyExists(s3.screen_resolution_size_X.ToString()))
                screen_resolution_size_X = s3.screen_resolution_size_X;
           // if(ES3.KeyExists("screen_resolution_size_Y"))
                screen_resolution_size_Y = s3.screen_resolution_size_Y;
           // if(ES3.KeyExists("screen_resol_index"))
                screen_resol_index = s3.screen_resol_index;
           // if(ES3.KeyExists("full_volume"))
                full_volume = s3.full_volume;
           // if (ES3.KeyExists("bgm_volume"))
                bgm_volume = s3.bgm_volume;
          //  if (ES3.KeyExists("sfx_volume"))
                sfx_volume = s3.sfx_volume;
          //  if (ES3.KeyExists("Keys"))
            //{
             
                for (int i = 0; i < s3.Keys.Count; i++)
                {
                    Keys.Add(s3.Keys[i]);
                }
                if (s3.Keys.Count < 9)
                {
                    int a = 9 - s3.Keys.Count;
                    for(int i = 9-a; i <9; i++)
                    {
                        switch (i)
                        {
                            case 0:
                                Keys.Add(KeyCode.UpArrow);
                                break;
                            case 1:
                                Keys.Add(KeyCode.DownArrow);
                                break;
                            case 2:
                                Keys.Add(KeyCode.LeftArrow);
                                break;
                            case 3:
                                Keys.Add(KeyCode.RightArrow);
                                break;
                            case 4:
                                Keys.Add(KeyCode.Z);
                                break;
                            case 5:
                                Keys.Add(KeyCode.X);
                                break;
                            case 6:
                                Keys.Add(KeyCode.LeftShift);
                                break;
                            case 7:
                                Keys.Add(KeyCode.Tab);
                                break;
                            case 8:
                                Keys.Add(KeyCode.Escape);
                                break;
                        }
                    }
                }
           // }
                Debug.Log("불려오기");
            
        }
        else
        {
            initialize_setting();
            ES3.Save("setting", this, Application.persistentDataPath + "/" + SavePath.path + "/" + "SSSSS.es3");
            Debug.Log("새로만들기");
        }
    }
    public void initialize_setting()
    {
        set_original_key_setting();
        set_original_screen_resolution();
        set_original_volume();
    }
 
    public void set_original_key_setting()
    {
        Keys.Add(KeyCode.UpArrow);
        Keys.Add(KeyCode.DownArrow);
        Keys.Add(KeyCode.LeftArrow);
        Keys.Add(KeyCode.RightArrow);
        Keys.Add(KeyCode.Z);
        Keys.Add(KeyCode.X);
        Keys.Add(KeyCode.LeftShift);
        Keys.Add(KeyCode.Tab);
        Keys.Add(KeyCode.Escape);
        /*
        --
        Keys[Setting.KeyAction.UP] = KeyCode.UpArrow;
        Keys[Setting.KeyAction.DOWN] = KeyCode.DownArrow;
        Keys[Setting.KeyAction.LEFT] = KeyCode.LeftArrow;
        Keys[Setting.KeyAction.RIGHT] = KeyCode.RightArrow;
        Keys[Setting.KeyAction.ATTACK] = KeyCode.Space;
        Keys[Setting.KeyAction.ACTIVE] = KeyCode.E;
        Keys[Setting.KeyAction.INVENTORY] = KeyCode.Tab;
        Keys[Setting.KeyAction.PAUSE] = KeyCode.Escape;*/
    }
    public void set_original_screen_resolution()
    {
        full_scren = true;
        screen_resolution_size_X = 1920;
        screen_resolution_size_Y = 1080;
        screen_resol_index = 0;
    }
    public void set_original_volume()
    {
        full_volume = 1;
        sfx_volume = 1;
        bgm_volume = 1;
    }
}
