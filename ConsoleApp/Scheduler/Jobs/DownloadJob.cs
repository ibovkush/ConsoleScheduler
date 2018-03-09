using ConsoleApp.Database;
using ConsoleApp.Parser.Exceptions;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Scheduler.Jobs
{
    internal class DownloadJob : IJob
    {

        private async void DoStuff()
        {
            using (var client = new HttpClient())
            {
                var getDataUrl = System.Configuration.ConfigurationManager.AppSettings["GetDataUrl"];

                var isDebugAllData = bool.Parse(System.Configuration.ConfigurationManager.AppSettings["DebugAllData"] ?? "False");

                if (string.IsNullOrEmpty(getDataUrl))
                {
                    await Console.Out.WriteLineAsync(">>>Url for data not found in config. Add key 'GetDataUrl'.");
                    return;
                }

                client.BaseAddress = new Uri(getDataUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                await Console.Out.WriteLineAsync(">>>Start to download the Data:");

                HttpResponseMessage response = await client.GetAsync("");
                if (response.IsSuccessStatusCode)
                {
                    await Console.Out.WriteLineAsync(">>>Data downloaded successfuly:");

                    var data = await response.Content.ReadAsStringAsync();
                    await Console.Out.WriteLineAsync("START ==================");

                    if (isDebugAllData)
                    {
                        await Console.Out.WriteLineAsync(data);
                    }
                    else
                    {
                        await Console.Out.WriteLineAsync("SKIPPED. Set 'DebugAllData' key to 'True' to see all received data.");
                    }

                    await Console.Out.WriteLineAsync("END ==================");
                    await Console.Out.WriteLineAsync(">>>Try to parse Data");

                    try
                    {
                        var prices = await Parser.Parser.Instance.Parse(data);
                        await Console.Out.WriteLineAsync(">>>Data Parsed!");
                        await Console.Out.WriteLineAsync(">>>Try to update Database!");
                        var updatedCount = await Updater.Instance.Update(prices);
                        await Console.Out.WriteLineAsync($">>>Database updated successful! {updatedCount} items added.");

                    }
                    catch (FuelDataParceErrorException ex)
                    {
                        await Console.Out.WriteLineAsync(">>>Found an ERROR! Performing Stopped.");
                        await Console.Out.WriteLineAsync("ERROR ==================");
                        await Console.Out.WriteLineAsync(ex.Message);
                        await Console.Out.WriteLineAsync("END ==================");
                    }

                }
                else
                {
                    await Console.Out.WriteLineAsync($">>>Data download Failed with reason: {response.ReasonPhrase}");

                }
            }

        }

        public void Execute(IJobExecutionContext context)
        {
            DoStuff();
        }
    }
}
