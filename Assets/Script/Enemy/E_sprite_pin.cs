using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_sprite_pin : MonoBehaviour
{
    GameCharacter g;
    Quaternion z;
    void Start()
    {
        g = this.transform.parent.GetComponent<GameCharacter>();
       
    }

    // Update is called once per frame
    void Update()
    {
        if (g.direction == 1 && this.transform.rotation.y % 360 != 0)
        {
            this.transform.Rotate(0, 180, 0);
        } else if (g.direction == -1 && this.transform.rotation.y % 360== 0)
        {
            this.transform.Rotate(0, 180, 0);
        }
    }
}
