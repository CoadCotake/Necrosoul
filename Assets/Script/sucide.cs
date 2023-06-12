using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sucide : MonoBehaviour
{
    public int enemyLife = 500; // 500프레임 동안 생존
    // Start is called before the first frame update
    void Start()
    {
        enemyLife = 500;
    }

    // Update is called once per frame
    void Update()
    {
        enemyLife -= 1; 
        if(enemyLife <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
