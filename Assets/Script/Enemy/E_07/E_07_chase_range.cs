using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_07_chase_range : MonoBehaviour
{
    public Transform E;
    public bool on_player;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = E.position;
    }
    private void OnTriggerEnter2D(Collider2D collision)
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
            //on_player = false;
        }
    }
}
