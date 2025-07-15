namespace Code.Scripts.Components.Interfaces
{
    public interface ISnapZone
    {
        bool CanAcceptCard(ACard card);
        void SnapCard(ACard card);
    }
}