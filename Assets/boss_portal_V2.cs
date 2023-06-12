using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss_portal_V2 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
 
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
           
        }
    }
}
