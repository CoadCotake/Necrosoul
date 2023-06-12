using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStarter : MonoBehaviour
{
    public GameObject g;
    public GameObject[] gm;
    public int set_stage;
    void Awake()
    {
       
    }
    private void Start()
    {
        if (gm.Length == 1)
        {
            if (Gamemanager.GM.stage != 0)
            {
                Gamemanager.GM.stage = 0;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        gm = GameObject.FindGameObjectsWithTag("GameController");
        if (gm.Length==0)
        {
            Instantiate(g);

            Gamemanager.GM.stage = set_stage;
        }
        else if (gm.Length > 1)
        {
            Destroy(gm[gm.Length - 1]);
        }
    }
}
