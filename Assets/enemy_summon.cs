using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_summon : MonoBehaviour
{
    public GameObject Enemy;
    public GameObject end_effect;
    /*int n = 4;
    int i = 0;*/
    // Start is called before the first frame update
    private void Awake()
    {
        Enemy.SetActive(false);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void summon_enemy()
    {
        /*i++;
        if (n == i)
        {*/
            Enemy.SetActive(true);
            GameObject e = Instantiate(end_effect, this.transform.position, Quaternion.identity);
            e.transform.localScale = this.transform.localScale;
            Destroy(this.gameObject);
       // }
        /////////////
        ///GameObject e=Instanslate(Enemy.this.transform.position);
    }
}
