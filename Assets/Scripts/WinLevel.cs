using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinLevel : MonoBehaviour
{
    public Material winMaterial;

    public MeshRenderer myRenderer;

    public GameObject menu;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("ball"))
        {
            myRenderer.material = winMaterial;
            show();
        }
    }

    public void hide()
    {
        menu.SetActive(false);
    }

    public void show()
    {
        menu.SetActive(true);
    }
}