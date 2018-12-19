using System.Threading.Tasks;
using Microsoft.Bot.Connector;
using Microsoft.Bot.Builder.Dialogs;
using System.Collections.Generic;
using AdaptiveCards;
using WorkshopProgrammers.Forecast;
using WorkshopProgrammers.Dialogs.AdaptiveCards;

namespace WorkshopProgrammers.Dialogs
{
    public class ForecastDialog : IDialog
    {
        private readonly IForecastAPI forecastAPI;
        private readonly string _entity;

        public ForecastDialog(string entity)
        {
            _entity = entity;
            forecastAPI = new ForecastAPI();
        }

        public async Task StartAsync(IDialogContext context)
        {
            //Coleta previsões do tempo.
            var results = await forecastAPI.GetForecast(_entity);

            if (results != null)
            {
                //Cria uma nova Activity.
                var message = context.MakeMessage();

                Attachment attachment = new Attachment();
                attachment.ContentType = AdaptiveCard.ContentType;

                foreach (var item in results)
                {
                    attachment.Content = new ForecastAdaptiveCard().GetAdaptiveCard(item);
                    message.Attachments = new List<Attachment> { attachment };

                    //Envia a mensagem contendo os cards para o usuário.
                    await context.PostAsync(message);
                }

                //Finaliza o diálogo atual, retornando o controle para o RootDialog.
                context.Done("");
            }

            else
            {
                //Envia mensagem para o usuário.
                await context.PostAsync($"Não consegui encontrar nenhuma previsão para \"{_entity}\".");

                //Finaliza o diálogo atual, retornando o controle para o RootDialog.
                context.Done("");
            }
        }
    }
}