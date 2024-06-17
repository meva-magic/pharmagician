using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ClearCreatures : MonoBehaviour
{
    private Shake shake;
    private GameObject stains;

    public SpriteRenderer rend;
    public GameObject effect;    
    public GameObject bloodStain;

    void Start()
    {
        shake = GameObject.FindGameObjectWithTag("ScreenShake").GetComponent<Shake>();
        stains = GameObject.FindGameObjectWithTag("CenterPoint");
    }

    public void ButtonPressed()
    {
        ClearAll();
        shake.CamShake();
    }

    public void ClearAll()
    {
        AudioManager.instance.Play("Clear");

        foreach (GameObject creatures in GameObject.FindGameObjectsWithTag("Creature")) 
        {
            Instantiate(effect, creatures.transform.position, Quaternion.identity);
            Destroy(creatures);
		}

        GameObject newBloodStain = Instantiate(bloodStain, stains.transform.position, transform.rotation) as GameObject;
        rend = newBloodStain.GetComponent<SpriteRenderer>();

        StartCoroutine(Fade());
        Destroy(newBloodStain, 2f);
    }

    IEnumerator Fade()
    {
        float alphaVal = rend.color.a;
        Color tmp = rend.color;

        while (rend.color.a < 1)
        {
            alphaVal -= 0.01f;
            tmp.a = alphaVal;
            rend.color = tmp;

            yield return new WaitForSeconds(0.06f);
        }
    }
}
