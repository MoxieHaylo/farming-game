using UnityEngine;
using UnityEngine.Events;

public class GardenPlot : MonoBehaviour
{
    public bool hasPlant = false;
    [SerializeField] private GameObject plant0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTryPlant()
    {
        if (!hasPlant) 
        {
            PlantSeed();
        }
        Debug.Log("Already has seed");
    }

    public void PlantSeed()
    {
        hasPlant = true;
        Debug.Log("Planted a seed");
        Vector3 spawnPos = transform.position;
        Instantiate(plant0,spawnPos,Quaternion.identity);

    }
}
