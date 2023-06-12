using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraManager : MonoBehaviour
{
    public static cameraManager cm;
    public action_camera action_cam;
    public GameObject main_cm;
    public GameObject main_cm_minimap;
    public GameObject boss_cm;
    public GameObject boss_cm_minimap;
    private void Awake()
    {
        cm = this;
        action_cam = this.transform.GetChild(1).GetComponent<action_camera>();
        main_cm= this.transform.GetChild(0).gameObject;
        main_cm_minimap = this.transform.GetChild(0).GetChild(0).gameObject;
        boss_cm = this.transform.GetChild(2).gameObject;
        boss_cm_minimap= this.transform.GetChild(2).GetChild(0).gameObject;
    }
    public void active_minimap()
    {
        if (Gamemanager.GM.boss)
        {
            boss_cm_minimap.SetActive(true);
        }
        else
        {
            main_cm_minimap.SetActive(true);
        }
    }
    public void disable_minimap()
    {
        if (Gamemanager.GM.boss)
        {
            boss_cm_minimap.SetActive(false);
        }
        else
        {
            main_cm_minimap.SetActive(false);
        }
    }
    public void active_action_cam()
    {
        action_cam.gameObject.SetActive(true);
        if (Gamemanager.GM.boss)
        {
            boss_cm.SetActive(false);
        }
        else
        {
            main_cm.SetActive(false);
        }
    }
    public void active_main_cam()
    {
        action_cam.gameObject.SetActive(false);
        if (Gamemanager.GM.boss)
        {
            boss_cm.SetActive(true);
        }
        else
        {
            main_cm.SetActive(true);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Gamemanager.GM.boss)
        {
            main_cm.SetActive(false);
        }
        else
        {
            boss_cm.SetActive(false);
        }
    }
}
