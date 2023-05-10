using UnityEngine;
public interface IHealth
{
    public void OnDamage(int damage, Vector3 force);
    public void Death(Vector3 force);
}

public interface IHasDamage
{

}
