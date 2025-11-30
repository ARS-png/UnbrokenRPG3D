using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    [SerializeField] Slider healthSlider;
    [SerializeField] Slider easeHealthSlider;
    [SerializeField] float lerpSpeed = 0.05f;
    public float health;

    //public float maxHealth = 100;


    [SerializeField] GameObject enemy;

    public void Start()
    {
        if (enemy != null)
        {
            if (enemy.TryGetComponent<Enemy>(out Enemy _enemy))
            {
                health = _enemy.GetHealth();
            }
        }


        healthSlider.maxValue = health;
        easeHealthSlider.maxValue = health;
    }

    private void Update()
    {
        if (enemy != null)
        {
            if (enemy.TryGetComponent<Enemy>(out Enemy _enemy))
            {
                health = _enemy.GetHealth();
            }
        }


        if (healthSlider.value != health)
        {
            healthSlider.value = health;
        }

        if (easeHealthSlider.value != healthSlider.value)
        {
            easeHealthSlider.value = Mathf.Lerp(easeHealthSlider.value, health, lerpSpeed);
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
    }

}
