using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopulationManager : MonoBehaviour
{
    public Text GenerationText;
    public Text PopulationSize;
    public Slider reproductionSlider;
    public Slider mutationSlider;

    public GameObject individualPrefab;
    public int initialPopulationSize = 10;
    public int numGenerations = 10;
    public float reproductionRate = 0.5f;
    public float mutationRate = 0.2f;

    private List<GameObject> population = new List<GameObject>();
    private int generation = 1;

    

    void Start()
    {
        InitializePopulation();
        UpdateUI();
    }

    void InitializePopulation()
    {
        for (int i = 0; i < initialPopulationSize; i++)
        {
            Vector3 spawnPosition = GetRandomSpawnPosition();
            GameObject individual = Instantiate(individualPrefab, spawnPosition, Quaternion.identity);
            // Access the Individual script of the instantiated GameObject.
            Individual individualScript = individual.GetComponent<Individual>();
            // Set individual properties, such as color or traits.
            individualScript.individualColor = Random.ColorHSV();
            population.Add(individual);
        }
    }

    void SimulateGeneration()
    {
        List<GameObject> newPopulation = new List<GameObject>();
        foreach (GameObject individual in population)
        {
            // Implement genetic mechanisms (e.g., reproduction and mutation) here.
            if (Random.value < reproductionRate)
            {
                Vector3 spawnPosition = GetRandomSpawnPosition();
                GameObject offspring = individual.GetComponent<Individual>().Reproduce(spawnPosition, mutationRate);
                newPopulation.Add(offspring);
            }

            if (individual.GetComponent<Individual>().age < 5f)
            {
                newPopulation.Add(individual);
            }
            else
            {
                Destroy(individual);
            }
        }
        population = newPopulation;
        generation++;
        UpdateUI();

    }

    Vector3 GetRandomSpawnPosition()
    {
        // Generate a random spawn position within your game world.
        return new Vector3(Random.Range(-5f, 5f), Random.Range(-5f, 5f), Random.Range(-5f, 5f));
    }

    void UpdateUI()
    {
        GenerationText.text = "Generation: " + generation;
        PopulationSize.text = "Population Size: " + population.Count;
    }
    void Update()
    {
        // Simulate generations over time.
        if (Input.GetKeyDown("space"))
        {
            if (generation <= numGenerations)
            {
                SimulateGeneration();
                Debug.Log("Generation " + generation + " completed. Population size: " + population.Count);
            }
        }

        reproductionRate = reproductionSlider.value;
        mutationRate = mutationSlider.value;
    }
}
