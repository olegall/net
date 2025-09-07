namespace DI_lifecycle;

public interface ITransientPostmanService 
{
    public void PickUpLetter(string postmanType);
    public void DeliverLetter(string postmanType);
    public void GetSignature(string postmanType);
    public void HandOverLetter(string postmanType);
}

public interface IScopedPostmanService 
{
    public void PickUpLetter(string postmanType);
    public void DeliverLetter(string postmanType);
    public void GetSignature(string postmanType);
    public void HandOverLetter(string postmanType);
}

public interface ISingletonPostmanService 
{
    public void PickUpLetter(string postmanType);
    public void DeliverLetter(string postmanType);
    public void GetSignature(string postmanType);
    public void HandOverLetter(string postmanType);
}

public interface IPostmanService
{
    void PickUpLetter(string postmanType);
    void DeliverLetter(string postmanType);
    void GetSignature(string postmanType);
    void HandOverLetter(string postmanType);
}