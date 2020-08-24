using Rock.Bus.Message;

namespace Rock.Bus.Consumer
{
    /// <summary>
    /// 
    /// </summary>
    public class EntityChangeMessageData
    {
        /// <summary>
        /// Gets or sets the entity type identifier.
        /// </summary>
        public int EntityTypeId { get; set; }

        /// <summary>
        /// Gets or sets the entity identifier.
        /// </summary>
        public int EntityId { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class EntityChangeMessage : RockMessage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EntityChangeMessage"/> class.
        /// </summary>
        public EntityChangeMessage()
        {
            Data = new EntityChangeMessageData();
        }

        /// <summary>
        /// Gets or sets the entity change message data.
        /// </summary>
        public EntityChangeMessageData EntityChangeMessageData
        {
            get => Data as EntityChangeMessageData;
            set => EntityChangeMessageData = value;
        }

        /// <summary>
        /// Gets or sets the entity type identifier.
        /// </summary>
        public int EntityTypeId
        {
            get => EntityChangeMessageData.EntityTypeId;
            set => EntityChangeMessageData.EntityTypeId = value;
        }

        /// <summary>
        /// Gets or sets the entity identifier.
        /// </summary>
        public int EntityId
        {
            get => EntityChangeMessageData.EntityId;
            set => EntityChangeMessageData.EntityId = value;
        }
    }
}
