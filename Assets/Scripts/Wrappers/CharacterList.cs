using System.Collections.Generic;
using UnityEngine;

public class CharacterList : MonoBehaviour
{
    [System.Serializable]
    public class CharacterData
    {
        public string characterId; 
        public GameObject characterObject;
    }

    [SerializeField] private List<CharacterData> characters = new List<CharacterData>();
    private Dictionary<string, CharacterData> characterDictionary = new Dictionary<string, CharacterData>();

    private void Awake()
    {
        foreach (CharacterData character in characters)
        {
            if (!characterDictionary.ContainsKey(character.characterId))
            {
                characterDictionary.Add(character.characterId, character);
            }
        }
    }

    public GameObject GetCharacterObject(string characterId)
    {
        if (characterDictionary.ContainsKey(characterId))
            return characterDictionary[characterId].characterObject;

        return null;
    }

    public Animator GetCharacterAnimator(string characterId)
    {
        if (characterDictionary.ContainsKey(characterId))
        {
            return characterDictionary[characterId].characterObject.GetComponent<Animator>();
        }

        return null;
    }
}