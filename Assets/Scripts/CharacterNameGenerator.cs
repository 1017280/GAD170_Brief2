using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Functions to complete:
/// - Generate Names
/// </summary>
public class CharacterNameGenerator : MonoBehaviour
{
 
    [SerializeField] private TextAsset firstNamesText;
    [SerializeField] private TextAsset middleNamesText;
    [SerializeField] private TextAsset lastNamesText;

    private string[] firstNames;
    private string[] middleNames;
    private string[] lastNames;

    private string[] nouns;
    private string[] adjectives;

    public void Awake()
    {
        firstNames = firstNamesText.text.Split('\n');
        middleNames = middleNamesText.text.Split('\n');
        lastNames = lastNamesText.text.Split('\n');

        nouns = new string[]
        {
            "apple",
            "banana",
            "nut",
            "lord",
            "robot",
            "egg",
            "president",
            "cookie",
            "dealer",
            "cousin",
            "teacher",
            "student",
            "wife",
            "husband"
        };
        adjectives = new string[]
        {
            "handsome",
            "tall",
            "short",
            "funny",
            "long",
            "crazy",
            "sexy",
            "shady",
            "lonely",
            "happy",
            "angry",
            "offensive",
            "ugly",
            "fat",
            "skinny"
        };
    }

    /// <summary>
    /// Returns an Array of Character Names based on the number of namesNeeded.
    /// </summary>
    /// <param name="namesNeeded"></param>
    /// <returns></returns>
    public CharacterName[] GenerateNames(int namesNeeded)
    {
        CharacterName[] names = new CharacterName[namesNeeded]; 

        for (int i = 0; i < names.Length; i++)
        {
            names[i] = MakeRandomName();
        }

        return names;
    }

    private string PickRandomWord(string[] collection)
    {
        return collection[Random.Range(0, collection.Length)];
    }

    private CharacterName MakeRandomName()
    {
        return new CharacterName(PickRandomWord(firstNames), PickRandomWord(lastNames), PickRandomWord(middleNames), PickRandomWord(adjectives) + " " + PickRandomWord(nouns));
    }
}