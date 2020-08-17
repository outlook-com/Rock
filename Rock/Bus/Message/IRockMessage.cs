using System.Collections.Generic;

namespace Rock.Bus.Message
{
    /// <summary>
    /// Rock Message Interface
    /// </summary>
    public interface IRockMessage : ICloudEvent<ICollection<RockMessageData>>
    {
    }
}
