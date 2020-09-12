using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GoogleARCore;

public class Farmcontroller : MonoBehaviour
{
    public GameObject Farm;

    public GameObject Plant;

    public GameObject Fruit;

    public GameObject Dirt;

    public float startTime;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time - startTime == 100) {
            Plant.SetActive(false);
            Fruit.SetActive(true);
        }

        if(false) { // 뽑으면
            Fruit.SetActive(false);
            Dirt.SetActive(true);
            Farm.SetActive(false);
        }
    }
}
