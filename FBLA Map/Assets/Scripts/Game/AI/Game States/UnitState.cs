

namespace FBLA.Game.AI
{

    public interface UnitState<T>
    {
        void EnterState(T entity);
        void UpdateState(T entity);
        void ExitState(T entity);
    }

}