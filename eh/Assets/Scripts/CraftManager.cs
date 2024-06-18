using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class CraftManager : MonoBehaviour
{
    public Button craftButton;
    public List<GameObject> craftablePrefabs;
    public Dictionary<string, string> craftRecipes;

    public List<GameObject> objectsInCraftArea = new List<GameObject>();

    private void Awake()
    {
        craftRecipes = new Dictionary<string, string>
        {
            { "CircleSquare", "Triangle" },
            { "SquareTriangle", "Circle" },
        };
    }

    void Start()
    {
        craftButton.onClick.AddListener(OnCraftButtonClicked);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"Trigger Enter: {other.gameObject.name}");

        string objectName = GetOriginalName(other.gameObject.name);

        if (craftablePrefabs.Exists(prefab => prefab.name == objectName))
        {
            if (!objectsInCraftArea.Contains(other.gameObject))
            {
                objectsInCraftArea.Add(other.gameObject);
                Debug.Log($"Object {objectName} entered crafting area.");
            }
        }
        else
        {
            Debug.Log($"Object {objectName} is not a craftable prefab.");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log($"Trigger Exit: {other.gameObject.name}");

        if (objectsInCraftArea.Contains(other.gameObject))
        {
            objectsInCraftArea.Remove(other.gameObject);
            Debug.Log($"Object {GetOriginalName(other.gameObject.name)} exited crafting area.");
        }
    }

    void OnCraftButtonClicked()
    {
        Debug.Log($"Current objects in crafting area: {objectsInCraftArea.Count}");

        foreach (var obj in objectsInCraftArea)
        {
            Debug.Log($"Object in crafting area: {GetOriginalName(obj.name)}");
        }

        if (objectsInCraftArea.Count == 2)
        {
            // Получение имен объектов и сортировка их в алфавитном порядке для создания ключа
            List<string> objectNames = objectsInCraftArea.Select(obj => GetOriginalName(obj.name)).ToList();
            objectNames.Sort();
            string key = objectNames[0] + objectNames[1];
            Debug.Log($"Crafting with key: {key}");

            if (craftRecipes.ContainsKey(key))
            {
                string resultPrefabName = craftRecipes[key];
                GameObject resultPrefab = craftablePrefabs.Find(prefab => prefab.name == resultPrefabName);

                if (resultPrefab != null)
                {
                    GameObject objectToReplace = objectsInCraftArea[0];
                    Vector3 spawnPosition = objectToReplace.transform.position;

                    Instantiate(resultPrefab, spawnPosition, Quaternion.identity);
                    Debug.Log($"Created new object: {resultPrefab.name}");

                    List<GameObject> objectsToRemove = new List<GameObject>(objectsInCraftArea);

                    foreach (var obj in objectsToRemove)
                    {
                        Destroy(obj);
                        Debug.Log($"Destroyed object: {GetOriginalName(obj.name)}");
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

    private string GetOriginalName(string name)
    {
        if (name.EndsWith("(Clone)"))
        {
            return name.Substring(0, name.Length - 7);
        }
        return name;
    }
}
