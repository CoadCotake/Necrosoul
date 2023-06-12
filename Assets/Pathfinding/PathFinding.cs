using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinding : MonoBehaviour
{
    AstarGrid grid;

    public Transform start_pos;
    public Transform Target_pos;
    public List<node> warning_node;
    bool onground = false;
    bool atceiling = false;
    private void Awake()
    {
        grid = GetComponent<AstarGrid>();
    }
    
    // Update is called once per frame
    void Update()
    {
        FindPath(start_pos.position, Target_pos.position);
    }
    void FindPath(Vector2 StartPos,Vector2 TargetPos)
    {
        node startnode = grid.GetNodeFromWorldPoint(StartPos);
        node targetnode = grid.GetNodeFromWorldPoint(TargetPos);

        List<node> openList = new List<node>();
        HashSet<node> closeList = new HashSet<node>();
        openList.Add(startnode);
        
        while (openList.Count > 0)
        {
            node currentnode = openList[0];
            for(int i = 1; i < openList.Count; i++)
            {
                if (openList[i].F < currentnode.F || openList[i].F == currentnode.F && openList[i].H < currentnode.H)
                {
                    currentnode = openList[i];
                }
            }
            openList.Remove(currentnode);
            closeList.Add(currentnode);
            if (currentnode == targetnode)
            {
                RetracePath(startnode,targetnode);
                return;
            }
            foreach(node n in grid.getneighbornode(currentnode))
            {
                if (n.obstacle || closeList.Contains(n))
                    continue;
                

                int newcurrentToNeighborCost = currentnode.G+GetDistanceCost(currentnode,n);
                if (newcurrentToNeighborCost < n.G || !openList.Contains(n))
                {
                    n.G = newcurrentToNeighborCost;
                    n.H=GetDistanceCost(n,targetnode);
                    n.Pnode = currentnode;
                    if (!openList.Contains(n))
                        openList.Add(n);
                }
            }
        }
    }
    void FindPath(Vector2 StartPos, Vector2 TargetPos,int characterWidth,int chaaracterHeight,short maxcharacterJumpHeight)
    {
        
        node startnode = grid.GetNodeFromWorldPoint(StartPos);
        startnode.gridZ = 0;
        
        node targetnode = grid.GetNodeFromWorldPoint(TargetPos);
        
        if (grid.isground(startnode))
        {
            onground = true;
            startnode.jumpLength = 0;
        }
        else
        {
            startnode.jumpLength = (short)(maxcharacterJumpHeight * 2);
        }
       
        List<node> openList = new List<node>();
        HashSet<node> closeList = new HashSet<node>();
        Stack<int> touchedLocation = new Stack<int>();
        openList.Add(startnode);

        while (openList.Count > 0)
        {
            node currentnode = openList[0];
            
            for (int i = 1; i < openList.Count; i++)
            {
                if (openList[i].F < currentnode.F || openList[i].F == currentnode.F && openList[i].H < currentnode.H)
                {
                    currentnode = openList[i];
                }
            }
            openList.Remove(currentnode);
            closeList.Add(currentnode);
            if (currentnode == targetnode)
            {
                RetracePath(startnode, targetnode);
                return;
            }
            if (grid.isground(currentnode))
            {
                onground = true;
            }
            if (grid.isceiling(currentnode))
            {
                atceiling = true;
            }
            foreach (node n in grid.getneighbornode(currentnode))
            {
                if (n.obstacle || closeList.Contains(n))
                    continue;

                int newcurrentToNeighborCost = currentnode.G + GetDistanceCost(currentnode, n);
                if (newcurrentToNeighborCost < n.G || !openList.Contains(n))
                {
                    n.G = newcurrentToNeighborCost;
                    n.H = GetDistanceCost(n, targetnode);
                    n.Pnode = currentnode;
                    if (!openList.Contains(n))
                        openList.Add(n);
                }
            }
        }
    }
    void RetracePath(node startnode,node endnode)
    {
        List<node> path = new List<node>();
        node currentnode = endnode;
        while (currentnode != startnode)
        {
            path.Add(currentnode);
            currentnode = currentnode.Pnode;
        }
        path.Reverse();
        grid.path = path;
    }
   
    int GetDistanceCost(node nodeA,node nodeB)
    {
        int distX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int distY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

        if (distX > distY)
            return 14 * distY + 10 * (distX - distY);
        return 14 * distX + 10 * (distY - distX);
    }
}
///1)점프값 계산 2)노드 중복 처리 3)2번 처리하는 김에 y값 제한 해두기 4)점프값 바탕ㅇ으로 ㅁㅁㄹㄹㄷㄹㄷㄹ