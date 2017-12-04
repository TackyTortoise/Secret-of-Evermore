using UnityEngine;
using UnityEngine.SceneManagement;

public class Script_GameManager : MonoBehaviour
{
    private static Script_GameManager _instance;
    
    public Script_CharacterManager CharacterManager{get;private set;}
    public Script_UIManager UIManager { get; private set; }
    public Script_Inventory Inventory { get; private set; }
    public Script_CombatTextManager CombatTextManager { get; private set; }

    public bool GameOver = false;

    // Use this for initialization
    void Awake()
    {
        if (_instance != null)
            Debug.LogError("Creating multiple GameManagers!");
        _instance = this;

        Inventory = new Script_Inventory();
        CharacterManager = new Script_CharacterManager();
        UIManager = new Script_UIManager();
        CombatTextManager = new Script_CombatTextManager();

        Inventory.AddItem(new Script_HealthPotion(), 15);
        //Give player sword to start with
        var sword = new Script_Sword();
        Inventory.AddItem(sword);
        CharacterManager.GetCharacters()[0].EquipItem(sword);
    }

    public static Script_GameManager GetInstance()
    {
        if (_instance == null)
            Debug.LogError("No GameManager present in scene");

        return _instance;
    }

    void Update()
    {
        //Switch Character
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CharacterManager.SwitchPlayerCharacter();
        }
        //Open character panel
        if (Input.GetKeyDown(KeyCode.C))
        {
            UIManager.CharacterPanel.SwitchState();
        }
        //Open inventory panel
        if (Input.GetKeyDown(KeyCode.I))
        {
            UIManager.InventoryPanel.SwitchState();
        }
        //Close all panels
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UIManager.InventoryPanel.Hide();
            UIManager.CharacterPanel.Hide();
            UIManager.ShopPanel.Hide();
        }
        //Heal to full
        if (Input.GetKeyDown(KeyCode.H))
        {
            CharacterManager.GetCharacters().ForEach(x => x.Heal(500));
        }
        //Give all items
        if (Input.GetKeyDown(KeyCode.G))
        {
            Inventory.Currency = 1000;
            Inventory.AddItem(new Script_Axe());
            Inventory.AddItem(new Script_Spear());
            Inventory.AddItem(new Script_Armor("Bronze Chest", Script_Armor.ArmorType.Chest, 4));
            Inventory.AddItem(new Script_Armor("Bronze Helm", Script_Armor.ArmorType.Helm, 2));
            Inventory.AddItem(new Script_Armor("Bronze Legs", Script_Armor.ArmorType.Legs, 3));
            Inventory.AddItem(new Script_Armor("Iron Chest", Script_Armor.ArmorType.Chest, 6));
            Inventory.AddItem(new Script_Armor("Iron Helm", Script_Armor.ArmorType.Helm, 4));
            Inventory.AddItem(new Script_Armor("Iron Legs", Script_Armor.ArmorType.Legs, 5));
            var hp = new Script_HealthPotion();
            hp.Amount = 10;
            Inventory.AddItem(hp);
            UIManager.InventoryPanel.Refresh();
        }
    }

    public void ResetScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        GameOver = false;
    }

    public void CompleteGame(bool win)
    {
        UIManager.ShowGameComplete(win);
        Time.timeScale = 0f;
        GameOver = true;
    }
}
