private SqlConnectionStringBuilder GetConnection(args)
        {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                string password = "aaaa";
                // ruleid: sqlconnectionstringbuilder-hardcoded-secret
                builder.Password = "reee!";
                // ruleid: sqlconnectionstringbuilder-hardcoded-secret
                builder["Password"] = "reee!";
                var cb = new SqlConnectionStringBuilder();
                // ruleid: sqlconnectionstringbuilder-hardcoded-secret
                cb["Password"] = password;
                // ruleid: sqlconnectionstringbuilder-hardcoded-secret
                cb.Password = "reee!";
                // ok: sqlconnectionstringbuilder-hardcoded-secret
                builder.Password = args[1];
                // ok: sqlconnectionstringbuilder-hardcoded-secret
                builder["Password"] = args[1];
                // ok: sqlconnectionstringbuilder-hardcoded-secret
                cb["Password"] = args[1];
                // ok: sqlconnectionstringbuilder-hardcoded-secret
                cb.Password = args[1];

        }