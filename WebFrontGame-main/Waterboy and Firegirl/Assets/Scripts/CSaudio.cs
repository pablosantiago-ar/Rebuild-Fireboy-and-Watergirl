using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CSaudio : MonoBehaviour
{
    public AudioSource Rocks;
    public AudioSource Water;

    // Start is called before the first frame update
    void Start()
{
    StartCoroutine(waiter());
}

IEnumerator waiter()
{
    //Rotate 90 deg
    Rocks.Play();

    //Wait for 4 seconds
    yield return new WaitForSecondsRealtime(8);

    //Rotate 40 deg
    Water.Play();

    //Wait for 2 seconds
    yield return new WaitForSecondsRealtime(4);

    //Rotate 20 deg
    SceneManager.LoadScene("Sel.LVL2");
}
}
