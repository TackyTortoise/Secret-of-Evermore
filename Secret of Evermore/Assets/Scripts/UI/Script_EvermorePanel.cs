using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Script_EvermorePanel : MonoBehaviour
{
    public abstract void Initialise();
    public abstract void Refresh();
    private bool _active = false;

    public void Show()
    {
        if (!_active)
        {
            ++Script_GameManager.GetInstance().UIManager.ActivePanels;
            Time.timeScale = 0f;
        }
        gameObject.SetActive(true);
        transform.SetAsLastSibling();
        Refresh();
        _active = true;
    }
    public virtual void Hide()
    {
        if (_active)
        {
            --Script_GameManager.GetInstance().UIManager.ActivePanels;
            if (Script_GameManager.GetInstance().UIManager.ActivePanels == 0)
                Time.timeScale = 1f;
        }
        gameObject.SetActive(false);
        _active = false;
    }

    public void SwitchState()
    {
        if (_active)
            Hide();
        else
            Show();
    }

    public bool IsActive()
    {
        return _active;
    }
}
