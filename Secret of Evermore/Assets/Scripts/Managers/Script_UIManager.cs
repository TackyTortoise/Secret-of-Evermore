using UnityEngine;
using UnityEngine.UI;

public class Script_UIManager
{
    public Script_CharacterPanel CharacterPanel { get; private set; }
    public Script_InventoryPanel InventoryPanel { get; private set; }
    public Script_ShopPanel ShopPanel { get; private set; }
    public GameObject Canvas { get; private set; }

    public int ActivePanels = 0;

    public Script_UIManager()
    {
        //Create character panel
        Canvas = GameObject.Find("Canvas");
        CharacterPanel = GameObject.Instantiate(Resources.Load<Script_CharacterPanel>("Prefabs/UI/CharacterPanel"));
        CharacterPanel.Initialise();
        CharacterPanel.transform.SetParent(Canvas.transform);
        CharacterPanel.transform.localPosition = Vector3.zero;
        CharacterPanel.Refresh();
        CharacterPanel.Hide();

        //Create inventory panel
        InventoryPanel = GameObject.Instantiate(Resources.Load<Script_InventoryPanel>("Prefabs/UI/InventoryPanel"));
        InventoryPanel.Initialise();
        InventoryPanel.transform.SetParent(Canvas.transform);
        InventoryPanel.transform.localPosition = Vector3.zero;
        InventoryPanel.Refresh();
        InventoryPanel.Hide();

        //Create shop panel
        ShopPanel = GameObject.Instantiate(Resources.Load<Script_ShopPanel>("Prefabs/UI/ShopPanel"));
        ShopPanel.Initialise();
        ShopPanel.transform.SetParent(Canvas.transform);
        ShopPanel.transform.localPosition = Vector3.zero;
        ShopPanel.Refresh();
        ShopPanel.Hide();
    }

    public void ShowGameComplete(bool win)
    {
        var endScreen = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/UI/GameCompleteScreen"));
        endScreen.transform.GetChild(0).GetComponent<Text>().text = win ? "Game Completed" : "Game Over";
        endScreen.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(Script_GameManager.GetInstance().ResetScene);
        endScreen.transform.SetParent(Canvas.transform);
        endScreen.transform.SetAsLastSibling();
        endScreen.transform.localPosition = Vector3.zero;
    }
}
