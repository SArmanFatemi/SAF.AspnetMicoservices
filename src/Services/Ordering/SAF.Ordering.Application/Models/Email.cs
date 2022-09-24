using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAF.Ordering.Application.Models;

public class Email
{
	// TODO: Resolve null problem
	public string To { get; set; }

	public string Subject { get; set; }

	public string Body { get; set; }
}
