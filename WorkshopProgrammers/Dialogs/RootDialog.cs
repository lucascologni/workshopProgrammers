using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Bot.Connector;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis.Models;

namespace WorkshopProgrammers.Dialogs
{
    [LuisModel("8b68a847-983e-4e94-b3bc-7f5fd1c1a4d2", "9d4608a23212416397bf1101691af52b")]
    [Serializable]
    public class RootDialog : LuisDialog<object>
    {
        private const string CITY_ENTITY = "City";

        [LuisIntent("")]
        [LuisIntent("None")]
        public async Task None(IDialogContext context, LuisResult result)
        {
            //Envia a mensagem para a conversa.
            await context.PostAsync("Desculpe, não entendi");
        }

        [LuisIntent("Forecast")]
        public async Task Forecast(IDialogContext context, LuisResult result)
        {
            //Verifica se há alguma entidade do tipo "City" na mensagem.
            if (result.Entities.Any(x => x.Type == CITY_ENTITY))

                //Chama o diálogo "ForecastDialog", encaminhando a entidade do tipo "City".
                //Configura "ResumeAfterForecast" como callback após o término do diálogo "ForecastDialog".
                context.Call(new ForecastDialog(result.Entities.First().Entity), ResumeAfterForecast);
            else
            {
                //Envia a mensagem para a conversa.
                await context.PostAsync("Qual a cidade?");

                //Aguarda uma nova mensagem do usuário.
                context.Wait(GetCityAnswer);
            }
        }

        [LuisIntent("Greetings")]
        public async Task Greetings(IDialogContext context, LuisResult result)
        {
            //Envia a mensagem para a conversa.
            await context.PostAsync("Olá! Sou o novo Bot de previsão do tempo. Em que posso lhe ajudar?");
        }

        private async Task GetCityAnswer(IDialogContext context, IAwaitable<object> result)
        {
            //Trata result como uma "Activity" e aguarda seus valores.
            var message = await result as Activity;

            //Chama o diálogo "ForecastDialog", encaminhando a mensagem do usuário.
            //Configura "ResumeAfterForecast" como callback após o término do diálogo "ForecastDialog".
            context.Call(new ForecastDialog(message.Text), ResumeAfterForecast);
        }

        private async Task ResumeAfterForecast(IDialogContext context, IAwaitable<object> result)
        {
            //Aguarda uma nova mensagem do usuário.
            context.Wait(MessageReceived);
        }
    }
}