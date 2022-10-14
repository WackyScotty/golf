using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class setText : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshPro text;
    public ClubHit hit;
    private string _currentText;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _currentText = "Strokes: " + hit.strokeCount;
        text.text = _currentText;

    }
}
