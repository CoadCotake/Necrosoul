using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class door_contol : MonoBehaviour
{
    public GameObject door_open;
    public GameObject door_close;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void open_door()
    {
        Debug.Log("¹®¿­¸²");
        door_open.SetActive(true);
        door_close.SetActive(false);
    }
    public void close_door()
    {
        Debug.Log("¹®´ÝÈû");
        door_open.SetActive(false);
        door_close.SetActive(true);
    }
    
}
