using LDAP.Models;
using System.DirectoryServices;

namespace LDAP
{
    public interface ILdapValidator
    {
        Resultado Validate(string userId, string password);
    }

    public class LdapManager : ILdapValidator
    {
        public readonly string DomainName;
        public readonly int PortNumber;

        public LdapManager(string domainName, int port = 389)
        {
            DomainName = domainName;
            PortNumber = port;
        }

        public Resultado Validate(string userId, string password)
        {
            try
            {
                string path = LdapPath();
                string username = UserFullId(userId);
                DirectoryEntry de = new DirectoryEntry(path, username, password, AuthenticationTypes.Secure);
                DirectorySearcher ds = new DirectorySearcher(de);
                SearchResult? SR = ds.FindOne();
                return new Resultado();
            }
            catch (System.Runtime.InteropServices.COMException ex)
            {
                return new Resultado(ex);
            }
        }

        public string UserFullId(string userId)
        {
            string value = string.Format(@"{0}@{1}", userId, DomainName);
            return value;
        }

        public string LdapPath()
        {
            string value = string.Format(@"LDAP://{0}:{1}", DomainName, PortNumber);
            return value;
        }
    }
}

