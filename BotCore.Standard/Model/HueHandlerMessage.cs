namespace BotCore.Models
{
    /// <summary>
    /// Simple object which holds a Message and a Success boolean after executing a Hue command
    /// </summary>
    public class HueHandlerMessage
    {
        /// <summary>
        /// Error or success message based on operation
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// Indicator if command was sucessfull
        /// </summary>
        public bool Success { get; set; }
    }
}