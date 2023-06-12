using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class door : MonoBehaviour
{

    public GameObject door_obj;
    public GameObject wall_spr;
    public bool door_active;
    public Transform portal_exit;
    //public GameObject portal_in;
    public int direct;//0=left 1=right 2=up 3=down
    public GameObject link;
    public bool col_Player;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Awake()
    {
        //make_door_random();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public int make_door_random()//벽이 되거나 문이 되거나
    {
        int a = Mathf.RoundToInt(Random.Range(0, 2));
        if (a == 0)
        {
            active_door();
            return 1;
        }
        else
        {
            active_wall();
            return 0;
        }
    }

    public void active_door()
    {
        door_obj.SetActive(true);
        wall_spr.SetActive(false);
        door_active = true;
    }
    public void active_wall()
    {
        door_obj.SetActive(false);
        wall_spr.SetActive(true);
    }
    public void open_door()
    {
        door_obj.GetComponent<door_contol>().open_door();
    }
    public void close_door()
    {
        door_obj.GetComponent<door_contol>().close_door();
    }
    
}
