﻿namespace QuickJson
{
    internal class JsonNull : JsonNode
    {
        public static JsonNull Instance { get; } = new();
    }
}