using Rock.Bus.Message;

namespace Rock.Bus.Consumer
{
    /// <summary>
    /// 
    /// </summary>
    public class PersonAddedMessageData
    {
        /// <summary>
        /// Gets or sets the entity identifier.
        /// </summary>
        public int PersonId { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class PersonAddedMessage : RockMessage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EntityChangeMessage"/> class.
        /// </summary>
        public PersonAddedMessage()
        {
            Data = new PersonAddedMessageData();
        }

        /// <summary>
        /// Gets or sets the entity change message data.
        /// </summary>
        public PersonAddedMessageData PersonAddedMessageData
        {
            get => Data as PersonAddedMessageData;
            set => PersonAddedMessageData = value;
        }

        /// <summary>
        /// Gets or sets the entity type identifier.
        /// </summary>
        public int PersonId
        {
            get => PersonAddedMessageData.PersonId;
            set => PersonAddedMessageData.PersonId = value;
        }
    }
}
