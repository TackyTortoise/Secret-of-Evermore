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
        _characterList.Add(new Script_PlayerCharacter(Script_PlayerCharacter.PlayerType.Hero));
        _characterList[0].GetVisualCharacter().transform.position = new Vector3(0,1,0);

        _characterList.Add(new Script_PlayerCharacter(Script_PlayerCharacter.PlayerType.Dog));
        (_characterList[1].GetVisualCharacter() as Script_CharacterBehaviour).SetActiveCharacter(false);
        (_characterList[1].GetVisualCharacter() as Script_CharacterBehaviour).SetFollowTarget(_characterList[0].GetVisualCharacter().transform);
        _characterList[1].GetVisualCharacter().transform.position = new Vector3(-1, 1, 0);

        SwitchPlayerCharacter();

        SpawnEnemies();
    }

    public void SwitchPlayerCharacter()
    {
        var other = _selectedCharacter;
        _selectedCharacter = _characterList.FirstOrDefault(x => x != _selectedCharacter);

        if (other != null)
        {
            (other.GetVisualCharacter() as Script_CharacterBehaviour).SetActiveCharacter(false);
            (other.GetVisualCharacter() as Script_CharacterBehaviour).SetFollowTarget(_selectedCharacter
                .GetVisualCharacter().transform);
        }

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
