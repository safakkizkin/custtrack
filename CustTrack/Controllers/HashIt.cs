using CustTrack.Models.EntityFramework;
using System;
using System.Security.Cryptography;
using System.Text;

namespace CustTrack.Controllers
{
    public class HashIt
    {
        public string Hashit(string pass)
        {
            var data = Encoding.UTF8.GetBytes(pass);
            byte[] hash;
            using (SHA512 shaM = new SHA512Managed())
            {
                hash = shaM.ComputeHash(data);
            }
            var result = BitConverter.ToString(hash).Replace("-", "");
            return result;
        }
    }
}