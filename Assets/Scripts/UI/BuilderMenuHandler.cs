using UnityEngine;

public class BuilderMenuHandler : MonoBehaviour
{
    private bool isInventoryOpen;
    private bool isTerrainOpen;
    private bool isDeletionMode;

    public GameObject inventory;
    private InventoryMenu inventoryMenu;
    public GameObject terrain;
    private TerrainMenu terrainMenu;
    
    void Start()
    {
        isInventoryOpen = false;
        isTerrainOpen = false;
        isDeletionMode = false;

        inventoryMenu = inventory.GetComponent<InventoryMenu>();
        terrainMenu = terrain.GetComponent<TerrainMenu>();

        inventory.SetActive(false);
        terrain.SetActive(false);
    }
    
    public void onInventoryButtonClick(){
        isInventoryOpen = !isInventoryOpen;
        if(isTerrainOpen)
            terrainMenu.destroySubMenuChildren();

        isTerrainOpen = false;
        isDeletionMode = false;

        if(isInventoryOpen==false)
            inventoryMenu.destroySubMenuChildren();

        inventoryMenu.mainMenu.SetActive(true);
        inventoryMenu.subMenu.SetActive(false);
        
        updateOpened();
    }

    public void updateOpened(){
        inventory.SetActive(isInventoryOpen);
        terrain.SetActive(isTerrainOpen);
    }

    public void onTerrainButtonClick(){
        isTerrainOpen = !isTerrainOpen;

        if(isInventoryOpen)
            inventoryMenu.destroySubMenuChildren();

        isInventoryOpen = false;
        isDeletionMode = false;

        if(isTerrainOpen == false)
            terrainMenu.destroySubMenuChildren();

        terrainMenu.mainMenu.SetActive(true);
        terrainMenu.subMenu.SetActive(false);
        
        updateOpened();
    }

    public void onDeletionButtonClick(){
        isDeletionMode = !isDeletionMode;

        if(isTerrainOpen)
            terrainMenu.destroySubMenuChildren();
        if(isInventoryOpen)
            inventoryMenu.destroySubMenuChildren();
        
        isTerrainOpen = false;
        isInventoryOpen = false;

        updateOpened();
    }

    void Update(){
        if(isDeletionMode){
            if(Input.GetMouseButtonDown(0)){
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, 400f,  1 << LayerMask.NameToLayer("Ground") | 1 << LayerMask.NameToLayer("Inventories")))
                {
                    Destroy(hit.transform.gameObject);
                }
            } 
        }
    }
}
