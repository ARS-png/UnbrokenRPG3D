using UnityEngine;

public class EnemyAttackSound : MonoBehaviour
{
    [SerializeField] float enemyAttackVolume;
    public AudioClip audioClip;

    private void EnemyAttackSoundPlay()
    {
        AudioSource.PlayClipAtPoint(audioClip, transform.position, enemyAttackVolume);
    }
}
