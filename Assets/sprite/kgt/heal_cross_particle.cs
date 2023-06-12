using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heal_cross_particle : MonoBehaviour
{
    ParticleSystem p;
    ParticleSystem.MainModule p_m;
    public int n;
    // Start is called before the first frame update
    void Start()
    {
        p = this.GetComponent<ParticleSystem>();
        p_m = p.main;
    }
    public void OnEnable()
    {
        p_m.maxParticles =1+ n * 2;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
