using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListsTest : MonoBehaviour
{

    public List<GameObject> characters = new List<GameObject>();
    public List<Player> heroes = new List<Player>();
    public List<Player> enemies = new List<Player>();

    void Start()
    {
        //Find all GO in the scene with the Player script
        var allCharacters = FindObjectsOfType<Player>();

        //Add them to the "characters" list
        foreach (var myCharacter in allCharacters)
        {
            characters.Add(myCharacter.gameObject);
        }

        foreach (var actualCharacter in characters)
        {
            //Gets the info in the Player script
            var player = actualCharacter.GetComponent<Player>();

            //Set currentHP and MP equal to maxHP and MP for both Hero and Enemy GO's
            player.stats.currentHP = player.stats.maxHP;
            player.stats.currentMP = player.stats.maxMP;

            //Filters the characters list by tag, if it is "Hero" it goes to the heroes list, and if it is "Enemy" it goes to the enemies list
            if (actualCharacter.tag == "Hero")
                heroes.Add(player);
            else if (actualCharacter.tag == "Enemy")
                enemies.Add(player);

        }
    }

    
    void Update()
    {
        
    }
}
