using UnityEngine;

public class EffectLife : MonoBehaviour
{
    private float timer = 0;
    
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > 2)
            Destroy(gameObject);
    }
}
