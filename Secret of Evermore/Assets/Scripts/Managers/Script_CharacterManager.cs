using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Script_CharacterManager
{
    private List<Script_Character> _characterList = new List<Script_Character>();
    private List<Script_Character> _enemyList = new List<Script_Character>();
    private Script_Character _selectedCharacter;

    public Script_CharacterManager()
    {
        var playerPos = GameObject.Find("PlayerSpawn");

        //Create hero
        _characterList.Add(new Script_PlayerCharacter(Script_Character.CharacterType.Hero));
        _characterList[0].GetVisualCharacter().transform.position = playerPos.transform.position;

        //Create dog and make it follow the hero
        _characterList.Add(new Script_PlayerCharacter(Script_Character.CharacterType.Dog));
        (_characterList[1].GetVisualCharacter() as Script_CharacterBehaviour).SetActiveCharacter(false);
        (_characterList[1].GetVisualCharacter() as Script_CharacterBehaviour).SetFollowTarget(_characterList[0].GetVisualCharacter().transform);
        _characterList[1].GetVisualCharacter().transform.position = playerPos.transform.position + 2 * Vector3.left;

        //Set hero as active character
        SwitchPlayerCharacter();

        //Spawn enemies
        SpawnEnemies();
    }

    public void SwitchPlayerCharacter()
    {
        //Save character to make other follow
        var other = _selectedCharacter;
        //Select other character
        _selectedCharacter = _characterList.FirstOrDefault(x => x != _selectedCharacter);

        //Make previously selected follow newly selected
        if (other != null)
        {
            (other.GetVisualCharacter() as Script_CharacterBehaviour).SetActiveCharacter(false);
            (other.GetVisualCharacter() as Script_CharacterBehaviour).SetFollowTarget(_selectedCharacter
                .GetVisualCharacter().transform);
        }

        //Set selected character as active and make camera follow
        (_selectedCharacter.GetVisualCharacter() as Script_CharacterBehaviour).SetActiveCharacter(true);
        Camera.main.GetComponent<Script_FollowCamera>().SetTarget(_selectedCharacter.GetVisualCharacter().transform);
    }

    private void SpawnEnemies()
    {
        var locations = GameObject.Find("EnemySpawnLocations").transform;
        foreach (Transform t in locations)
        {
            _enemyList.Add(new Script_EnemyCharacter());
            _enemyList[_enemyList.Count - 1].GetVisualCharacter().transform.position = t.position;
        }
    }

    public List<Script_Character> GetCharacters()
    {
        return _characterList;
    }

    public Script_Character GetSelectedCharacter()
    {
        return _selectedCharacter;
    }
}
