using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data : MonoBehaviour
{
    public CharacterMove characterMove;

    void SaveGame()
    {
        PlayerPrefs.SetFloat("SavedFloat", characterMove.countCrystall);
        PlayerPrefs.Save();
        Debug.Log("Game data saved");
    }

    void LoadGame()
    {
        if (PlayerPrefs.HasKey("SavedFloat"))
        {
            characterMove.countCrystall = PlayerPrefs.GetFloat("SavedFloat");
            Debug.Log("Game data loaded");
        }

        else
        {
            Debug.LogError("There is no save data!");
        }
    }

    void ResetData()
    {
        PlayerPrefs.DeleteAll();
        characterMove.countCrystall = 0f;
        Debug.Log("Data reset complete");
    }
}
