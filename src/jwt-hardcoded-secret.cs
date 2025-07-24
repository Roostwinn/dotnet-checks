using System.Diagnostics;
using System.IO.Compression;
using JWT;
using JWT.Algorithms;
using JWT.Builder;
using JWT.Serializers;
using Microsoft.AspNetCore.Mvc;
using MvcMovie.Models;

namespace Example.Foobar;

public class Foobar
{

    public void JwtTest1()
    {
        var payload = new Dictionary<string, object>
        {
            { "claim1", 0 },
            { "claim2", "claim2-value" }
        };

        IJwtAlgorithm algorithm = new HMACSHA256Algorithm();
        IJsonSerializer serializer = new JsonNetSerializer();
        IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
        IJwtEncoder encoder = new JwtEncoder(algorithm, serializer, urlEncoder);
        const string key = "razdvatri";

        // ruleid: jwt-hardcoded-secret
        var token = encoder.Encode(payload, key);
        Console.WriteLine(token);
    }

    public void JwtTest2()
    {
        IJsonSerializer serializer = new JsonNetSerializer();
        IDateTimeProvider provider = new UtcDateTimeProvider();
        IJwtValidator validator = new JwtValidator(serializer, provider);
        IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
        IJwtAlgorithm algorithm = new HMACSHA256Algorithm();
        IJwtDecoder decoder = new JwtDecoder(serializer, validator, urlEncoder, algorithm);

        // ruleid: jwt-hardcoded-secret
        var json = decoder.Decode(token, "secret123");
        Console.WriteLine(json);
    }

    public void JwtTest3()
    {
        // ruleid: jwt-hardcoded-secret
        var token = JwtBuilder.Create()
            .WithAlgorithm(new HMACSHA256Algorithm())
            .WithSecret("razdvatri")
            .AddClaim("exp", DateTimeOffset.UtcNow.AddHours(1).ToUnixTimeSeconds())
            .AddClaim("claim1", 0)
            .AddClaim("claim2", "claim2-value")
            .Encode();

        Console.WriteLine(token);
    }

    public void JwtTest3()
    {
        var secret = "secret123";
        var builder = JwtBuilder.Create();
        builder.WithAlgorithm(new HMACSHA256Algorithm());
        // ruleid: jwt-hardcoded-secret
        builder.WithSecret(secret);
        builder.AddClaim("exp", DateTimeOffset.UtcNow.AddHours(1).ToUnixTimeSeconds());
        builder.AddClaim("claim1", 0);
        builder.AddClaim("claim2", "claim2-value");

        var token = builder.Encode();

        Console.WriteLine(token);
    }

    public void OkJwtTest1()
    {
        var payload = new Dictionary<string, object>
        {
            { "claim1", 0 },
            { "claim2", "claim2-value" }
        };

        IJwtAlgorithm algorithm = new HMACSHA256Algorithm();
        IJsonSerializer serializer = new JsonNetSerializer();
        IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
        IJwtEncoder encoder = new JwtEncoder(algorithm, serializer, urlEncoder);
        const string key = getKey();

        // ok: jwt-hardcoded-secret
        var token = encoder.Encode(payload, key);
        Console.WriteLine(token);
    }

    public void OkJwtTest2(string secret)
    {
        var token = JwtBuilder.Create()
            .WithAlgorithm(new HMACSHA256Algorithm())
            // ok: jwt-hardcoded-secret
            .WithSecret(secret)
            .AddClaim("exp", DateTimeOffset.UtcNow.AddHours(1).ToUnixTimeSeconds())
            .AddClaim("claim1", 0)
            .AddClaim("claim2", "claim2-value")
            .Encode();

        Console.WriteLine(token);
    }

}
