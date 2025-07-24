 private SqlConnectionStringBuilder GetConnection(args)
        {           
                    // ruleid: jwt-dotnet-hardcoded-secret
                    var json = new JwtBuilder()
                    .Decode<IDictionary<string, object>>(jwtToken)
                    .WithSecret("aaaaaa");
                    // ok: jwt-dotnet-hardcoded-secret
                    var json = new JwtBuilder()
                    .Decode<IDictionary<string, object>>(jwtToken)
                    .WithSecret(config);
                    string password = "password";
                    // ruleid: jwt-dotnet-hardcoded-secret
                    var json = new JwtBuilder()
                    .Decode<IDictionary<string, object>>(jwtToken)
                    .WithSecret(password);
        }