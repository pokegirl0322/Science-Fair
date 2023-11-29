using UnityEngine;
using System.Collections;

public class Individual : MonoBehaviour
{
    public Color individualColor; // Represents a trait (e.g., color).
    public float age = 0f;

    // Method for reproduction (creating offspring with potential mutations).
    public GameObject Reproduce(Vector3 spawnPosition, float mutationRate)
    {
        // Create an offspring GameObject.
        GameObject offspring = Instantiate(gameObject, spawnPosition, Quaternion.identity);

        // Access the Individual script of the offspring.
        Individual offspringScript = offspring.GetComponent<Individual>();

        // Apply genetic changes, such as mutation, if desired.
        if (Random.value < mutationRate)
        {
            offspring.GetComponent<Renderer>().material.color = Random.ColorHSV();
        }

        offspringScript.age = 0f;

        return offspring;
    }

    void Update()
    {
        age+= Time.deltaTime;

        if (age > 5f)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}