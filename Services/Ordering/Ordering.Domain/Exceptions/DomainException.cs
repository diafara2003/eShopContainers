

namespace Ordering.Domain.Exceptions;

public class DomainException:Exception
{
    public DomainException(string message)
        :base($"Domain Expcetion \"{message}\" throws from Damian Layer." )
    {
        
    }
}
