using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class door_open_col : MonoBehaviour
{
    public door door_;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            door_.col_Player = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            door_.col_Player = false;
        }
    }
}
