using System.Collections.ObjectModel;

namespace BotModel.Interfaces
{
    public interface IUser
    {
        long ID { get; }
        string FirstName { get; set; }
        string LastName { get; set; }
        ObservableCollection<IMessage>  Messages { get; }
    }
}
