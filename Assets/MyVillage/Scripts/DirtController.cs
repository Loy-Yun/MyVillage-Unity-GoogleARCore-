using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GoogleARCore;

public class DirtController : MonoBehaviour
{
    public float startTime;

    public GameObject Farm;

    public GameObject Dirt;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - startTime == 100) {
            Dirt.SetActive(false);
            Farm.SetActive(true);
        }
    }
}
