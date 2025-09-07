using Microsoft.Extensions.Logging;

namespace DI_lifecycle;

// почтальон (м.б. правильнее Postman)
internal class PostmanService : ITransientPostmanService, IScopedPostmanService, ISingletonPostmanService
{
    private readonly string _name;
    private readonly string[] _possibleNames = { "Peter", "Jack", "Bob", "Alex" };
    private readonly string[] _possibleLastNames = { "Brown", "Jackson", "Gibson", "Williams" };
    private readonly ILogger<PostmanService> _logger;

    public PostmanService(ILogger<PostmanService> logger)
    {
        _logger = logger;

        var rnd = new Random();
        _name = $"{_possibleNames[rnd.Next(0, _possibleNames.Length - 1)]} {_possibleLastNames[rnd.Next(0, _possibleLastNames.Length - 1)]}";

        _logger.LogInformation($"Hi! My name is {_name}.");
    }

    public void DeliverLetter(string postmanType)
    {
        _logger.LogInformation($"Postman {_name} delivered the letter. [{postmanType}]");
    }

    public void GetSignature(string postmanType)
    {
        _logger.LogInformation($"Postman {_name} got a signature. [{postmanType}]");
    }

    public void HandOverLetter(string postmanType)
    {
        _logger.LogInformation($"Postman {_name} handed the letter. [{postmanType}]");
    }

    public void PickUpLetter(string postmanType)
    {
        _logger.LogInformation($"Postman {_name} took the letter. [{postmanType}]");
    }
}
