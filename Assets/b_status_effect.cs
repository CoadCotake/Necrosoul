using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class b_status_effect : MonoBehaviour
{
    GameCharacter g;
    
    public int status_num;
    void Start()
    {
        g = this.transform.parent.GetComponent<GameCharacter>();
    }
    public bool status_chk()
    {
        for (int i = 0; i < g.B_status.Count; i++)
        {
            if (g.B_status[i].status_num == this.status_num)
            {
                return false;
            }
        }
        return true;
    }
    // Update is called once per frame
    void Update()
    {
        if (g != null)
        {
            if (status_chk())
            {
                Destroy(this.gameObject);
            }
        }
    }
}
