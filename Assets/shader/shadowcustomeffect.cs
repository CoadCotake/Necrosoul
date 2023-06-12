using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shadowcustomeffect : MonoBehaviour
{
    public Material shadowMaterial;
    [Range(0,1)]
    public float shadow_thres;

    public Color shadowColor;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        shadowMaterial.SetFloat("_shadowThreshold", shadow_thres);
        shadowMaterial.SetColor("_shadowColor", shadowColor);
        Graphics.Blit(source, destination, shadowMaterial);
    }
}
