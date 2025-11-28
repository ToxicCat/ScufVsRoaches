using UnityEngine;
using UnityEngine.SceneManagement;

public class GroundCheckKillEnemy : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            KillEnemy(other.gameObject);
        }
    }

    private void KillEnemy(GameObject enemyObject)
    {
        Destroy(enemyObject);
        //добавить импульс от напрыга
    }
}