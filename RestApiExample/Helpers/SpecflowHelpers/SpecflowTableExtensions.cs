using RestApiExample.Helpers.SpecflowHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using TechTalk.SpecFlow;

namespace RestApiExample.Helpers.SpecflowHelpers
{
    static class SpecflowTableExtensions
    {
        public static Dictionary<string, string> ToDictionary(this Table table)
        {
            if (table == null)
                throw new ArgumentNullException(nameof(table));

            if (table.Rows.Count == 0)
                throw new InvalidOperationException("Gherkin data table has no rows");

            if (table.Rows.First().Count != 2)
                throw new InvalidOperationException($@"Gherkin data table must have exactly 2 columns. Columns found: ""{string.Join(@""", """, table.Rows.First().Keys)}""");

            return table.Rows.ToDictionary(row => row[0], row => row[1]);
        }
    }
}
