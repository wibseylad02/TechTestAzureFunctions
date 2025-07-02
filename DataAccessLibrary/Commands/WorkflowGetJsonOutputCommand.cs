using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Commands
{
    /// <summary>
    /// Command object for retrieving workflow details from a JSON source.
    /// </summary>
    public class WorkflowGetJsonOutputCommand
    {
        public string CustomerId { get; set; }
        public string Property { get; set; }
    }
}
