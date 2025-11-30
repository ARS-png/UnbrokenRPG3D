using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthSlider;
    public Slider easeHealthSlider;
    [SerializeField] float lerpSpeed = 0.05f;

    //public float maxHealth = 3;

    public float health;

    private HealthSystem healthSystem;

    GameObject player;

    private void Start()
    {
        //health = maxHealth;
        player = GameObject.FindGameObjectWithTag("Player");
        health = player.GetComponent<HealthSystem>().GetHealth();
        healthSlider.value = health;
        easeHealthSlider.value = health;

        healthSlider.maxValue = health;
        easeHealthSlider.maxValue = health;
    }

    private void Update()
    {
        //Debug.Log($"Health: {health}, Slider: {healthSlider.value}, EaseHealthSlider: {easeHealthSlider.value}");



        if (player != null)
        {
            if (player.TryGetComponent<HealthSystem>(out HealthSystem healthSystem))
            {
                health = healthSystem.GetHealth();
            }
        }
        else
        {
            health = 0;
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


}
