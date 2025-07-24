using System;
using Npgsql;

namespace a
{
    class Program
    {
        static void Main(string[] args)
        {
           NpgsqlConnectionStringBuilder urlBuilder = new NpgsqlConnectionStringBuilder();
           string password = "aaa";
            // ruleid: npgsqlconnectionstringbuilder-hardcoded-secret
            urlBuilder.Password = "aaaa";
            // ruleid: npgsqlconnectionstringbuilder-hardcoded-secret
            urlBuilder["Password"] = "aaaa";
            // ruleid: npgsqlconnectionstringbuilder-hardcoded-secret
            urlBuilder["Password"] = password;
            // ok: npgsqlconnectionstringbuilder-hardcoded-secret
            urlBuilder.Password = args[1];
            // ok: npgsqlconnectionstringbuilder-hardcoded-secret
            urlBuilder["Password"] = args[1];
        }
    }
}