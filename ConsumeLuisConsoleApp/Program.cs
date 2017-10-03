using Microsoft.Cognitive.LUIS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsumeLuisConsoleApp
{
    class Program
    {
        static string _appId = "your app id here";
        static string _subscriptionKey = "subscription key here";
        static bool _preview = true;
        static string _textToPredict = "open the door";


        static void Main(string[] args)
        {
            Program prg = new Program();
            Task.WaitAll(prg.Predict());
        }

        public async Task Predict()
        {
            LuisClient client = new LuisClient(_appId, _subscriptionKey, _preview);
            LuisResult res = await client.Predict(_textToPredict);
            processRes(res);

        }

        private void processRes(LuisResult res)
        {
            Console.WriteLine("OriginalQuery = " + res.OriginalQuery);
            Console.WriteLine("TopScoringIntent = " + res.TopScoringIntent.Name);

            Console.WriteLine("-------AllEntities-------");
            var entities = res.GetAllEntities();
            foreach (Entity entity in entities)
            {
                Console.WriteLine("entity.Name = " + entity.Name);                
            }
            
            if (res.DialogResponse != null)
            {
                Console.WriteLine("-------DialogResponse-------");
                if (res.DialogResponse.Status != "Finished")
                {
                    Console.WriteLine(res.DialogResponse.Prompt);
                }
                else
                {
                    Console.WriteLine("Finished");
                }
            }
        }

    }
}
