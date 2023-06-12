using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_AI_01_range : MonoBehaviour
{
    public bool on_player;
    public Transform E;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = E.position;
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
