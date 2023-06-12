using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class end_door : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
        
    }
    void Awake()
    {


    }
    // Update is called once per frame
    void Update()
    {
        

      
    }
    public void End_portal()
    {
        Gamemanager.GM.fade_out();
        Gamemanager.GM.room_end = true;
    }
   


   

}
