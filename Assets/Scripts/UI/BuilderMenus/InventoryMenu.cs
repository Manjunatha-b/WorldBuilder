using UnityEngine;

public class InventoryMenu: BaseMenu{

    InventoryMenu(){
        layerName = "Inventories";
    }

    override

    public void drag()
    { 
        if(Input.GetMouseButton(0)){
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 400.0f, 1 << LayerMask.NameToLayer("Ground")))
            {
                newObj.transform.position = hit.point;
            }
            
        }
        else{
            isDrag = false;
            if(newObj.GetComponent<Collider>()!=null)
                newObj.GetComponent<Collider>().enabled =true;
        }
    }
}