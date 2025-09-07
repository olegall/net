namespace DI_lifecycle;

internal class PostmanHandler
{
    private readonly ITransientPostmanService _transientPostman;
    private readonly IScopedPostmanService _scopedPostman;
    private readonly ISingletonPostmanService _singletonPostman;

    public PostmanHandler(ITransientPostmanService transientPostman, IScopedPostmanService scopedPostman, ISingletonPostmanService singletonPostman)
    {
        _transientPostman = transientPostman;
        _scopedPostman = scopedPostman;
        _singletonPostman = singletonPostman;
    }

    public void PickUpLetter()
    {
        _transientPostman.PickUpLetter(nameof(_transientPostman));
        _scopedPostman.PickUpLetter(nameof(_scopedPostman));
        _singletonPostman.PickUpLetter(nameof(_singletonPostman));
    }

    public void DeliverLetter()
    {
        _transientPostman.DeliverLetter(nameof(_transientPostman));
        _scopedPostman.DeliverLetter(nameof(_scopedPostman));
        _singletonPostman.DeliverLetter(nameof(_singletonPostman));
    }

    public void GetSignature()
    {
        _transientPostman.GetSignature(nameof(_transientPostman));
        _scopedPostman.GetSignature(nameof(_scopedPostman));
        _singletonPostman.GetSignature(nameof(_singletonPostman));
    }

    public void HandOverLetter()
    {
        _transientPostman.HandOverLetter(nameof(_transientPostman));
        _scopedPostman.HandOverLetter(nameof(_scopedPostman));
        _singletonPostman.HandOverLetter(nameof(_singletonPostman));
    }
}
