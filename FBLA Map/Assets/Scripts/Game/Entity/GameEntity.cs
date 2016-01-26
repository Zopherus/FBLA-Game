using UnityEngine;
using System.Collections;

public interface GameEntity
{
    void SetSpeed(float speed);
    float GetSpeed();

    int GetTeam();
    void SetTeam(int team);

    Vector3 GetPos();

    void MoveTo(Vector3 position);
}
