using RestSharp;

namespace GestTPV
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
			enviaWhatsAppAsync();
		}

		private async Task enviaWhatsAppAsync()
		{
			var url = "https://api.alvochat.com/instance1199/messages/chat";
			var client = new RestClient(url);

			var request = new RestRequest(url, Method.Post);
			request.AddHeader("content-type", "application/x-www-form-urlencoded");
			request.AddParameter("token", "1560363990790602");
			request.AddParameter("to", "+34669250375");
			request.AddParameter("body", "WhatsApp API on alvochat.com works good");
			request.AddParameter("priority", "");
			request.AddParameter("preview_url", "");
			request.AddParameter("message_id", "");


			RestResponse response = await client.ExecuteAsync(request);
			var output = response.Content;
			Console.WriteLine(output);
		}
	}
}
