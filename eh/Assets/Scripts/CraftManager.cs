using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftManager : MonoBehaviour
{
    public GameObject craftingArea; 
    public Button craftButton; 
    public List<GameObject> craftablePrefabs; 
    public Dictionary<string, string> craftRecipes; 

    private List<GameObject> objectsInCraftArea = new List<GameObject>(); 

    void Start()
    {
        craftButton.onClick.AddListener(OnCraftButtonClicked);

        craftRecipes = new Dictionary<string, string>
        {
            { "CircleSquare", "Triangle" }, 
            { "SquareTriangle", "Circle" }, 
        };
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (craftablePrefabs.Contains(other.gameObject))
        {
            objectsInCraftArea.Add(other.gameObject);
            Debug.Log($"Object {other.gameObject.name} entered crafting area.");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (objectsInCraftArea.Contains(other.gameObject))
        {
            objectsInCraftArea.Remove(other.gameObject);
            Debug.Log($"Object {other.gameObject.name} exited crafting area.");
        }
    }

    void OnCraftButtonClicked()
    {
        Debug.Log($"Current objects in crafting area: {objectsInCraftArea.Count}");

        foreach (var obj in objectsInCraftArea)
        {
            Debug.Log($"Object in crafting area: {obj.name}");
        }

        if (objectsInCraftArea.Count == 2)
        {
            string key = objectsInCraftArea[0].name + objectsInCraftArea[1].name;
            Debug.Log($"Crafting with key: {key}");

            if (craftRecipes.ContainsKey(key))
            {
                string resultPrefabName = craftRecipes[key];
                GameObject resultPrefab = craftablePrefabs.Find(prefab => prefab.name == resultPrefabName);

                if (resultPrefab != null)
                {
                    Instantiate(resultPrefab, craftingArea.transform.position, Quaternion.identity);
                    Debug.Log($"Created new object: {resultPrefab.name}");

                    foreach (var obj in objectsInCraftArea)
                    {
                        Destroy(obj);
                        Debug.Log($"Destroyed object: {obj.name}");
                    }

                    objectsInCraftArea.Clear();
                }
                else
                {
                    Debug.LogError("Result prefab not found in craftablePrefabs list");
                }
            }
            else
            {
                Debug.LogError("Recipe not found");
            }
        }
        else
        {
            Debug.LogError("Not enough objects in crafting area");
        }
    }
}
