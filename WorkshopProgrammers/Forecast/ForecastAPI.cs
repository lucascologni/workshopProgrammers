using System;
using System.Xml;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using MMLib.Extensions;
using System.Linq;

namespace WorkshopProgrammers.Forecast
{
    public class ForecastAPI : IForecastAPI
    {
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
                var result = await client.GetAsync(ForecastConsts.CITY_ENDPOINT + cityName);

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
                var result = await client.GetAsync($"{ForecastConsts.CITY_FORECAST_ENDPOINT + cityID}/previsao.xml");

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
            List<ForecastResult> forecastResultList = new List<ForecastResult>();

            for (int i = 0; i < forecasts.Count - 1; i++)
            {
                var d = forecasts.Item(i).FirstChild;
                var t = d.NextSibling;
                var max = t.NextSibling;
                var min = max.NextSibling;

                ForecastResult forecastResult = GetWeatherImagesLinks(t.InnerText);

                forecastResult.Dia = DateTime.ParseExact(d.InnerText, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                forecastResult.Tempo = GetWeatherDescriptionByInitials(t.InnerText);
                forecastResult.Minima = min.InnerText;
                forecastResult.Maxima = max.InnerText;

                forecastResultList.Add(forecastResult);
            }

            return forecastResultList;
        }

        private ForecastResult GetWeatherImagesLinks(string weatherInitials)
        {
            ForecastResult forecastResult = new ForecastResult();

            if (ForecastConsts.SUNNY_CONDITION_LIST.Contains(weatherInitials))
            {
                forecastResult.LinkImagemClima = ForecastConsts.SUNNY_CONDITION_IMAGE_LINK;
                forecastResult.LinkImagemBackground = ForecastConsts.SUNNY_CONDITION_IMAGE_BACKGROUND_LINK;
            }

            else if (ForecastConsts.CLOUDY_CONDITION_LIST.Contains(weatherInitials))
            {
                forecastResult.LinkImagemClima = ForecastConsts.CLOUDY_CONDITION_IMAGE_LINK;
                forecastResult.LinkImagemBackground = ForecastConsts.CLOUDY_CONDITION_IMAGE_BACKGROUND_LINK;
            }

            else if (ForecastConsts.RAINY_CONDITION_LIST.Contains(weatherInitials))
            {
                forecastResult.LinkImagemClima = ForecastConsts.RAINY_CONDITION_IMAGE_LINK;
                forecastResult.LinkImagemBackground = ForecastConsts.RAINY_CONDITION_IMAGE_BACKGROUND_LINK;
            }

            else if (ForecastConsts.SNOWY_CONDITION_LIST.Contains(weatherInitials))
            {
                forecastResult.LinkImagemClima = ForecastConsts.SNOWY_CONDITION_IMAGE_LINK;
                forecastResult.LinkImagemBackground = ForecastConsts.SNOWY_CONDITION_IMAGE_BACKGROUND_LINK;
            }

            else
            {
                forecastResult.LinkImagemClima = ForecastConsts.UNKNOWN_CONDITION_IMAGE_LINK;
                forecastResult.LinkImagemBackground = ForecastConsts.UNKNOWN_CONDITION_IMAGE_BACKGROUND_LINK;
            }

            return forecastResult;
        }

        private string GetWeatherDescriptionByInitials(string weatherInitials)
        {
            string weatherDiscription = string.Empty;

            ForecastConsts.TIME_MAP.TryGetValue(weatherInitials, out weatherDiscription);

            return weatherDiscription;
        }
    }
}