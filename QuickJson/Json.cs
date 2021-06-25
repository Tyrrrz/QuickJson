namespace QuickJson
{
    public static class Json
    {
        public static JsonNode? TryParse(string source) => new JsonReader(source).TryReadDocument();

        public static JsonNode Parse(string source) => new JsonReader(source).ReadDocument();
    }
}