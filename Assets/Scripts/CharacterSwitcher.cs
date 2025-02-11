using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSwitcher : MonoBehaviour
{
    public CharacterData characterData;

    public GameObject gameObject;

    public int selectedOption = 0;

     void Start()
    {
        
    }
    void NextOption() { 
    selectedOption++;
        if(selectedOption >=characterData.characterCount) { 
        selectedOption = 0;
        }
        UpdateCharacter(selectedOption);
    }
    void BackOption()
    {
        selectedOption--;
        if (selectedOption <0)
        {
            selectedOption = characterData.characterCount - 1;
     }
        UpdateCharacter(selectedOption);
    }

    public void UpdateCharacter(int selectedOption)
    {
        Character character = characterData.GetCharacter(selectedOption);

        gameObject = character.characterObject;
    }
}
