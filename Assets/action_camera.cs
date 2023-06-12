using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class action_camera : MonoBehaviour
{
   public enum action { p_death_cam};
    public action a=new action();
    public float timer;
    float t_chk;
    float t_total;
    Camera cm;
    // Start is called before the first frame update
    void Start()
    {
        cm = this.GetComponent<Camera>();
        t_chk = timer * Time.deltaTime;
    }
    private void OnDisable()
    {
        if (Time.timeScale != 1)
        {
            Time.timeScale = 1;
        }
        t_total = 0;
    }
    private void OnEnable()
    {
      
    }
    // Update is called once per frame
    void Update()
    {
        if (a == action.p_death_cam)
        {
            if(Gamemanager.GM.Player_obj!=null)
                this.transform.position = new Vector3(Gamemanager.GM.Player_obj.transform.position.x, Gamemanager.GM.Player_obj.transform.position.y, this.transform.position.z);
            cm.orthographicSize = 2.5f;
            if (t_total < 1)
            {
                t_total += t_chk;
                if (t_total > 1)
                    t_total = 1;
                Time.timeScale = t_total;
            }
            else
            {
                Time.timeScale = 1;
            }
        }
       
    }
}
