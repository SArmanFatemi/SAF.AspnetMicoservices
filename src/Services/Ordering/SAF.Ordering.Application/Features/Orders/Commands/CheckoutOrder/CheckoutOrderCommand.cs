using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAF.Ordering.Application.Features.Orders.Commands.CheckoutOrder;

public class CheckoutOrderCommand : IRequest<int>
{
	// TODO:
	// Format
	// Null problem
	// Convert Payment and BillingObject to searated objects
	public string UserName { get; set; }
	public decimal TotalPrice { get; set; }

	// BillingAddress
	public string FirstName { get; set; }
	public string LastName { get; set; }
	public string EmailAddress { get; set; }
	public string AddressLine { get; set; }
	public string Country { get; set; }
	public string State { get; set; }
	public string ZipCode { get; set; }

	// Payment
	public string CardName { get; set; }
	public string CardNumber { get; set; }
	public string Expiration { get; set; }
	public string CVV { get; set; }
	public int PaymentMethod { get; set; }
}
