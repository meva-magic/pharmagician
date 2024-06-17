using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearField : MonoBehaviour
{
    private Shake shake;
    public GameObject effect;

    void Start()
    {
        shake = GameObject.FindGameObjectWithTag("ScreenShake").GetComponent<Shake>();
    }

    public void ButtonPressed()
    {
        ClearAllCreatures();
        shake.CamShake();
    }

    public void ClearAllCreatures()
    {
        foreach (GameObject creaturesArray in GameObject.FindGameObjectsWithTag("Creature")) 
        {
            Instantiate(effect, creaturesArray.transform.position, Quaternion.identity);
            Destroy(creaturesArray);
		}
    }
}
