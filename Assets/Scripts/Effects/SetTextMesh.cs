using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class SetTextMesh : MonoBehaviour
{
    TextMesh textMesh; 

    void Start()
    {
        textMesh = GetComponent<TextMesh>(); 
    }

    
    public void SetNewText(string newText)
    {
        textMesh = GetComponent<TextMesh>();
        textMesh.text = newText; 
    }

}
