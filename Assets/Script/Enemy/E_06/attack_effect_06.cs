using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attack_effect_06 : MonoBehaviour
{

    public bool hitted;
    public int Attack;
    public Vector2 hitted_force;
    bad_status b;
    // Start is called before the first frame update
    void Start()
    {
        b = new bad_status(0, 2);

    }
    private void OnDisable()
    {
        hitted = false;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        
                if (!hitted)
                {
                    if (collision.gameObject.CompareTag("Player"))
                    {
                        PlayerCharacter p = collision.GetComponent<PlayerCharacter>();
                p.get_bad_status(b);
                        p.hitted_event(this.transform.position,Vector2.zero);
                        p.player_hitted(Attack);
                
                    hitted = true;
                    }
                }
            
        
    }
}
