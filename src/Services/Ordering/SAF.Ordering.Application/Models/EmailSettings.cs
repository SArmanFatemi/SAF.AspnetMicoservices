using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAF.Ordering.Application.Models;

public class EmailSettings
{
	// TODO: Resolve null problems
	public string ApiKey { get; set; }

	public string FromAddress { get; set; }

	public string FromName { get; set; }
}
