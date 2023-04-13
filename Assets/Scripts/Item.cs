using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    GameObject gameControllerObj;
    GlobalVariables globalVariables;

    GameManager gameManager;
    GameObject gameManagerObj;
    ClickAndDrag clickAndDrag;
    Camera camera;

    SpriteRenderer spriteRenderer;
    [SerializeField] Sprite easySprite;

    [SerializeField] string trashType;
    bool onCorrectBin = false;
    bool onBin = false;

    void Start() 
    {
        camera = Camera.main;
        clickAndDrag = camera.GetComponent<ClickAndDrag>();

        gameControllerObj = GameObject.FindWithTag("GameController");
        globalVariables = gameControllerObj.GetComponent<GlobalVariables>();

        gameManagerObj = GameObject.FindWithTag("LevelController");
        gameManager = gameManagerObj.GetComponent<GameManager>();

        spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
        if(globalVariables.difficulty == "easy")
        {
            spriteRenderer.sprite = easySprite;
        }
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.name == trashType)
        {
            onCorrectBin = true;
        }
        else if(other.name != trashType)
        {
            onCorrectBin = false;
        }
        if(other.tag == "bin")
        {
            onBin = true;
        }
    }
    void OnTriggerExit2D(Collider2D other) 
    {
        if(other.tag == "bin")
        {
            onCorrectBin = false;
            onBin = false;
        }
    }

    public void CheckForBin() 
    {
        if(onBin == true)
        {
            if(onCorrectBin == true)
            {
                gameManager.ChangeScore(1);
            }
            else 
            {
                gameManager.ChangeScore(-1);
            }
            Destroy(this.gameObject);
        }
    }
}
