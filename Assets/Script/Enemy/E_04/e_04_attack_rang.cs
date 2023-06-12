using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class e_04_attack_rang : MonoBehaviour
{
    public Transform t;
    public bool on_attack;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = t.position;
        transform.rotation = t.transform.GetChild(1).rotation;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            on_attack = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            on_attack = false;
        }
    }
}
