using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DonDestory : MonoBehaviour
{
    public GameObject Sound;
    // Start is called before the first frame update
    void Start()
    {
        Sound = this.gameObject;
        if (SceneManager.GetActiveScene().name == "Main 1" || SceneManager.GetActiveScene().name == "StageSelect")
        {
            DontDestroyOnLoad(Sound);
        }
        else
        {
            Destroy(Sound);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
