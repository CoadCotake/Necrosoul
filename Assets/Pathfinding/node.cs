using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class node
{
    enum status
    {
        empty,
        obstacle

    }
    public bool obstacle;
    status n_status;
    public Vector2 pos;
    public int gridX;
    public int gridY;
    public int gridZ;
    public int G;
    public int H;
    public node Pnode;
    public short jumpLength;
    public bool no_path;
    public bool chk;
    public node(bool obs, Vector2 p,int ngridX,int ngridY)
    {
        no_path = obs;
        obstacle = obs;
        pos = p;
        gridX = ngridX;
        gridY = ngridY;
        chk = true;
       // Debug.Log("畴靛积己" + obs + p);
    }
    public node(bool obs,bool can, Vector2 p, int ngridX, int ngridY)
    {
        no_path = can;
        obstacle = obs;
        pos = p;
        gridX = ngridX;
        gridY = ngridY;
        chk = true;
        // Debug.Log("畴靛积己" + obs + p);
    }
    public int F
    {
        get { return G + H; }
    }
    // Start is called before the first frame update

}
