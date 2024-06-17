using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject[] creature;
    public List<Transform> spawnPoints;

    private Shake shake;

    private void Start()
    {
        shake = GameObject.FindGameObjectWithTag("ScreenShake").GetComponent<Shake>();
    }

    public void ButtonPressed()
    {
        spawnPoints = new List<Transform>(spawnPoints);
        SpawnCreature();
    }

    void SpawnCreature()
    {
        int i = 0;
        var spawn = Random.Range(0, spawnPoints.Count);
        Instantiate(creature[i], spawnPoints[spawn].transform.position, Quaternion.identity);

        //AudioManager.instance.Play("SpawnSound");
        shake.CamShake();
    }
}
