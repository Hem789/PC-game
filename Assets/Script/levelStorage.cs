using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class levelStorage : MonoBehaviour
{
    public static int currentLevel,yourScore, yourScores;
    public float normal,shoot,scoped,vol;
    //public static float stanormal,stashoot,stascoped;

    public Slider norm,shot,scop,volu;
    public int don,storedLevel;
    public Animator anime;
    private Vector3 distance;
    private GameObject player;
    public Text level,score,scores;
    /*void Awake()
    {
        if(norm)
        {
            levelData data=SaveManager.StoredItem();
            if(data!=null)
            {
            norm.value=data.normal;
            shot.value=data.zoom;
            scop.value=data.scoped;
            volu.value=data.volume;
            }
            else
            {
                Default();
            }
        }
    }*/
    void OnEnable()
    {
        if(norm)
        {
            levelData data=SaveManager.StoredItem();
            if(data!=null)
            {
            norm.value=data.normal;
            shot.value=data.zoom;
            scop.value=data.scoped;
            volu.value=data.volume;
            storedLevel=data.store;
            }
            else
            {
                Default();
            }
        }
    }
    void Start()
    {
        if(FindObjectOfType<GameManager>())
        {
        currentLevel=FindObjectOfType<GameManager>().level;
        //Debug.Log(currentLevel);
        if(currentLevel==6)
        {
            this.gameObject.SetActive(false);
        }
        
        }
        if(level)
        {
            if(currentLevel==2)
            {
                level.text= "Survival";
            }
            else
            {
                int h=currentLevel-2;
            level.text="Level "+h;    
            }
            score.text="Your Kills="+yourScore+" kills,";
            if(currentLevel!=4 && currentLevel!=6 & currentLevel!=7)
            {
                scores.text="Helicopters Destroyed="+yourScores+",";
            }
            else
            {
                scores.text=" ";
            }
        }
        
        
    }
    void Awake()
    {
        //if(currentLevel!=0)
        //{
        levelData data=SaveManager.StoredItem();
        if(data!=null)
        {
        normal=data.normal;
        shoot=data.zoom;
        scoped=data.scoped;
        vol=data.volume;
        storedLevel=data.store;
        //Debug.Log(normal+","+shoot+","+scoped);
        }

       // norm.value=.5F;
        //shot.value=.35F;
        //scop.value=.04F;
        //}
    }
    void Update()
    {
         if(FindObjectOfType<GameManager>())
        {
        yourScore=FindObjectOfType<GameManager>().Deaths;
        yourScores=FindObjectOfType<GameManager>().dstryed;

        }
        don=currentLevel;
        if(anime!=null)
        {
        player=GameObject.FindWithTag("Player");
        distance=transform.position-player.transform.position;
        if(distance.magnitude>30)
        {
            anime.enabled=false;
        }
        if(distance.magnitude<=30)
        {
            anime.enabled=true;
        }
        }
    }


    // Update is called once per frame
    public void Yes()
    {
        SceneManager.LoadScene(currentLevel);
    }

    // Update is called once per frame
    public void No()
    {
        SceneManager.LoadScene(0);
    }
    void OnTriggerEnter()
    {
        /*levelData data=SaveManager.StoredItem();
        if(data!=null)
        {
        normal=data.normal;
        shoot=data.zoom;
        scoped=data.scoped;
        vol=data.vol;
        }*/
        if(storedLevel+1>currentLevel)
        {
            don=storedLevel+1;
        }
        SaveManager.Save(this);
        
        if(FindObjectOfType<GameManager>())
        {
            FindObjectOfType<GameManager>().levelUP();
        }
        
    }
    public void Reset()
    {
        currentLevel=0;
        
        normal=norm.value;
        shoot=shot.value;
        scoped=scop.value;
        vol=volu.value;
        don=2;
        SaveManager.Save(this);
    }
   
    public void Default()
    {
        norm.value=10F;
        shot.value=5F;
        scop.value=1F;
        volu.value=.5F;
    }
    public void Save()
    {
        normal=norm.value;
        shoot=shot.value;
        scoped=scop.value;
        vol=volu.value;
        don=storedLevel+1;
        SaveManager.Save(this);
        
    }
    

}
