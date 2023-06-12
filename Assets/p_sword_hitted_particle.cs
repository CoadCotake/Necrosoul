using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class p_sword_hitted_particle : MonoBehaviour
{
    ParticleSystem m;
    ParticleSystem.MainModule m_main;
    float rand;
    public float setting_rotation=-444;
    public bool crit;
    Color normal;
    Color red;
    
    private void Awake()
    {
        m = this.GetComponent<ParticleSystem>();
        m_main = m.main;
        normal = m.main.startColor.color;
        red = Color.red;
    }
    void Start()
    {
        if (setting_rotation == -444)
        {
            rand = Random.Range(-91, 91);

            m_main.startRotation = rand;
        }
    }
    private void OnEnable()
    {
        if (setting_rotation == -444) {
            rand = Random.Range(-91, 91);
            m_main.startRotation = rand;
        }
        else
        {
            m_main.startRotation = setting_rotation;
        }
        if (crit)
        {
            transform.localScale = new Vector3(1f, 1f);
            m_main.startColor=red;
            crit = false;
        }
        else
        {
            m_main.startColor = normal;
            transform.localScale = new Vector3(0.5f, 0.5f);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
