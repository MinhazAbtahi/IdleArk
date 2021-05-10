using UnityEngine;

public abstract class AbstractPlayer : MonoBehaviour
{
    public int health;
    public int resource;
    public bool isDead;

    public abstract void Attack();
    public abstract void TakeDamage(int damage);
    public abstract void Die();
}
