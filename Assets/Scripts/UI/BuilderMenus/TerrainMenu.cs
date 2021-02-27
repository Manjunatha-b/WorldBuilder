using UnityEngine;

public class TerrainMenu:BaseMenu{

    TerrainMenu(){
        layerName="Ground";
    }
    
    float clamper(float inp)
    {
        return ((int)(inp / 30)) * 30f;
    }

    override 
    public void drag()
    {
        
        if (Input.GetMouseButton(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 400.0f, 1 << LayerMask.NameToLayer("GroundZero")))
            {
                newObj.transform.position = new Vector3(clamper(hit.point.x),0,clamper(hit.point.z));
            }
        }
        else{
            isDrag = false;
            if(newObj.GetComponent<Collider>()!=null)
                newObj.GetComponent<Collider>().enabled =true;
        }
    }
}