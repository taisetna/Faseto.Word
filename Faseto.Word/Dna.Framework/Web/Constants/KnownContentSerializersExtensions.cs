namespace Dna
{
    /// <summary>
    /// 
    /// </summary>
    public static class KnownContentSerializersExtensions
    {
        public static string ToMimeString(this KnownConentSerializers serializers)
        {
            // Switch on the serializer
            switch (serializers)
            {
                // Json
                case KnownConentSerializers.Json:
                    return "application/json";
                // XML
                case KnownConentSerializers.Xml:
                    return "application/xml";
                // Unknown
                default:
                    return "application/octet-stream";
            }
        }
    }
}