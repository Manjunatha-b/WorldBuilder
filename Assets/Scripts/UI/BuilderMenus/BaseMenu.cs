using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 using UnityEngine.Events;
 using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BaseMenu : MonoBehaviour
{
    public string resourcePath;

    public GameObject mainMenu;
    public GameObject subMenu;

    public GameObject dummyNarrowButton;
    public GameObject dummyIconButton;

    private List<string> menuItems;
    private List<string> subMenuItems;

    private GameObject mainScrollContent;
    private GameObject subScrollContent;

    protected GameObject newObj;
    protected bool isDrag = false;

    protected string layerName;

    private int rotation;
    public GameObject rotationButton;
    private string[] rotationModes = {"0 Degrees", "90 Degrees", "180 Degrees", "270 Degrees"};

    void Start()
    {
        menuItems = parser(resourcePath+"main");
        subMenu.SetActive(false);
        rotation = 0;

        mainScrollContent = mainMenu.transform.Find("Viewport").gameObject.transform.Find("Content").gameObject;
        subScrollContent = subMenu.transform.Find("Viewport").gameObject.transform.Find("Content").gameObject;
        rotationUpdate();
        menuBuilder();
    }

    void rotationUpdate(){
        Button rotationTemp = rotationButton.GetComponent<Button>();
        rotationTemp.GetComponentInChildren<Text>().text = rotationModes[rotation];
    }

    void menuBuilder(){
        foreach(string item in menuItems){
            GameObject temp = Instantiate(dummyNarrowButton);
            temp.GetComponentInChildren<Text>().text = item; 
            Button tempButton = temp.GetComponent<Button>();
            tempButton.onClick.AddListener(()=>onMenuClicked(item));
            temp.transform.SetParent(mainScrollContent.transform);
        }
    }

    void onMenuClicked(string item){
        subMenuItems = parser(resourcePath+item);
        subMenuBuilder();
        mainMenu.SetActive(false);
        subMenu.SetActive(true);
    }

    void subMenuBuilder(){
        foreach(string item in subMenuItems){
            GameObject temp = Instantiate(dummyIconButton);
            Button tempButton = temp.GetComponent<Button>();
            Texture2D tempText = Resources.Load<Texture2D>(item + "/icon");
            Sprite tempSprite = Sprite.Create(tempText, new Rect(0.0f, 0.0f, tempText.width, tempText.height), new Vector2(0.5f, 0.5f), 100.0f);
            tempButton.image.sprite = tempSprite;

            EventTrigger trigger = tempButton.gameObject.AddComponent<EventTrigger>();
            var pointerDown = new EventTrigger.Entry();
            pointerDown.eventID = EventTriggerType.BeginDrag;
            pointerDown.callback.AddListener((e) => onSubMenuClicked(item));
            trigger.triggers.Add(pointerDown);

            tempButton.transform.SetParent(subScrollContent.transform);
        }
    }

    public void destroySubMenuChildren(){
        foreach(Transform child in subScrollContent.transform)
        {
            Destroy(child.gameObject);
        }
    }

    void onSubMenuClicked(string item){
        GameObject template = Resources.Load<GameObject>(item);
        newObj = Instantiate(template);
        newObj.transform.RotateAround(newObj.transform.position, Vector3.up, (rotation * 90) % 360);
        newObj.layer = LayerMask.NameToLayer(layerName);
        if(newObj.GetComponent<Collider>()!=null)
            newObj.GetComponent<Collider>().enabled =false;
        isDrag = true;
    }

    public List<string> parser(string textfile){
        List<string> parsedList = new List<string>();
        Debug.Log(textfile);
        string raw = Resources.Load<TextAsset>(textfile).text;
        string[] splits = raw.Split('\n');
        foreach(string item in splits)
            if(item!=""){
                parsedList.Add(item.Trim());
            }
        return parsedList;
    }

    void Update(){
        if(isDrag)
            drag();
    }

    public void onRotationButtonClick(){
        rotation=(rotation+1)%4;
        rotationUpdate();
    }

    public virtual void drag(){}
}
