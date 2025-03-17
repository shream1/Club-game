using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class player_hp_system : MonoBehaviour
{
    HP hp ;
    public int health;
    bool outOfCompat;
    public double time;
    public double timer;
    public TextMeshProUGUI HpText;
    public GameObject[] images;
    void Start()
    {
        hp = new HP();
        
    }

    // Update is called once per frame
    void Update()
    {
        heal_system();
        time = Time.deltaTime;
        health = (int)hp.Gethp();
        HpText.text = health + "/100";

        HPUI();
    }

    private void HPUI()
    {
        switch (health)
        {
            case  <1:
                {
                    images[0].active = false;
                    images[1].active = false;
                    images[2].active = false;
                    images[3].active = false;
                }; break;
            case < 25:
                {
                    images[0].active = true;
                    images[1].active = false;
                    images[2].active = false;
                    images[3].active = false;
                }; break;
            case < 50: 
                {
                    images[0].active = true;
                    images[1].active = true;
                    images[2].active = false;
                    images[3].active = false;
                }; break;
            case < 75:
                {
                    images[0].active = true;
                    images[1].active = true;
                    images[2].active = true;
                    images[3].active = false;
                }; break;
            default:
                {
                    images[0].active = true;
                    images[1].active = true;
                    images[2].active = true;
                    images[3].active = true;
                }; break;
        }  
    }
    private void heal_system()
    {
        if (timer >= 15 && !(hp.Gethp() > 100))
        {
            healing();
        }

        if (outOfCompat && timer<15)
        {
            timer += time;
        }
    }
    private void healing()
    {
        hp.sethp(1*time);
    }

    private void OutOfCompat()
    {
        outOfCompat = true;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("bullet")) 
        { 
            hp.sethp(-15);
            timer = 0;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("bullet"))
        {
            OutOfCompat();
        }
    }

}
