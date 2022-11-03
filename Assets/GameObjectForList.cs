using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SaveLoadSystem;

[RequireComponent(typeof(SaveableEntity))]

public class GameObjectForList : MonoBehaviour, ISaveable
{

    public List<string> gameObjects = new List<string>();

    private GameObject [] enemies;

    private GameObject pfCube;

    public int age;

    public string language;
    // Start is called before the first frame update
    void Start()
    {
        // gameObjects.Add(pfCube.GetComponent<SaveableEntity>().GetID());
        // Debug.Log(pfCube.GetComponent<SaveableEntity>().GetID() + " 1 Id");
        // pfCube.transform.position = new Vector3(-2, -2 , 0);
        // gameObjects.Add(pfCube.GetComponent<SaveableEntity>().GetID());
        // Debug.Log(pfCube.GetComponent<SaveableEntity>().GetID() + " 2 Id");
        // pfCube.transform.position = new Vector3(-5, -5 , 0);
        // Debug.Log(pfCube.transform.position);

        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject go in enemies) {
            gameObjects.Add(go.GetComponent<SaveableEntity>().GetID());
        }


        // pfCube = GameObject.Find("CubePrefabs");
        // gameObjects.Add(pfCube.GetComponent<SaveableEntity>().GetID());
    }

    // Update is called once per frame
    void Update()
    {
        
    }


   //------------------------------------
    // ISaveable implementation...
    //------------------------------------

    // Create a Serializable struct which contains all sorable data:
    // You don't need to save the location, rotation and scale, this will be done behind the scenes ;)
    [System.Serializable]
    struct PlayerData
    {
        public int age;
        public string language;
        // public Dictionary<PlayerTypeSO, int> tempDictionary;
        public List<string> primeGameObject;
    }
    public object SaveState()
    {
        // List<GameObject> primeGameObject = new List<GameObject>();
        // Instantiate the struct which contains the data we want to save and return it as object
        return new PlayerData()
        {
            age = age,
            language = language,
            primeGameObject = gameObjects,
            // tempDictionary = playerAmountDictionary,
        };
    }
    public void LoadState(object state)
    {
        PlayerData data = (PlayerData)state; // Receive a object which we need to
                                             // cast to extract our loaded data
        this.age = data.age;
        this.language = data.language;
        this.gameObjects = data.primeGameObject;
        // playerAmountDictionary = data.tempDictionary;
    }
    public bool NeedsToBeSaved()
    {
        // If this GameObject has a parent which also inherits from ISaveable,
        // than any return value of of childs will be ignored, because the parent will decide if the whole Object structure
        // gets saved or not.

        // Otherwise:
        // If true gets returned, this GameObject will be saved
        // If false gets returned, this GameObject will be ignored when saving
        return true;
    }

    // Return true, if this object needs to be reinstantiated at load or false if the loading is enough
    public bool NeedsReinstantiation()
    {
        return true;
    }
    public void GotAddedAsChild(GameObject obj, GameObject hisParent)
    {
        // This function lets you know, that somewere deeper in the hirarchy of this GameObject a loaded GameObject got added to the structure
        // You may want to know that so you can setup the relations you may need

    }

    public void PostInstantiation(object state)
    {
        PlayerData data = (PlayerData)state; // Receive a object which we need to cast to extract our loaded data
        // Will be called after all Objects got loaded and Parents of each Objects are set.
        // You may need to do stuff like in GotAddedAsChild(...) but with objects which are not childs of this
        // Then you do this stuff here, because here it is guaranteed, that all objects got Instantiated
        // You can find your target objects using:
        // SaveableEntity.FindID(string id) -> this will search all objects by the ID
        // just grab the .gameObject of the returned Object and use it
        // You may want to store the targets ID, so you can load back to the right object.
        // Call: obj.id on the SaveableEntity component of your target to get the ID

    }

}
