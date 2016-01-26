using UnityEngine;
using System.Collections;

public interface GameEntity
{
    void SetSpeed(float speed);
    float GetSpeed();

    int GetTeam();
    void SetTeam(int team);

    float GetAttackRange();
    void SetAttackRange(float attackRange);

    float GetHealth();

    void StopMovement();

    FBLA.Game.AI.StateMachine<GameEntity> GetStateMachine();

    Vector3 GetPos();

    void DamageEnemy(GameEntity gameEntity);

    void TakeDamage(float damage);

    float GetDamage();

    void MoveTo(Vector3 position);
    void SetDir(Vector3 lookAt);

    bool IsDead();
}
