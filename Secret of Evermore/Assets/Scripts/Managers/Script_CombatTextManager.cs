using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Script_CombatTextManager
{
    private List<GameObject> _textPool = new List<GameObject>();
    private GameObject _poolHolder;
    private GameObject _textPrefab;

    public Script_CombatTextManager()
    {
        _poolHolder = new GameObject("Combat text pool");
        _textPrefab = Resources.Load<GameObject>("Prefabs/UI/CombatText");
    }

    public void AddText(String text, Vector3 position)
    {
        var to = GetFreeTextObject();
        to.transform.position = position;
        to.GetComponentInChildren<Text>().text = text;
    }

    private GameObject GetFreeTextObject()
    {
        foreach (var o in _textPool)
        {
            //Return first inactive object in pool
            if (!o.activeSelf)
            {
                o.SetActive(true);
                var routine = DeactivateAfterTime(.5f, o);
                Script_GameManager.GetInstance().StartCoroutine(routine);
                return o;
            }
        }

        //If no objects available, create new one and add to pool
        var obj = GameObject.Instantiate(_textPrefab);
        _textPool.Add(obj);
        obj.transform.SetParent(_poolHolder.transform);

        //Deactivate text after a time
        var coroutine = DeactivateAfterTime(.5f, obj);
        Script_GameManager.GetInstance().StartCoroutine(coroutine);
        return obj;
    }

    private IEnumerator DeactivateAfterTime(float time, GameObject o)
    {
        yield return new WaitForSeconds(time);
        o.SetActive(false);
    }
}
