
public class TappableSnapArea : MonoBehaviour, ISnapArea
{
    private Transform _snapPoint;

    private void Awake()
    {
        _snapPoint = transform;
    }

    public bool CanAcceptCard(ACard card)
    {
        if (card.GetDataCard().cardType == 1)
        {
            return true;
        }

        return false;
    }

    public void SnapCard(ACard card)
    {
        if (card == null) return;

        if (CanAcceptCard(card))
        {
            card.transform.position = _snapPoint.position;
            card.transform.DORotate(new Vector3(0, 90, 0), 0.5f).SetEase(Ease.OutBack);
        }
    }
}