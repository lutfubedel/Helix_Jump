using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [Header("Obstacles")]
    public Transform parent_obstacle;
    public GameObject[] obstacleList;

    [Header("Materials")]
    public Material[] colors;



    void Start()
    {
        
        for(int i=0,j=0; i<25;i++,j+=8)
        {
            int randSayi1 = Random.Range(0,4);
            Instantiate(obstacleList[randSayi1],new Vector3(0,-100-j,0),Quaternion.Euler(0,Random.Range(0,360),0),parent_obstacle);
        }

        GameObject[] deadly_blocks = GameObject.FindGameObjectsWithTag("DeadlyBlock");
        int randSayi2 = Random.Range(0,colors.Length);

        for(int i=0;i<deadly_blocks.Length;i++)
        {
            deadly_blocks[i].GetComponent<Renderer>().material = colors[randSayi2];
        }
  
    }


}
