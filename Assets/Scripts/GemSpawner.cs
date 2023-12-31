using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class GemSpawner : MonoBehaviour
{
    public List<GameObject> gemPrefabs; // List of gem prefabs
    // public GameObject basketPrefab; // Basket prefab

    private List<GameObject> spawnedGems = new List<GameObject>(); // List to keep track of spawned gems
    // private GameObject spawnedBasket; // Reference to the spawned basket

    private ARTrackedImageManager arTrackedImageManager;

    // Specify the name or index of the image from the library that should trigger the ship appearance
    [SerializeField] private string targetImageName = "cavescene"; 

    private void OnEnable()
    {
        arTrackedImageManager = gameObject.GetComponent<ARTrackedImageManager>();
        arTrackedImageManager.trackedImagesChanged += OnImageChanged;
    }

    private void OnImageChanged(ARTrackedImagesChangedEventArgs obj)
    {
        foreach (ARTrackedImage image in obj.added)
        {
            // Check if the recognized image matches the target image            
            if (image.referenceImage.name == targetImageName)
            {
                // Iterate through gem prefabs and instantiate each at its own position without applying offset
                for (int i = 0; i < gemPrefabs.Count; i++)
                {
                    GameObject gem = Instantiate(gemPrefabs[i], gemPrefabs[i].transform.position, Quaternion.identity);
                    // gem.transform.parent = spawnedBasket.transform; // Gems are children of the basket
                    spawnedGems.Add(gem);
                }
            }
        }
    }
}
