using SAF.Ordering.Domain.Common;

namespace SAF.Ordering.Domain.Entities;

public class Order : BaseEntity
{
	public Order(
		string userName,
		decimal totalPrice,
		string firstName,
		string lastName,
		string emailAddress,
		string country,
		string state,
		string addressLine)
	{
		UserName = userName;
		TotalPrice = totalPrice;
		FirstName = firstName;
		LastName = lastName;
		EmailAddress = emailAddress;
		Country = country;
		State = state;
		AddressLine = addressLine;
	}

	// TODO:
	// Format
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
	public string? ZipCode { get; set; }

	// Payment
	public string? CardName { get; set; }
	public string? CardNumber { get; set; }
	public string? Expiration { get; set; }
	public string? CVV { get; set; }
	public int PaymentMethod { get; set; }
}
