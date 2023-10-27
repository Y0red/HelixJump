using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallChanger : MonoBehaviour
{
    int index = 0;
    [SerializeField]List<BallController> balls;
    [SerializeField] Button nextBtn, privBtn;
    [SerializeField]CameraController cam;
    void Start()
    {
        nextBtn.onClick.AddListener(Next);
        privBtn.onClick.AddListener(Priv);
    }

     void Update()
    {
        privBtn.interactable = true ? index > 0 : false;
        nextBtn.interactable = true ? index < balls.Count-1 : false;
    }

    void Next()
    {
        balls[index].gameObject.SetActive(false);
        index = (index + 1) % balls.Count;
        balls[index].gameObject.SetActive(true);
        cam.SetTarget(balls[index]);
    }
    void Priv()
    {
        balls[index].gameObject.SetActive(false);
        index = (index - 1) % balls.Count;
        balls[index].gameObject.SetActive(true);
        cam.SetTarget(balls[index]);
    }
}
