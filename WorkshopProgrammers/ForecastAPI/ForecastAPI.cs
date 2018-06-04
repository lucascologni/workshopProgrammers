using System;
using System.Xml;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using MMLib.Extensions;

namespace WorkshopProgrammers.ForecastAPI
{
    public class ForecastAPI : IForecastAPI
    {
        private const string GET_CITY_ENDPOINT = "http://servicos.cptec.inpe.br/XML/listaCidades?city=";
        private const string GET_CITY_FORECAST_ENDPOINT = "http://servicos.cptec.inpe.br/XML/cidade/";


        public async Task<List<ForecastResult>> GetForecast(string cityName)
        {
            var cityID = await GetCityIDAsync(cityName.RemoveDiacritics());
            if (cityID != null)
                return await GetForecastAsync(cityID);

            return null;
        }

        private async Task<string> GetCityIDAsync(string cityName)
        {
            using (var client = new HttpClient())
            {
                var result = await client.GetAsync(GET_CITY_ENDPOINT + cityName);

                if (result.StatusCode == HttpStatusCode.OK)
                {
                    var content = await result.Content.ReadAsStringAsync();
                    var xml = new XmlDocument();
                    xml.LoadXml(content);

                    if (xml.GetElementsByTagName("cidade").Count > 0)
                        return xml.GetElementsByTagName("id").Item(0).InnerText;

                }
                return null;
            }
        }

        private async Task<List<ForecastResult>> GetForecastAsync(string cityID)
        {
            using (var client = new HttpClient())
            {
                var result = await client.GetAsync($"{GET_CITY_FORECAST_ENDPOINT + cityID}/previsao.xml");

                if (result.StatusCode == HttpStatusCode.OK)
                {
                    var content = await result.Content.ReadAsStringAsync();
                    var xml = new XmlDocument();
                    xml.LoadXml(content);

                    var forecasts = xml.GetElementsByTagName("previsao");
                    if (forecasts.Count > 0)
                        return GetForecastData(forecasts);
                }
                return null;
            }
        }

        private List<ForecastResult> GetForecastData(XmlNodeList forecasts)
        {
            List<ForecastResult> forecastList = new List<ForecastResult>();
            for (int i = 0; i < forecasts.Count - 1; i++)
            {
                var d = forecasts.Item(i).FirstChild;
                var t = d.NextSibling;
                var max = t.NextSibling;
                var min = max.NextSibling;

                forecastList.Add(new ForecastResult()
                {
                    dia = DateTime.ParseExact(d.InnerText, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture),
                    tempo = t.InnerText,
                    maxima = max.InnerText,
                    minima = min.InnerText

                });

            }
            return forecastList;
        }
    }
}