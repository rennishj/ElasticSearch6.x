using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ES6.POCO
{
    public static class ListExtensions
    {
        public static List<T> ServerFilter<T>(this List<T> collection, string filterText, params Func<T, string>[] matchFields)
        {
            if (collection == null)
                return new List<T>(0);

            if (string.IsNullOrWhiteSpace(filterText) || filterText == "*" || !collection.Any())
                return collection;

            var filter = filterText == null ? "" : Regex.Escape(filterText);
            filter = filter.Replace(@"\*", ".*").Replace(@"\?", ".").Replace(@"\ ", ".*");
            var match = new Regex(string.Concat(".*(", filter, ").*"),
                RegexOptions.Singleline | RegexOptions.CultureInvariant | RegexOptions.IgnoreCase);

            return collection.Where(r => matchFields.Any(m =>
            {
                var v = m(r);
                if (v == null) return false;
                return match.IsMatch(v);
            })).ToList();
        }
        //usage
        //var pHistory = pHistory.ServerFilter(query.Address, f => f.AgentAddress)
        //        .ServerFilter(query.AgentNumber, f => f.AgentNumber)
        //        .ServerFilter(query.Customer, f => f.CompanyName)
        //        .ServerFilter(query.State, f => f.State, f => f.State)
        //        .ServerFilter(query.RollupUnderwriter, f => f.RolledUpUnderwriter)
        //        .ServerFilter(query.ReferenceNumber, f => f.Reference)
        //        .ServerFilter(query.UserName, f => f.UserName)
        //        .ServerFilter(sb.ToString(), a => a.CompanyName,
        //            a => a.AgentNumber,
        //            a => a.Confirmation,
        //            a => a.RolledUpUnderwriter,
        //            a => a.UserName,
        //            a => a.Reference,
        //            a => a.FileNumber);
    }
}
