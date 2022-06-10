using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public bool displayGridGizmos;
    public LayerMask unwalkableMask;
    public Vector2 gridWorldSize;
    public float nodeRadius;
    public terrainType[] walkableRegions;
    public int obstacleProximityPenalty = 10;
    LayerMask walkableMask;
    Dictionary<int, int> walkableRegionsDictionary = new Dictionary<int, int>();
    Node[,] grid;
    float nodeDiameter;
    int gridSizeX, gridSizeY;

    int penaltyMin = int.MaxValue;
    int penaltyMax = int.MinValue;

    public void Awake()
    {
        nodeDiameter = nodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);

        foreach (terrainType region in walkableRegions)
        {
            walkableMask.value |= region.terrainMask.value;
            walkableRegionsDictionary.Add((int)Mathf.Log(region.terrainMask.value, 2), region.terrainPenalty);
        }

        createGrid();
    }

    private void createGrid()
    {
        grid = new Node[gridSizeX, gridSizeY];
        Vector3 worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.forward * gridWorldSize.y / 2;

        for (int i = 0; i < gridSizeX; i++)
        {
            for (int j = 0; j < gridSizeY; j++)
            {
                Vector3 worldPoint = worldBottomLeft + Vector3.right * (i * nodeDiameter + nodeRadius) + Vector3.forward * (j * nodeDiameter + nodeRadius);
                bool walkable = !(Physics.CheckSphere(worldPoint, nodeRadius, unwalkableMask));
                int movementPenalty = 0;
    
                Ray ray = new Ray(worldPoint + Vector3.up * 50f, Vector3.down);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 1000f, walkableMask))
                {
                    walkableRegionsDictionary.TryGetValue(hit.collider.gameObject.layer, out movementPenalty);
                }

                if (!walkable)
                {
                    movementPenalty += obstacleProximityPenalty;
                }

                grid[i, j] = new Node(walkable, worldPoint, i, j, movementPenalty);
            }
        }

        BlurPenaltyMap(3);
    }

    public int maxSize { get { return gridSizeX * gridSizeY; } }

    void BlurPenaltyMap(int blurSize)
    {
        int kernelSize = blurSize * 2 + 1;
        int kernelExtents = (kernelSize - 1) / 2;

        int[,] penaltiesHorizontalPass = new int[gridSizeX, gridSizeY];
        int[,] penaltiesVerticalPass = new int[gridSizeX, gridSizeY];

        for (int i = 0; i < gridSizeY; i++)
        {
            for (int j = -kernelExtents; j <= kernelExtents; j++)
            {
                int sampleX = Mathf.Clamp(i, 0, kernelExtents);
                penaltiesHorizontalPass[0, i] += grid[sampleX, i].movementPenalty;
            }

            for (int k = 1; k < gridSizeX; k++)
            {
                int removeIndex = Mathf.Clamp(k - kernelExtents - 1, 0, gridSizeX);
                int addIndex = Mathf.Clamp(k + kernelExtents, 0, gridSizeX - 1);

                penaltiesHorizontalPass[k, i] = penaltiesHorizontalPass[k - 1, i] - grid[removeIndex, i].movementPenalty + grid[addIndex, i].movementPenalty;
            }
        }

        for (int i = 0; i < gridSizeX; i++)
        {
            for (int j = -kernelExtents; j <= kernelExtents; j++)
            {
                int sampleY = Mathf.Clamp(i, 0, kernelExtents);
                penaltiesVerticalPass[i, 0] += penaltiesHorizontalPass[i, sampleY];
            }

            int blurredPenalty = Mathf.RoundToInt((float)penaltiesVerticalPass[i, 0] / (kernelSize * kernelSize));
            grid[i, 0].movementPenalty = blurredPenalty;

            for (int k = 1; k < gridSizeY; k++)
            {
                int removeIndex = Mathf.Clamp(k - kernelExtents - 1, 0, gridSizeY);
                int addIndex = Mathf.Clamp(k + kernelExtents, 0, gridSizeY - 1);

                penaltiesVerticalPass[i, k] = penaltiesVerticalPass[i, k - 1] - penaltiesHorizontalPass[i, removeIndex] + penaltiesHorizontalPass[i, addIndex];
                blurredPenalty = Mathf.RoundToInt((float)penaltiesVerticalPass[i, k] / (kernelSize * kernelSize));
                grid[i, k].movementPenalty = blurredPenalty;

                if (blurredPenalty > penaltyMax)
                {
                    penaltyMax = blurredPenalty;
                }
                if (blurredPenalty < penaltyMin)
                {
                    penaltyMin = blurredPenalty;
                }
            }
        }
    }

    public List<Node> GetNeightbours(Node node)
    {
        List<Node> neightbours = new List<Node>();

        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                if (i == 0 && j == 0)
                {
                    continue;
                }

                int checkX = node.GridX + i;
                int checkY = node.GridY + j;

                if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY)
                {
                    neightbours.Add(grid[checkX, checkY]);
                }
            }
        }

        return neightbours;
    }

    public Node nodeFromWorldPoint(Vector3 worldPosition)
    {
        float percentX = (worldPosition.x + gridWorldSize.x / 2) / gridWorldSize.x;
        float percentY = (worldPosition.z + gridWorldSize.y / 2) / gridWorldSize.y;

        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
        int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);

        return grid[x, y];
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, 1, gridWorldSize.y));

        if (grid != null && displayGridGizmos)
        {
            foreach (Node n in grid)
            {
                Gizmos.color = Color.Lerp(Color.white, Color.black, Mathf.InverseLerp(penaltyMin, penaltyMax, n.movementPenalty));

                Gizmos.color = (n.walkable) ? Gizmos.color : Color.red;
                Gizmos.DrawCube(n.worldPosition, Vector3.one * (nodeDiameter));
            }
        }        
    }

    [System.Serializable]
    public class terrainType
    {
        public LayerMask terrainMask;
        public int terrainPenalty;
    }
}
