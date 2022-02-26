using System.Collections.ObjectModel;

namespace BotModel.Interfaces
{
    /// <summary>
    /// Набор свойств для классов представляющих экземпляры пользователей
    /// </summary>
    public interface IUser
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        long ID { get; }
        /// <summary>
        /// Имя пользователя
        /// </summary>
        string FirstName { get; set; }
        /// <summary>
        /// Фамилия пользователя
        /// </summary>
        string LastName { get; set; }
        /// <summary>
        /// Коллекция полученных сообщений от пользователя
        /// </summary>
        ObservableCollection<IMessage>  Messages { get; }
    }
}
