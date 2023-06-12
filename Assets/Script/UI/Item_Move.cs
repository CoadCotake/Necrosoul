using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item_Move : MonoBehaviour
{
    Vector3 dir;
    float time;
    public float _fadeTime = 10f;

    void Start()
    {
        resetAnim();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(dir * Time.deltaTime*200);
        if (time < _fadeTime)
        {
            GetComponent<Image>().color = new Color(1, 1, 1, 1f - time / _fadeTime);
        }
        else
        {
            time = 0;
            this.gameObject.SetActive(false);
        }
        time += Time.deltaTime;
    }

    public void resetAnim()
    {
        GetComponent<Image>().color = Color.white;
        this.gameObject.SetActive(true);
        transform.position = this.transform.position;
        dir = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), -1).normalized;
    }
}
