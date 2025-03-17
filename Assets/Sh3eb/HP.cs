using UnityEngine;

public class HP : MonoBehaviour
{
    private double hp;

    public HP()
    {
        hp = 100;
    }
    public double Gethp()
    {
        return hp;
    }
    public void sethp(double hp) 
    {
       this.hp += hp;
    }
}
