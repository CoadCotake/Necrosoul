using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class air_melee : MonoBehaviour
{
    public Vector3 air_melee_pos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //this.transform.position = Gamemanager.GM.Player_obj.transform.position + air_melee_pos;
        if (Gamemanager.GM.Player_obj.GetComponent<PlayerCharacter>().onground)
        {
            this.gameObject.SetActive(false);
        }
    }
}
