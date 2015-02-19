using System.Security.Cryptography.X509Certificates;

namespace ReviewMe.Model
{
    public interface ISoftDelete
    {
       bool IsActive { get; set; }
    }
}