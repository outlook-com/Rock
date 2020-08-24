using Rock.Bus.Message;

namespace Rock.Bus.Consumer
{
    /// <summary>
    /// 
    /// </summary>
    public class PersonUpdatedMessageData
    {
        /// <summary>
        /// Gets or sets the entity identifier.
        /// </summary>
        public int PersonId { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class PersonUpdatedMessage : RockMessage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EntityChangeMessage"/> class.
        /// </summary>
        public PersonUpdatedMessage()
        {
            Data = new PersonUpdatedMessageData();
        }

        /// <summary>
        /// Gets or sets the entity change message data.
        /// </summary>
        public PersonUpdatedMessageData PersonUpdatedMessageData
        {
            get => Data as PersonUpdatedMessageData;
            set => PersonUpdatedMessageData = value;
        }

        /// <summary>
        /// Gets or sets the entity type identifier.
        /// </summary>
        public int PersonId
        {
            get => PersonUpdatedMessageData.PersonId;
            set => PersonUpdatedMessageData.PersonId = value;
        }
    }
}
