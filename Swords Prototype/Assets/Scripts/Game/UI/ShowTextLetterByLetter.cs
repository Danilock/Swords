using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowTextLetterByLetter : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textMeshProTarget;
    string wholeText;

    private void Start()
    {
        wholeText = textMeshProTarget.text;
        textMeshProTarget.text = "";
    }

    public void ShowText() => StartCoroutine(TypeText(wholeText));

    IEnumerator TypeText(string sentence)
    {
        foreach(char letter in sentence.ToCharArray())
        {
            textMeshProTarget.text += letter;
            yield return new WaitForSeconds(.005f);
        }
    }
}
