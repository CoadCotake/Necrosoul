using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.UI.Button;

public class End_Panel : MonoBehaviour
{
    public Button End_Button;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            ButtonClickedEvent btn = End_Button.onClick;
            btn.Invoke();
        }
    }

}
