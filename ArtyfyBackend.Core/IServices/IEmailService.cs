namespace ArtyfyBackend.Core.Services
{
	public interface IEmailService
	{
		public void SendEmail(string from, string to, string subject, string body);
	}
}
