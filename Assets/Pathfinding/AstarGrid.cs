using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstarGrid : MonoBehaviour
{
   
    public Vector2 gridWorldsize;
    public float nodeRadius;

    float nodeDiameter;
    public int gridsizeX;
    public int gridsizeY;
    node[,] grid;
    public List<node> path;

    // Start is called before the first frame update
    void Start()
    {
        nodeDiameter = nodeRadius * 2;
        gridsizeX = Mathf.RoundToInt(gridWorldsize.x / nodeDiameter);
        gridsizeY = Mathf.RoundToInt(gridWorldsize.y / nodeDiameter);
        createGrid();
    }
    void createGrid()
    {
        grid = new node[gridsizeX, gridsizeY];
        Vector2 worlddBottomLeft = transform.position - Vector3.right * gridWorldsize.x / 2 - Vector3.up * gridWorldsize.y / 2;
        Vector2 worldpoint;
        bool obs=false;
        for (int x = 0; x < gridsizeX; x++)
        {
            for(int y = 0; y < gridsizeY; y++)
            {
               
                worldpoint = worlddBottomLeft + Vector2.right * (x * nodeDiameter + nodeRadius) + Vector2.up * (y * nodeDiameter + nodeRadius);
                Collider2D box= Physics2D.OverlapBox(worldpoint, new Vector2(nodeRadius,nodeRadius), 0);
                if (box != null)
                {
                    
                    if (box.gameObject.layer == 12)
                    {
                        obs = true;
                    }
                    else
                    {
                        obs = false;
                    }
                }
                else
                {
                    obs = false;
                }
                grid[x, y] = new node(obs,worldpoint,x,y);
            }
        }
    }
    public bool isground(node nd)
    {
        if (nd.gridY - 1 >= 0)
        {
            node a = grid[nd.gridX, nd.gridY - 1];
            if (a.obstacle)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }
    public bool isceiling(node nd)
    {
        if (nd.gridY + 1 < gridsizeY)
        {
            node a = grid[nd.gridX, nd.gridY + 1];
            if (a.obstacle)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }
    public List<node> getneighbornode(node nd)
    {
        List<node> neighbor = new List<node>();
        for(int x = -1; x <= 1; x++)
        {
            for(int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0) continue;
                int c_x = nd.gridX + x;
                int c_y = nd.gridY + y;

                if (c_x >= 0 && c_x < gridsizeX && c_y >= 0 && c_y < gridsizeY)
                {
                    neighbor.Add(grid[c_x, c_y]);
                }
            }
        }
        return neighbor;
    }
    public node GetNodeFromWorldPoint(Vector2 WorldPosition)
    {
        float percentX = (WorldPosition.x + gridWorldsize.x / 2) / gridWorldsize.x;
        float percentY = (WorldPosition.y + gridWorldsize.y / 2) / gridWorldsize.y;
        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        int x = Mathf.RoundToInt((gridsizeX - 1) * percentX);
        int y = Mathf.RoundToInt((gridsizeY - 1) * percentY);
        return grid[x, y];
    }
    
    
    private void OnDrawGizmos()
    {
        
        Gizmos.DrawWireCube(transform.position, new Vector2(gridWorldsize.x, gridWorldsize.y));
        if (grid != null)
        {
            foreach(node n in grid)
            {
                Gizmos.color = (n.obstacle) ? Color.black : Color.white;
                Gizmos.color = (n.no_path) ? Color.blue : Color.white;
                if (path != null)
                    if (path.Contains(n))
                        Gizmos.color = Color.yellow;
                Gizmos.DrawCube(n.pos, Vector3.one * (nodeDiameter - .1f));
            }
        }
    }
}
