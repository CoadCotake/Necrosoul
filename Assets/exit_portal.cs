using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class exit_portal : MonoBehaviour
{
    public Animator m_ani;
    // Start is called before the first frame update
    private void Awake()
    {
        m_ani = this.GetComponent<Animator>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            m_ani.SetBool("Player_out", true);
        }
    }
}
