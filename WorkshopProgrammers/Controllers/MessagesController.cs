using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using WorkshopProgrammers.Dialogs;

namespace WorkshopProgrammers
{
    [BotAuthentication]
    public class MessagesController : ApiController
    {
        /// <summary>
        /// POST: api/Messages
        /// Receive a message from a user and reply to it
        /// </summary>
        public async Task<HttpResponseMessage> Post([FromBody]Activity activity)
        {
            //Verifica se a mensagem recebida é do tipo "Mensagem".
            if (activity.GetActivityType() == ActivityTypes.Message)
                
                //Encaminha a mensagem para a pilha de diálogos.
                await Conversation.SendAsync(activity, () => new RootDialog());

            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}