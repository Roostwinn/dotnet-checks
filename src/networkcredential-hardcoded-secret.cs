 private A GetConnection(args)
        {
                NetworkCredential cre = new NetworkCredential();
                string password = "aaa";
                // ruleid: networkcredential-hardcoded-secret
                new NetworkCredential("username", "password");
                // ruleid: networkcredential-hardcoded-secret
                cre.Password = "aaaa";
                // ruleid: networkcredential-hardcoded-secret
                new NetworkCredential("username", password);
                // ruleid: networkcredential-hardcoded-secret
                cre.Password = password;
                // ok: networkcredential-hardcoded-secret
                new NetworkCredential("username", args[1]);
                // ok: networkcredential-hardcoded-secret
                cre.Password = args[1];

        }