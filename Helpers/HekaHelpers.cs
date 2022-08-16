using AutoMapper;
using System.Text;  
using System.Security.Cryptography;  

namespace HekaMiniumApi.Helpers{
    public static class HekaHelpers
    {
        public static V MapTo<T, V>(this T from, V to)
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<T, V>();
            });

            IMapper iMapper = config.CreateMapper();
            iMapper.Map<T, V>(from, to);

            return to;
        }

        public static int ToInt32(this string text)
        {
            return Convert.ToInt32(text);
        }

        public static string ComputeSha256Hash(string rawData)  
        {  
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())  
            {  
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));  
  
                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();  
                for (int i = 0; i < bytes.Length; i++)  
                {  
                    builder.Append(bytes[i].ToString("x2"));  
                }  
                return builder.ToString();  
            }  
        }
    }
}