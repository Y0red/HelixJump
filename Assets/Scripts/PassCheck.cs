using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassCheck : MonoBehaviour {
[SerializeField]AudioSource audio;
    private void OnTriggerEnter(Collider other)
    {
        GameManager.singleton.AddScore(2);
        audio.Play();

        FindObjectOfType<BallController>().perfectPass++;
    }
}
