using UnityEngine;

public class Tree : MonoBehaviour
{
    // Public variable to store the tree's fruits
    public GameObject[] fruits;

    void Start()
    {
        // Disable all the fruits initially
        foreach (var fruit in fruits)
        {
            fruit.SetActive(false);
        }
    }

    public void DropFruits()
    {
        // This method is called when the tree is hit by the boomerang

        // Enable all the fruits to make them fall
        foreach (var fruit in fruits)
        {
            if (fruit != null)
            {
                fruit.SetActive(true);
            }
        }
    }
}