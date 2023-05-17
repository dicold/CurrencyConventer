using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Constraints;
using System.Text;

namespace CurrencyConventerServer.Controllers
{
    [ApiController]
    [Route("api")]
    public class CurrencyConventerController : ControllerBase
    {
        [HttpGet("Conventer")]
        public string Get(string number)
        {
            var result = new StringBuilder();
            number = number.Replace(" ", "").Replace(",", ".");

            try
            {
                // check the correct input of number
                var currencyNumber = Convert.ToDecimal(number);
                if (currencyNumber > 999999999.99m)
                    throw new Exception("Number is too big");

                // if number is negative, result string should start with a word "minus"
                if (currencyNumber < 0)
                    result.Append("minus ");

                return CurrencyConventer.NumberIntoString(currencyNumber);

            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    
        static IEnumerable<string> ChunkFromEnd(string str, int chunkSize)
        {
            if (chunkSize >= str.Length)
            {
                yield return str;
                yield break;
            }

            int startIndex = Math.Max(0, str.Length - chunkSize);
            yield return str.Substring(startIndex);

            while (startIndex > 0)
            {
                startIndex = Math.Max(0, startIndex - chunkSize);
                int length = Math.Min(chunkSize, str.Length - startIndex);
                yield return str.Substring(startIndex, length);
            }
        }
    }
}