using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CSVReader : MonoBehaviour
{
    public string csvFileName="death_points"; // Name of your CSV file (without the file extension)
    public GameObject pointPrefab; // Prefab for the point to be instantiated

    void Start()
    {
        // Load the CSV data
        TextAsset csvData = Resources.Load<TextAsset>(csvFileName);
        string[] rows = csvData.text.Split('\n');

        foreach (string row in rows)
        {
            string[] columns = row.Split(',');
            if (columns.Length >= 2)
            {
                // Try parsing X and Y coordinates from CSV
                if (float.TryParse(columns[0], out float x) && float.TryParse(columns[1], out float y))
                {
                    // Create a point at the specified coordinates
                    Vector3 pointPosition = new Vector3(x, y, 0);
                    Instantiate(pointPrefab, pointPosition, Quaternion.identity);
                }
                else
                {
                    Debug.LogError("Failed to parse X and Y coordinates from the CSV row: " + row);
                }
            }
        }
    }
}
