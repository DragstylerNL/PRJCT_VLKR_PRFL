using UnityEngine;

[CreateAssetMenu(fileName = "Collection", menuName = "Characters/New Collection", order = 1)]
public class CharacterCollection : ScriptableObject
{
    public CharacterHolder[] characters;
}
