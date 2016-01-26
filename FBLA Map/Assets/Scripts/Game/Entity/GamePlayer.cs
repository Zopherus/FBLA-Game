using UnityEngine;
using System.Collections;
using FBLA.Game.AI;

public abstract class GamePlayer : MonoBehaviour, GameEntity
{

    private const float HEALTH_BAR_WIDTH = 100.0f;
    private const float HEALTH_BAR_HEIGHT = 5.0f;
    private const float HEALTH_DAMAGE_DISP_TIME = 3.0f;
    private const float ENEMY_TINT = 0.2f;

    private float _timeSinceDamageTaken = -1.0f;
    private float _timeSinceLastAttack = -1.0f;
    private FBLA.Game.AI.StateMachine<GameEntity> _stateMachine;

    public float UnitSpeed = 10.0f;
    public float MaxUnitHealth = 100.0f;
    public float AttackRange = 15.0f;
    public float DamageRate = 1.0f;
    public float Damage = 5.0f;
    public float CurrentUnitHealth = 100.0f;
    public int Team = 0;
    public Texture2D HealthBarTexture = null;

    // Use this for initialization
    public virtual void Start ()
    {
        _stateMachine = new FBLA.Game.AI.StateMachine<GameEntity>(this);

        if (GetTeam() != 0)
        {
            // Give the object a slight tint.
            Color setColor = Color.green;
            setColor.a = ENEMY_TINT;
            SetSelectedColor(setColor);
        }
    }

    // Update is called once per frame
    public virtual void Update()
    {
        if (IsDead())
        {
            Destroy(gameObject);
            this.GetStateMachine().ChangeState(new DyingState());
            return;
        }

        _stateMachine.Update();

        // Update all of the counters.
        if (_timeSinceDamageTaken >= 0.0f)
        {
            _timeSinceDamageTaken += Time.deltaTime;
            if (_timeSinceDamageTaken > HEALTH_DAMAGE_DISP_TIME)
                _timeSinceDamageTaken = -1.0f;
        }

        if (_timeSinceLastAttack >= 0.0f)
        {
            _timeSinceLastAttack += Time.deltaTime;
            if (_timeSinceLastAttack > DamageRate)
                _timeSinceLastAttack = -1.0f;
        }
    }

    void OnGUI()
    {
        if (Event.current.type == EventType.Repaint && ShouldRenderHealthBar())
        {
            RenderHealthBar();
        }
    }

    protected abstract bool ShouldRenderHealthBar();

    private void RenderHealthBar()
    {
        Vector3 shiftedPos = transform.position;
        shiftedPos.y += 10.0f;

        Vector3 screenSpacePoint = WorldToScreenPoint(shiftedPos);
        // Display the units health bar. Also draw it centered.
        float percentageFull = CurrentUnitHealth / MaxUnitHealth;
        GUI.DrawTexture(new Rect(screenSpacePoint.x - (HEALTH_BAR_WIDTH / 2.0f), screenSpacePoint.y, HEALTH_BAR_WIDTH * percentageFull, HEALTH_BAR_HEIGHT), HealthBarTexture);
    }

    protected void SetSelectedColor(Color color)
    {
        // All the rendering components of the children must be set as well.
        Renderer[] childrenRenderers = GetComponentsInChildren<Renderer>() as Renderer[];
        foreach (Renderer childrenRenderer in childrenRenderers)
            childrenRenderer.material.color = color;
    }

    protected static Vector3 WorldToScreenPoint(Vector3 position)
    {
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(position);
        screenPosition.y = Screen.height - screenPosition.y;
        return screenPosition;
    }

    public Vector3 GetPos()
    {
        return transform.position;
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

    public StateMachine<GameEntity> GetStateMachine()
    {
        return _stateMachine;
    }

    public float GetHealth()
    {
        return CurrentUnitHealth;
    }

    public void TakeDamage(float damage)
    {
        _timeSinceDamageTaken = 0.0f;
        CurrentUnitHealth -= damage;
    }

    public virtual void DamageEnemy(GameEntity gameEntity)
    {
        gameEntity.TakeDamage(this.GetDamage());
        Vector3 towards = gameEntity.GetPos() - this.GetPos();
        SetDir(towards);
        _timeSinceLastAttack = 0.0f;
    }

    public float GetDamage()
    {
        return Damage;
    }

    public bool IsDead()
    {
        return CurrentUnitHealth <= 0.0f;
    }

    public bool CanAttack()
    {
        return _timeSinceLastAttack == -1.0f;
    }

    public bool WasRecentlyAttacked()
    {
        return _timeSinceDamageTaken > 0.0f;
    }

    public void SetDir(Vector3 lookAt)
    {
        transform.rotation = Quaternion.LookRotation(lookAt, Vector3.up);
    }

    public abstract void StopMovement();
    public abstract void MoveTo(Vector3 position);
}
