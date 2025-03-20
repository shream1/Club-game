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
    [SerializeField] private GameObject deathEffect;
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

        if (timer<15)
        {
            timer += time;
        }
    }
    private void healing()
    {
        hp.sethp(1*time);
    }
    public void TakeDamage(int damage)
    {
        hp.sethp(-damage);
        if (health <= 0)
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
            gameObject.SetActive(false);
        }
        timer = 0;
    }

}
