using UnityEngine;
using System.Collections;
using System;
using FBLA.Game.AI;

public class GameUnit : MonoBehaviour, GameEntity
{

    private const float HEALTH_BAR_WIDTH = 100.0f;
    private const float HEALTH_BAR_HEIGHT = 5.0f;
    private const float HEALTH_DAMAGE_DISP_TIME = 3.0f;


    private NavMeshAgent agent;
    private FBLA.Game.AI.StateMachine<GameEntity> _stateMachine;
    private float _timeSinceDamageTaken = -1.0f;

    // Variables to be set in the editor.
    public float UnitSpeed = 10.0f;
    public float MaxUnitHealth = 100.0f;
    public float AttackRange = 15.0f;
    public float Damage = 5.0f;
    public Texture2D HealthBarTexture = null;

    public bool Selected = false;
    // A team of zero is the player.
    public int Team = 0;
    public float CurrentUnitHealth = 100.0f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = UnitSpeed;
        _stateMachine = new FBLA.Game.AI.StateMachine<GameEntity>(this);
    }

	// Update is called once per frame
	void Update ()
    {
        UpdateIsSelected();
        _stateMachine.Update();

        if (_timeSinceDamageTaken > 0.0f)
        {
            _timeSinceDamageTaken += Time.deltaTime;
            if (_timeSinceDamageTaken > HEALTH_DAMAGE_DISP_TIME)
                _timeSinceDamageTaken = -1.0f;
        }
    }
    
    public void UpdateIsSelected()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (CameraBehavior.Selection.width < 0)
            {
                CameraBehavior.Selection.x += CameraBehavior.Selection.width;
                CameraBehavior.Selection.width = -CameraBehavior.Selection.width;
            }
            if (CameraBehavior.Selection.height < 0)
            {
                CameraBehavior.Selection.y += CameraBehavior.Selection.height;
                CameraBehavior.Selection.height = -CameraBehavior.Selection.height;
            }
            Vector3 camPos = Camera.main.WorldToScreenPoint(transform.position);
            camPos.y = CameraBehavior.InvertMouseY(camPos.y);
            Selected = CameraBehavior.Selection.Contains(camPos);
        }
        if (Selected)
            SetSelectedColor(Color.red);
        else
            SetSelectedColor(Color.white);
    }

    void OnGUI()
    {
        if (Event.current.type == EventType.Repaint && ShouldDrawHealthBar())
        {
            Vector3 shiftedPos = transform.position;
            shiftedPos.y += 10.0f;

            Vector3 screenSpacePoint = WorldToScreenPoint(shiftedPos);
            // Display the units health bar. Also draw it centered.
            float percentageFull = CurrentUnitHealth / MaxUnitHealth;
            GUI.DrawTexture(new Rect(screenSpacePoint.x - (HEALTH_BAR_WIDTH / 2.0f), screenSpacePoint.y, HEALTH_BAR_WIDTH * percentageFull, HEALTH_BAR_HEIGHT), HealthBarTexture);
        }
    }

    public bool ShouldDrawHealthBar()
    {
        return Selected || (_timeSinceDamageTaken > 0.0f);
    }

    public static Vector3 WorldToScreenPoint(Vector3 position)
    {
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(position);
        screenPosition.y = Screen.height - screenPosition.y;
        return screenPosition;
    }

    void SetSelectedColor(Color color)
    {
        // All the rendering components of the children must be set as well.
        Renderer[] childrenRenderers = GetComponentsInChildren<Renderer>() as Renderer[];
        foreach (Renderer childrenRenderer in childrenRenderers)
            childrenRenderer.material.color = color;
    }


    public Vector3 GetPos()
    {
        return transform.position;
    }

    public void MoveTo(Vector3 positionToMoveTo)
    {
        if (agent.destination != positionToMoveTo)
            agent.destination = positionToMoveTo;
    }

    public void SetSpeed(float speed)
    {
        UnitSpeed = speed;
    }

    public float GetSpeed()
    {
        return UnitSpeed;
    }

    public int GetTeam()
    {
        return Team;
    }

    public void SetTeam(int team)
    {
        Team = team;
    }

    public float GetAttackRange()
    {
        return AttackRange;
    }

    public void SetAttackRange(float attackRange)
    {
        AttackRange = attackRange;
    }

    public void StopMovement()
    {
        if (agent.destination != transform.position)
            agent.destination = transform.position;
    }

    public StateMachine<GameEntity> GetStateMachine()
    {
        return _stateMachine;
    }

    public float GetHealth()
    {
        return CurrentUnitHealth;
    }

    public void DamageEnemy(GameEntity gameEntity)
    {
        Debug.Log("Attacking enemy");
        // Spawn the particle system to show the unit is being show at.
        GameObject shotPS = (GameObject)Instantiate(Resources.Load("ShotPrefab"));
        // Orient the particle system such that it is pointing towards the object being shot at.

        Vector3 heading = gameEntity.GetPos() - this.GetPos();

        shotPS.transform.rotation = Quaternion.LookRotation(heading, Vector3.up);

        gameEntity.TakeDamage(this.GetDamage());
    }

    public void TakeDamage(float damage)
    {
        _timeSinceDamageTaken = 0.0f;
        CurrentUnitHealth -= damage;
    }

    public float GetDamage()
    {
        return Damage;
    }
}
