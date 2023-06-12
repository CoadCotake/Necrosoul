using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_07_back : MonoBehaviour
{
    public bool on_player;
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
            on_player = true;
        }
    }
    private void OnTriggeExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            on_player = false;
        }
    }
}
