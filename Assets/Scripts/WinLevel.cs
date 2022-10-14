using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinLevel : MonoBehaviour
{
    public Material winMaterial;

    public MeshRenderer myRenderer;

    public GameObject menu;

    public GameObject player;

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


    public void show()
    {
        menu.SetActive(true);
        Vector3 playerPosition = player.transform.position;
        Vector3 nextLevelPosition = playerPosition + (2.5f * player.transform.forward);
        nextLevelPosition.y = playerPosition.y;
        menu.transform.position = nextLevelPosition;
    }
}