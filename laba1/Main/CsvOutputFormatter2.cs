using Entities.DataTransferObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main
{
    public class CsvOutputFormatter2 : TextOutputFormatter
    {
        public CsvOutputFormatter2()
        {
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/csv"));
            SupportedEncodings.Add(Encoding.UTF8);
            SupportedEncodings.Add(Encoding.Unicode);
        }
        protected override bool CanWriteType(Type type)
        {
            if (typeof(RealtyCompanyDto).IsAssignableFrom(type) ||
            typeof(IEnumerable<RealtyCompanyDto>).IsAssignableFrom(type))
            {
                return base.CanWriteType(type);
            }
            return false;
        }
        public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
        {
            var response = context.HttpContext.Response;
            var buffer = new StringBuilder();
            if (context.Object is IEnumerable<RealtyCompanyDto>)
            {
                foreach (var realtycompany in (IEnumerable<RealtyCompanyDto>)context.Object)
                {
                    FormatCsv(buffer, realtycompany);
                }
            }
            else
            {
                FormatCsv(buffer, (RealtyCompanyDto)context.Object);
            }
            await response.WriteAsync(buffer.ToString());
        }
        private static void FormatCsv(StringBuilder buffer, RealtyCompanyDto realtycompany)
        {

            buffer.AppendLine($"{realtycompany.Id},\"{realtycompany.Name},\"{realtycompany.Location}\",{realtycompany.Country}\"");
        }
    }
}
