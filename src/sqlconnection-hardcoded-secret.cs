using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace SendMailTest
{
    class Program
    {
        static void Main(string[] args)
        {

               new SqlConnection(
				   // ruleid: sqlconnection-hardcoded-secret
                   @"Data Source=208.91.198.196;Initial Catalog=BookMe;User Id=upo133;Password=test1234");
                   SqlConnection con =
               new SqlConnection(
				    // ruleid: sqlconnection-hardcoded-secret
                   @"Data Source=208.91.198.196;Initial Catalog=BookMe;User Id=upo133;pwd=test1234");
                new SqlConnection(
					// ruleid: sqlconnection-hardcoded-secret
                   @"Data Source=208.91.198.196;Initial Catalog=BookMe;User Id=upo133;password=test1234");
                // ok: sqlconnection-hardcoded-secret
                new SqlConnection(
                   @"Data Source=208.91.198.196;Initial Catalog=BookMe;User Id=upo133;password=");
                // ok: sqlconnection-hardcoded-secret
                SqlConnection con =
                new SqlConnection(
                    @"Data Source=208.91.198.196;Initial Catalog=BookMe;User Id=upo133;Password=<password>");
                // ok: sqlconnection-hardcoded-secret
                new SqlConnection(
                    @"Data Source=208.91.198.196;Initial Catalog=BookMe;Password=;User Id=upo133");

        }
    }
}