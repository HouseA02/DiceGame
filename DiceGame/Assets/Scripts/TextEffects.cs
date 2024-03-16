using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextEffects : MonoBehaviour
{
    public TMP_Text textComponent;

    [SerializeField]
    private float waveRange = 1f;
    [SerializeField]
    private int waveSpeed = 1;
    [SerializeField]
    private int shudderStrength= 1;
    private void Awake()
    {
        textComponent = GetComponent<TMP_Text>();
    }
    private void Update()
    {
        textComponent.ForceMeshUpdate();

        // Loops each link tag
        foreach (TMP_LinkInfo link in textComponent.textInfo.linkInfo)
        {
            if (link.GetLinkID() == "wavy")
            {

                // Loops all characters containing the rainbow link.
                for (int i = link.linkTextfirstCharacterIndex; i < link.linkTextfirstCharacterIndex + link.linkTextLength; i++)
                {
                    TMP_CharacterInfo charInfo = textComponent.textInfo.characterInfo[i]; // Gets info on the current character
                    int materialIndex = charInfo.materialReferenceIndex; // Gets the index of the current character material

                    Color32[] newColors = textComponent.textInfo.meshInfo[materialIndex].colors32;
                    Vector3[] newVertices = textComponent.textInfo.meshInfo[materialIndex].vertices;

                    // Loop all vertexes of the current characters
                    for (int j = 0; j < 4; j++)
                    {
                        if (charInfo.character == ' ') continue; // Skips spaces
                        int vertexIndex = charInfo.vertexIndex + j;

                        // Offset and Rainbow effects, replace it with any other effect you want.
                        //Vector3 offset = new Vector2(Mathf.Sin((Time.realtimeSinceStartup * waveSpeed) + (vertexIndex * waveRange.x)), Mathf.Cos((Time.realtimeSinceStartup * waveSpeed) + (vertexIndex * waveRange.y))) * 10f;
                        Vector3 offset = new Vector2(0f, Mathf.Cos(Time.realtimeSinceStartup * waveSpeed + (vertexIndex) * waveRange));
                        // Sets the new effects
                        newVertices[vertexIndex] += offset;
                    }
                }
            }

            if (link.GetLinkID() == "shudder")
            {

                // Loops all characters containing the rainbow link.
                for (int i = link.linkTextfirstCharacterIndex; i < link.linkTextfirstCharacterIndex + link.linkTextLength; i++)
                {
                    TMP_CharacterInfo charInfo = textComponent.textInfo.characterInfo[i]; // Gets info on the current character
                    int materialIndex = charInfo.materialReferenceIndex; // Gets the index of the current character material

                    Color32[] newColors = textComponent.textInfo.meshInfo[materialIndex].colors32;
                    Vector3[] newVertices = textComponent.textInfo.meshInfo[materialIndex].vertices;
                    Vector3 offset = new Vector2(Random.Range(-shudderStrength, shudderStrength), Random.Range(-shudderStrength, shudderStrength));
                    // Loop all vertexes of the current characters
                    for (int j = 0; j < 4; j++)
                    {
                        if (charInfo.character == ' ') continue; // Skips spaces
                        int vertexIndex = charInfo.vertexIndex + j;

                        // Offset and Rainbow effects, replace it with any other effect you want.
                        
                        // Sets the new effects
                        newVertices[vertexIndex] += offset;
                    }
                }
            }
        }

        textComponent.UpdateVertexData(TMP_VertexDataUpdateFlags.All);
    }
}
