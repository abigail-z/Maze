using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    public GameObject node;
    public float nodeWidth;
    public int mazeSize;

    private Node[,] nodes;

    void Start ()
    {
        // this is to center the maze on the parent's position
        Vector3 offset = new Vector3((float)(mazeSize - 1) / 2 * nodeWidth, 0, (float)(mazeSize - 1) / 2 * nodeWidth);

        // instantiate nodes
        nodes = new Node[mazeSize, mazeSize];
        for (int x = 0; x < mazeSize; x++)
        {
            for (int y = 0; y < mazeSize; y++)
            {
                nodes[x, y] = Instantiate(node).GetComponent<Node>();
                nodes[x, y].transform.parent = transform;
                nodes[x, y].transform.position = new Vector3(transform.position.x - offset.x + x * nodeWidth,
                    transform.position.y + 0, transform.position.z - offset.z + y * nodeWidth);
            }
        }

        // link nodes
        for (int x = 0; x < mazeSize; x++)
        {
            for (int y = 0; y < mazeSize; y++)
            {
                // link left node
                if (x > 0)
                {
                    nodes[x, y].Left = nodes[x - 1, y];
                }

                // link right node
                if (x < mazeSize - 1)
                {
                    nodes[x, y].Right = nodes[x + 1, y];
                }

                // link up node
                if (y < mazeSize - 1)
                {
                    nodes[x, y].Up = nodes[x, y + 1];
                }

                // link down node
                if (y > 0)
                {
                    nodes[x, y].Down = nodes[x, y - 1];
                }
            }
        }

        // make a maze!
        DFSMaze();
    }

    // http://www.algosome.com/articles/maze-generation-depth-first.html
    private void DFSMaze ()
    {
        Queue<Node> Q = new Queue<Node>();

        // 1. Randomly select a node (or cell) N. 
        Node N = nodes[Random.Range(0, mazeSize), Random.Range(0, mazeSize)];

        while (true)
        {
            // 2. Push the node N onto a queue Q.
            Q.Enqueue(N);

            // 3. Mark the cell N as visited. 
            N.Visited = true;

            Direction neighborDirection;
            do
            {
                // 4. Randomly select an adjacent cell A of node N that has not been visited.
                neighborDirection = FindNeighborDirection(N);

                // If all the neighbors of N have been visited: 
                if (neighborDirection == Direction.None)
                {
                    // Continue to pop items off the queue Q until a node is encountered with at least one non-visited neighbor.
                    // Assign this node to N and go to step 4.
                    if (Q.Count > 0)
                    {
                        N = Q.Dequeue();
                    }
                    else
                    {
                        // If no nodes exist: stop.
                        return;
                    }
                }
            }
            while (neighborDirection == Direction.None);

            // 5. Break the wall between N and A.
            Node A = null;
            switch (neighborDirection)
            {
                case Direction.Left:
                    A = N.Left;
                    N.DestroyLeft();
                    A.DestroyRight();
                    break;
                case Direction.Right:
                    A = N.Right;
                    N.DestroyRight();
                    A.DestroyLeft();
                    break;
                case Direction.Up:
                    A = N.Up;
                    N.DestroyTop();
                    A.DestroyBottom();
                    break;
                case Direction.Down:
                    A = N.Down;
                    N.DestroyBottom();
                    A.DestroyTop();
                    break;
            }

            // 6. Assign the value A to N.
            N = A;

            // 7. Go to step 2.
        }
    }

    private static Direction FindNeighborDirection (Node N)
    {
        Direction neighborDirection;
        List<Direction> directions = new List<Direction> { Direction.Left, Direction.Right, Direction.Up, Direction.Down };
        while (directions.Count > 0)
        {
            Node A = null;
            int randomIndex = Random.Range(0, directions.Count);
            neighborDirection = directions[randomIndex];
            switch (neighborDirection)
            {
                case Direction.Left:
                    A = N.Left;
                    break;
                case Direction.Right:
                    A = N.Right;
                    break;
                case Direction.Up:
                    A = N.Up;
                    break;
                case Direction.Down:
                    A = N.Down;
                    break;
            }

            if (A != null && A.Visited == false)
            {
                // found an unvisited node!
                return neighborDirection;
            }

            // this node is visited, or null!
            // remove from possible directions
            directions.RemoveAt(randomIndex);
        }

        // checked all directions, no neighbors
        return Direction.None;
    }

    private enum Direction
    {
        None, Left, Right, Up, Down
    }
}
