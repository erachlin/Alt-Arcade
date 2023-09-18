using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public GameObject brickPrefab;
    public Material[] brickMaterials;
    public int rows = 5;
    public int columns = 8;
    public int depth = 2;  // Number of layers in the Z-axis
    //public float spacing = 1.2f;

    // Define the starting and ending points for each axis
    private float xStart = -10f;
    private float xEnd = 10f;
    private float yStart = 0f;
    private float yEnd = 6f;
    private float zStart = -16f;
    private float zEnd = -5f;
    private int lastNum = -1;

    void Start()
    {
        // Calculate the spacing based on the size of the area and the number of bricks
        float xSpacing = (xEnd - xStart) / (columns - 1);
        float ySpacing = (yEnd - yStart) / (rows - 1);
        float zSpacing = (zEnd - zStart) / (depth - 1);

        for (int y = 0; y < rows; y++)
        {
            for (int x = 0; x < columns; x++)
            {
                for (int z = 0; z < depth; z++)
                {
                    // Calculate the position for each brick
                    Vector3 position = new Vector3(xStart + x * xSpacing, yStart + y * ySpacing, zStart + z * zSpacing);
                    GameObject brick = Instantiate(brickPrefab, position, Quaternion.Euler(0, 90, 0));

                    int randomIndex = Random.Range(0, brickMaterials.Length);
                    while (randomIndex == lastNum) {
                        randomIndex = Random.Range(0, brickMaterials.Length);
                    }
                    lastNum = randomIndex;
                    brick.GetComponent<Renderer>().material = brickMaterials[randomIndex];

                }
            }
        }
    }
}
