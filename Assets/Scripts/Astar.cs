using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public class _Node {
    // Change this depending on what the desired size is for each element in the grid
    public static int _Node_SIZE = 32;
    public _Node Parent;
    public Vector2 Position;
    public Vector2 Center {
        get {
            return new Vector2(Position.X + _Node_SIZE / 2, Position.Y + _Node_SIZE / 2);
        }
    }
    public float DistanceToTarget;
    public float Cost;
    public float F {
        get {
            if (DistanceToTarget != -1 && Cost != -1)
                return DistanceToTarget + Cost;
            else
                return -1;
        }
    }
    public bool Walkable;

    public _Node(Vector2 pos, bool walkable) {
        Parent = null;
        Position = pos;
        DistanceToTarget = -1;
        Cost = 1;
        Walkable = walkable;
    }
}

public class Astar {
    List<List<_Node>> Grid;
    int GridRows {
        get {
            return Grid[0].Count;
        }
    }
    int GridCols {
        get {
            return Grid.Count;
        }
    }

    public Astar(List<List<_Node>> grid) {
        Grid = grid;
    }

    public Stack<_Node> FindPath(Vector2 Start, Vector2 End) {
        _Node start = new _Node(new Vector2((int)(Start.X / _Node._Node_SIZE), (int)(Start.Y / _Node._Node_SIZE)), true);
        _Node end = new _Node(new Vector2((int)(End.X / _Node._Node_SIZE), (int)(End.Y / _Node._Node_SIZE)), true);

        Stack<_Node> Path = new Stack<_Node>();
        List<_Node> OpenList = new List<_Node>();
        List<_Node> ClosedList = new List<_Node>();
        List<_Node> adjacencies;
        _Node current = start;

        // add start _Node to Open List
        OpenList.Add(start);

        while (OpenList.Count != 0 && !ClosedList.Exists(x => x.Position == end.Position)) {
            current = OpenList[0];
            OpenList.Remove(current);
            ClosedList.Add(current);
            adjacencies = GetAdjacent_Nodes(current);


            foreach (_Node n in adjacencies) {
                if (!ClosedList.Contains(n) && n.Walkable) {
                    if (!OpenList.Contains(n)) {
                        n.Parent = current;
                        n.DistanceToTarget = Math.Abs(n.Position.X - end.Position.X) + Math.Abs(n.Position.Y - end.Position.Y);
                        n.Cost = 1 + n.Parent.Cost;
                        OpenList.Add(n);
                        OpenList = OpenList.OrderBy(_Node => _Node.F).ToList<_Node>();
                    }
                }
            }
        }

        // construct path, if end was not closed return null
        if (!ClosedList.Exists(x => x.Position == end.Position)) {
            return null;
        }

        // if all good, return path
        _Node temp = ClosedList[ClosedList.IndexOf(current)];
        while (temp.Parent != start && temp != null) {
            Path.Push(temp);
            temp = temp.Parent;
        }
        return Path;
    }

    private List<_Node> GetAdjacent_Nodes(_Node n) {
        List<_Node> temp = new List<_Node>();

        int row = (int)n.Position.Y;
        int col = (int)n.Position.X;

        if (row + 1 < GridRows) {
            temp.Add(Grid[col][row + 1]);
        }
        if (row - 1 >= 0) {
            temp.Add(Grid[col][row - 1]);
        }
        if (col - 1 >= 0) {
            temp.Add(Grid[col - 1][row]);
        }
        if (col + 1 < GridCols) {
            temp.Add(Grid[col + 1][row]);
        }

        return temp;
    }
}
}
