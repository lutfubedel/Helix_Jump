using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    [Header("Speeds")]
    public int jump_force;
    public int camera_speed;
 
    [Header("Scripts")]
    public UIManager uimanager;
    public ObstacleMovement obstacle;

    [Header("GameObjects")]
    public GameObject[] ball_frags;
    public GameObject camera_holder;
    public GameObject splash_prefab;
    public GameObject trail;

    [Header("Clips")]
    public AudioClip passable_block_sound;
    public AudioClip normal_block_sound;
    public AudioClip dead_sound;

    [Header("Values")]
    public bool isDead;
    public int score;
    public int hight_score;
    public int level;

    Rigidbody rb;
    AudioSource source;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        source = GetComponent<AudioSource>();

        score = PlayerPrefs.GetInt("Score");
        level = PlayerPrefs.GetInt("Level");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("NormalBlock"))
        {
            rb.velocity = new Vector3(0, jump_force * Time.deltaTime, 0);
            GameObject splash= Instantiate(splash_prefab,transform.position + new Vector3(0,-0.2f,0),Quaternion.Euler(90,0,Random.Range(0,360)));
            splash.transform.SetParent(collision.gameObject.transform);
            source.PlayOneShot(normal_block_sound);
            

        }

        if(collision.gameObject.CompareTag("DeadlyBlock"))
        {
            if(isDead==false)
            {
                GameObject splash = Instantiate(splash_prefab, transform.position + new Vector3(0, -0.2f, 0), Quaternion.Euler(90, 0, Random.Range(0, 360)));
                splash.transform.SetParent(collision.gameObject.transform);
                GetComponent<MeshRenderer>().enabled = false;
                source.PlayOneShot(dead_sound);
                print("bitti");
            }
            

            foreach (GameObject item in ball_frags)
            {
                item.GetComponent<Rigidbody>().isKinematic = false;
                item.GetComponent<SphereCollider>().isTrigger = false;

                Camera.main.GetComponent<CameraControler>().CameraShakeFonks();
                uimanager.WhiteEffectFonks();
                trail.SetActive(false);
                
                obstacle.move_speed = 0;
                jump_force = 0;
                camera_speed = 0;

            }

            PlayerPrefs.SetInt("Score", 0);

            if (score > PlayerPrefs.GetInt("HightScore"))
            {
                PlayerPrefs.SetInt("HightScore", score);
            }

            isDead = true;
        }

        if(collision.gameObject.CompareTag("FinishBlock"))
        {
            level++;
            
            PlayerPrefs.SetInt("Level", level);
            PlayerPrefs.SetInt("Score", score);

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("PassableBlock") && isDead==false)
        {
            score+=Random.Range(3,11);
            uimanager.fill_rate.fillAmount += 0.04f;
            source.PlayOneShot(passable_block_sound);

            Transform parent_Objeect =  other.transform.parent.gameObject.transform;

            for(int i=0;i<parent_Objeect.childCount-1;i++)
            {
                parent_Objeect.GetChild(i).GetComponent<Rigidbody>().isKinematic = false;
                parent_Objeect.GetChild(i).GetComponent<Rigidbody>().useGravity=true;

                Collider[] colliders = Physics.OverlapSphere(transform.position, 1000);

                foreach (Collider newCollider in colliders)
                {
                    Rigidbody rb = newCollider.GetComponent<Rigidbody>();
                    if(rb !=null)
                    {
                        rb.AddExplosionForce(1000, transform.position, 1000);
                    }
                }

                Destroy(parent_Objeect.GetChild(i).gameObject, 2);
            }
        }
    }


}
