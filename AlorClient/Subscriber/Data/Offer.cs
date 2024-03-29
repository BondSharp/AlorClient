﻿using AlorClient.Domain;
using System.Text.Json.Serialization;

namespace AlorClient;

public class Offer 
{
    [JsonPropertyName("price")]
    public double Price { get; set; }

    [JsonPropertyName("volume")]
    public int Volume { get; set; }

    public override string ToString()
    {
        return $"{Price}:{Volume}";
    }
}
