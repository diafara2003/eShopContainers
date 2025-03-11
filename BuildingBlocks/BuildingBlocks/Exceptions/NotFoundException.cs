
namespace BuildingBlocks.Exceptions;

public class NotFoundException:Exception
{
    public NotFoundException(string Message):base(Message)
    {
            
    }
    public NotFoundException(string Name,object Key): base($"Entity \"{Name}\" ({Key}) was not found.")
    {
        
    }
}
