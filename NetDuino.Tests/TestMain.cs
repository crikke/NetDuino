using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace NetDuino.Tests
{
    public class TestMain
    {
        public static IPrincipal SetUser()
        {
            //if (User.Identity.GetUserId() == ApplicationDbContext.Arduinos.Single(x => model.ArduinoID == x.Id).UserId)

            var identity = new TestIdentity("Hasse");


                ClaimsIdentity claimsIdentity = new ClaimsIdentity(identity);
            
            claimsIdentity.AddClaim(new Claim(ClaimTypes.NameIdentifier, "userId"));
            
            var principal = new GenericPrincipal(claimsIdentity, null);
            Thread.CurrentPrincipal = principal;
            return principal;
        }

    }

    public class TestPrincipal : IPrincipal
    {
        public IIdentity Identity { get; set; }

        public TestPrincipal(IIdentity identity)
        {
            Identity = identity;
        }

        public bool IsInRole(string role)
        {
            return true;
        }
    }

    public class TestIdentity : IIdentity
    {

        public TestIdentity(string name)
        {
            Name = name;
            AuthenticationType = "foobar";
        }
        // use this to override extension method
        public string GetUserId() => ID;
        public string Name { get; set; }
        public string AuthenticationType { get; set; }
        public bool IsAuthenticated
        {
            get
            {
                return true;
            }

        }
        public string ID { get; }
    }
}
