using Twilio;
using Twilio.Rest.Api.V2010.Account;
namespace SenMSGText.Data
{
    public class TwilioSmsService
    {
        private readonly string _accountSid;
        private readonly string _authToken;
        private readonly string _phoneNumber;

        public TwilioSmsService(IConfiguration configuration)
        {
            _accountSid = configuration["Twilio:AccountSid"];
            _authToken = configuration["Twilio:AuthToken"];
            _phoneNumber = configuration["Twilio:PhoneNumber"];
        }

        public async Task<bool> EnviarMensajeAsync(string destino, string mensaje)
        {
            try
            {
                TwilioClient.Init(_accountSid, _authToken);

                var message = await MessageResource.CreateAsync(
                    body: mensaje,
                    from: new Twilio.Types.PhoneNumber(_phoneNumber),
                    to: new Twilio.Types.PhoneNumber(destino)
                );

                // Puedes manejar la respuesta de Twilio según tus necesidades
                return !string.IsNullOrEmpty(message.Sid);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
